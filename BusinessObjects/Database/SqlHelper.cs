using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Linq;

namespace BusinessObjects.Database
{
    /// <summary>
    /// This class provides functions to leverage a static cache of procedure parameters and 
    /// the ability to discover parameters for stored procedures at run-time.
    /// </summary>
    public static class SqlHelperParameterCache
    {

        #region Member Variables

        // Since this class provides only static methods, make the default constructor private to prevent
        // instances from being created with "new SqlHelperParameterCache()".
        /// <summary>
        /// Used to map the sp_procedure_params_rowset resultset to the SqlCommand property enum values
        /// </summary>
        private static readonly Hashtable ParamCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Used to map the sp_procedure_params_rowset resultset to the SqlCommand property enum values
        /// </summary>
        private static readonly Hashtable ParamTypes = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Used to map the sp_procedure_params_rowset resultset to the SqlCommand property enum values
        /// </summary>
        private static readonly Hashtable ParamDirections = Hashtable.Synchronized(new Hashtable());

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="SqlHelperParameterCache"/> class.
        /// </summary>
        static SqlHelperParameterCache()
        {
            // populate the mapping hashtables
            ParamTypes.Add("bigint", SqlDbType.BigInt);
            ParamTypes.Add("binary", SqlDbType.Binary);
            ParamTypes.Add("bit", SqlDbType.Bit);
            ParamTypes.Add("char", SqlDbType.Char);
            ParamTypes.Add("datetime", SqlDbType.DateTime);
            ParamTypes.Add("decimal", SqlDbType.Decimal);
            ParamTypes.Add("float", SqlDbType.Float);
            ParamTypes.Add("image", SqlDbType.Image);
            ParamTypes.Add("int", SqlDbType.Int);
            ParamTypes.Add("money", SqlDbType.Money);
            ParamTypes.Add("nchar", SqlDbType.NChar);
            ParamTypes.Add("ntext", SqlDbType.NText);
            ParamTypes.Add("numeric", SqlDbType.Decimal);
            ParamTypes.Add("nvarchar", SqlDbType.NVarChar);
            ParamTypes.Add("real", SqlDbType.Real);
            ParamTypes.Add("smalldatetime", SqlDbType.SmallDateTime);
            ParamTypes.Add("smallint", SqlDbType.SmallInt);
            ParamTypes.Add("smallmoney", SqlDbType.SmallMoney);
            ParamTypes.Add("sql_variant", SqlDbType.Variant);
            ParamTypes.Add("text", SqlDbType.Text);
            ParamTypes.Add("timestamp", SqlDbType.Timestamp);
            ParamTypes.Add("tinyint", SqlDbType.TinyInt);
            ParamTypes.Add("uniqueidentifier", SqlDbType.UniqueIdentifier);
            ParamTypes.Add("varbinary", SqlDbType.VarBinary);
            ParamTypes.Add("varchar", SqlDbType.VarChar);

            ParamDirections.Add((short)1, ParameterDirection.Input);
            ParamDirections.Add((short)2, ParameterDirection.InputOutput);
            ParamDirections.Add((short)4, ParameterDirection.ReturnValue);
        }

        #endregion

        #region Public Interface

        #region Caching Functions

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters to be cached</param>
        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            var hashKey = connectionString + ":" + commandText;

            ParamCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An array of SqlParameters</returns>
        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            var hashKey = connectionString + ":" + commandText;

            var cachedParameters = (SqlParameter[])ParamCache[hashKey];

            return cachedParameters == null ? null : CloneParameters(cachedParameters);
        }

        #endregion

        #region Parameter Discovery Functions

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <returns>An array of SqlParameters</returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string storedProcedureName)
        {
            return GetSpParameterSet(connectionString, storedProcedureName, false);
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating weather the return value parameter should be included in the results</param>
        /// <returns>An array of SqlParameters</returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string storedProcedureName, bool includeReturnValueParameter)
        {
            var hashKey = connectionString + ":" + storedProcedureName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : string.Empty);

            var cachedParameters = (SqlParameter[])ParamCache[hashKey] ??
                                   (SqlParameter[])(ParamCache[hashKey] = DiscoverSpParameterSet(connectionString, storedProcedureName, includeReturnValueParameter));

            return CloneParameters(cachedParameters);
        }

        #endregion

        #endregion

        #region Private Interface

        /// <summary>
        /// Resolve at run-time the appropriate set of SqlParameters for a stored procedure
        /// </summary>
        /// <remarks>This uses the sql stored procedure <c>sp_procedure_params_rowset</c></remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="storeProcedureName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">Whether or not to include the return value parameter</param>
        /// <returns>The list of SqlParameters for the stored proc</returns>
        private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string storeProcedureName, bool includeReturnValueParameter)
        {
            var paramDescriptions = new DataTable("paramDescriptions");

            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                var cmd = new SqlCommand("sp_procedure_params_rowset", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@procedure_name", storeProcedureName);

                var da = new SqlDataAdapter(cmd);
                da.Fill(paramDescriptions);
            }

            SqlParameter[] discoveredParameters;

            if (paramDescriptions.Rows.Count <= 0)
            {
                // sp not found - throw exception
                throw new ArgumentException("Stored procedure '" + storeProcedureName + "' not found", "storeProcedureName");
            }

            int startRow;

            if (includeReturnValueParameter)
            {
                discoveredParameters = new SqlParameter[paramDescriptions.Rows.Count];
                startRow = 0;
            }
            else
            {
                discoveredParameters = new SqlParameter[paramDescriptions.Rows.Count - 1];
                startRow = 1;
            }

            for (int i = 0, j = discoveredParameters.Length; i < j; i++)
            {
                var paramRow = paramDescriptions.Rows[i + startRow];
                discoveredParameters[i] = new SqlParameter
                {
                    ParameterName = (string)paramRow["PARAMETER_NAME"],
                    SqlDbType = (SqlDbType)ParamTypes[paramRow["TYPE_NAME"]],
                    Direction = (ParameterDirection)ParamDirections[(short)paramRow["PARAMETER_TYPE"]],
                    Size = paramRow["CHARACTER_OCTET_LENGTH"] == DBNull.Value ? 0 : (int)paramRow["CHARACTER_OCTET_LENGTH"],
                    Precision = paramRow["NUMERIC_PRECISION"] == DBNull.Value ? (byte)0 : (byte)(short)paramRow["NUMERIC_PRECISION"],
                    Scale = paramRow["NUMERIC_SCALE"] == DBNull.Value ? (byte)0 : (byte)(short)paramRow["NUMERIC_SCALE"]
                };
            }

            return discoveredParameters;
        }

        /// <summary>
        /// Creates a deep copy of cached SqlParameter array.
        /// </summary>
        /// <param name="originalParameters">The original parameters.</param>
        /// <returns>The list of SqlParameters</returns>
        private static SqlParameter[] CloneParameters(IList<SqlParameter> originalParameters)
        {
            var clonedParameters = new SqlParameter[originalParameters.Count];

            for (int i = 0, j = originalParameters.Count; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion

    }

    /// <summary>
    /// Helper class used to abstract the ADO.Net <see cref="System.Data.SqlClient.SqlConnection"/> and <see cref="System.Data.SqlClient.SqlCommand"/> objects
    /// </summary>
    /// <remarks>
    /// Usage:
    /// <code>
    /// try
    /// {
    ///     using(SqlHelper db = new SqlHelper())
    ///     {
    ///          // us db connection here
    ///     } // connection is automatically closed from the IDisposable interface
    /// }
    /// catch (Exception e)
    /// {
    ///     // handle exception
    /// }
    /// </code>
    /// </remarks>
    public sealed class SqlHelper : IDisposable
    {

        #region Private Fields

        /// <summary>
        /// The Trace Source
        /// </summary>
        private static readonly TraceSource Trace = new TraceSource("eHR.Common.SqlHelper");

        /// <summary>
        /// Used to store the database connection.
        /// </summary>
        private SqlConnection _connection;

        /// <summary>
        /// Used to store the database connection string.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// The timeout value the SQL Command 
        /// </summary>
        private int _commandTimeout;

        /// <summary>
        /// Indicator if the object is being disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Gets or sets a value indicating whether to leave the connection open.  Using() and Dispose() will not manage the connection.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [leave connection open]; otherwise, <c>false</c>.
        /// </value>
        private bool _leaveConnectionOpen = false;

        #endregion

        #region Constructors and Destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlHelper"/> class. Reads in configuration and creates connection.
        /// </summary>
        /// <remarks>
        /// Reads the "ConnectionString" attribute from the <see cref="AppConfigSettings"/> object
        /// </remarks>
        public SqlHelper()
            : this(ConfigHelper.ConnectionString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlHelper"/> class with the provided connection string
        /// </summary>
        /// <param name="connectionString">valid connection string <see cref="System.Data.SqlClient.SqlConnection.ConnectionString"/></param>
        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        #region Classes, Structures and Enumerations

        /// <summary>
        /// This enum is used to indicate weather the connection was provided by the caller, or created by SqlHelper, so that
        /// we can set the appropriate CommandBehavior when calling ExecuteReader()
        /// </summary>
        private enum SqlConnectionOwnership
        {
            /// <summary>Connection is owned and managed by SqlHelper</summary>
            Internal,

            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Command Timeout Used.
        /// </summary>
        /// <value>
        /// The command time out.
        /// </value>
        public int CommandTimeOut
        {
            get
            {
                return _commandTimeout;
            }

            set
            {
                _commandTimeout = value;
            }
        }

        /// <summary>
        /// Gets name of server
        /// </summary>
        public string DataSource
        {
            get
            {
                // We don't care to test if _connection is null because it simply can't be due to the nature
                // of it being hidden and automatically constucted as part of the constructors
                try
                {
                    OpenConnection();
                    var result = _connection.DataSource;
                    return result;
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// Gets name of database
        /// </summary>
        public string Database
        {
            get
            {
                // We don't care to test if _connection is null because it simply can't be due to the nature
                // of it being hidden and automatically constucted as part of the constructors
                try
                {
                    OpenConnection();
                    var result = _connection.Database;
                    return result;
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// Gets the current UTC DateTime structure from the SQL Server
        /// Uses the SqlHelper object passed in
        /// </summary>
        /// <returns>System.DateTime object of the current UTC datetime on the SQL Server</returns>
        public DateTime SqlDateTime
        {
            get
            {
                var temp = ExecuteScalar(CommandType.Text, "SELECT GETUTCDATE()");
                return DateTime.Parse(temp.ToString());
            }
        }

        #endregion

        #region Public Static Interface

        /// <summary>
        /// Converts data from a datareader to a XmlDocument
        /// </summary>
        /// <param name="dr">The SqlDatareader to construct the XmlDocument from</param>
        /// <param name="useElements">True to use elements, false to use attributes</param>
        /// <param name="rootName">Name of the document root</param>
        /// <param name="rowName">Name of each node that represents a row in the datareader</param>
        /// <returns>XmlDocument containing data from the passed SqlDataReader</returns>
        public static XmlDocument DataReaderToXml(SqlDataReader dr, bool useElements, string rootName, string rowName)
        {
            var dom = new XmlDocument();

            var elem = dom.CreateElement(rootName);
            dom.AppendChild(elem);

            var root = dom.DocumentElement;

            while (dr.Read())
            {
                // create row
                elem = dom.CreateElement(rowName);

                // loop through each field
                for (var x = 0; x < dr.FieldCount; x++)
                {
                    if (useElements)
                    {
                        var child = dom.CreateElement(dr.GetName(x).ToLower());
                        child.InnerText = dr[x].ToString();
                        elem.AppendChild(child);
                    }
                    else
                    {
                        var attr = dom.CreateAttribute(dr.GetName(x).ToLower());
                        attr.Value = dr[x].ToString();
                        elem.Attributes.Append(attr);
                    }
                }

                // add row
                if (root != null)
                {
                    root.AppendChild(elem);
                }
            }

            return dom;
        }

        /// <summary>
        /// Converts data from a datareader to a XmlDocument using attributes and the default names for 
        /// rootName and rowName
        /// </summary>
        /// <param name="dr">The SqlDatareader to construct the XmlDocument from</param>
        /// <returns>XmlDocument containing data from the passed SqlDataReader</returns>
        public static XmlDocument DataReaderToXml(SqlDataReader dr)
        {
            return DataReaderToXml(dr, false);
        }

        /// <summary>
        /// Converts data from a datareader to a XmlDocument using the default names for 
        /// rootName and rowName
        /// </summary>
        /// <param name="dr">The SqlDatareader to construct the XmlDocument from</param>
        /// <param name="useElements">true to use elements, false to use attributes</param>
        /// <returns>XmlDocument containing data from the passed SqlDataReader</returns>
        public static XmlDocument DataReaderToXml(SqlDataReader dr, bool useElements)
        {
            return DataReaderToXml(dr, useElements, "rows", "row");
        }

        /// <summary>
        /// Gets the current UTC DateTime structure from the SQL Server
        /// </summary>
        /// <returns>System.DateTime object of the current UTC datetime on the SQL Server</returns>
        public static DateTime GetCurrentSqlDateTime()
        {
            using (var db = new SqlHelper())
            {
                return db.SqlDateTime;
            }
        }

        #endregion

        #region Public Interface

        #region General Public Methods

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="type">The parameter type.</param>
        /// <param name="value">The parameter value.</param>
        /// <returns>The populated instance of a SqlParameter</returns>
        public SqlParameter CreateParameter(string name, SqlDbType type, object value)
        {
            var result = new SqlParameter
            {
                ParameterName = name,
                SqlDbType = type,
                Value = value ?? DBNull.Value
            };

            return result;
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Closes connection and suppresses finalize
        /// </summary>
        public void Dispose()
        {
            // call internal disose
            Dispose(true);

            // supress finalization
            GC.SuppressFinalize(this);
        }

        #endregion

        #region BeginTransaction

        /// <summary>
        /// Begin a transaction on the internal SqlConnection
        /// </summary>
        /// <returns>An object representing the new transaction.</returns>
        public SqlTransaction BeginTransaction()
        {
            OpenConnection();

            _leaveConnectionOpen = true;

            return _connection.BeginTransaction();
        }

        /// <summary>
        /// Begins a database transaction with the specified isolation level.
        /// </summary>
        /// <param name="iso">The isolation level under which the transaction should run</param>
        /// <returns>An object representing the new transaction.</returns>
        public SqlTransaction BeginTransaction(IsolationLevel iso)
        {
            OpenConnection();

            _leaveConnectionOpen = true;

            return _connection.BeginTransaction(iso);
        }

        /// <summary>
        /// Begins a database transaction with the specified transaction name.
        /// </summary>
        /// <param name="transactionName">The name of the transaction.</param>
        /// <returns>An object representing the new transaction.</returns>
        public SqlTransaction BeginTransaction(string transactionName)
        {
            OpenConnection();

            _leaveConnectionOpen = true;

            return _connection.BeginTransaction(transactionName);
        }

        /// <summary>
        /// Begins a database transaction with the specified isolation level and transaction name.
        /// </summary>
        /// <param name="iso">The isolation level under which the transaction should run</param>
        /// <param name="transactionName">The name of the transaction.</param>
        /// <returns>An object representing the new transaction.</returns>
        public SqlTransaction BeginTransaction(IsolationLevel iso, string transactionName)
        {
            OpenConnection();

            _leaveConnectionOpen = true;

            return _connection.BeginTransaction(iso, transactionName);
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            // pass through the call using a null transaction value
            return ExecuteNonQuery(null, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified SqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@orderId", 24));
        /// </remarks>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // pass through the call using a null transaction value
            return ExecuteNonQuery(null, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection
        /// using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        /// int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="storedProcedureName">
        /// the name of the stored procedure
        /// </param>
        /// <param name="parameterValues">
        /// an array of objects to be assigned as the input values of the stored procedure
        /// </param>
        /// <returns>
        /// an int representing the number of rows affected by the command
        /// </returns>
        public int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
        {
            // pass through the call using a null transaction value
            return ExecuteNonQuery(null, storedProcedureName, parameterValues);
        }

        // these three method overloads currently take both connection and transaction.  In post-beta2 builds, only
        // transaction will need to be passed in, and the .Connection property will be available from that transaction

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection
        /// and SqlTransaction.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  int result = ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified SqlConnection and SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  int result = ExecuteNonQuery(conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@orderId", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // create a command and prepare it for execution
            var cmd = new SqlCommand();

            var result = 0;

            try
            {
                // make sure connection is created and open
                OpenConnection();

                // setup command objects
                PrepareCommand(cmd, transaction, commandType, commandText, commandParameters);

                // finally, execute the command.
                result = cmd.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection
        /// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        ///  int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="storedProcedureName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(SqlTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            // if we got parameter values, we need to figure out where they go
            if (parameterValues != null && parameterValues.Length > 0)
            {
                // pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                var commandParameters = SqlHelperParameterCache.GetSpParameterSet(_connectionString, storedProcedureName);

                // assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // otherwise we can just call the SP without params
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

        #endregion ExecuteNonQuery

        #region ExecuteDataSet

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            // pass through the call using a null transaction value
            return ExecuteDataset(null, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@orderId", 24));
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // pass through the call using a null transaction value
            return ExecuteDataset(null, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
        /// using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        ///  DataSet ds = ExecuteDataset(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="storedProcedureName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public DataSet ExecuteDataset(string storedProcedureName, params object[] parameterValues)
        {
            // pass through the call using a null transaction value
            return ExecuteDataset(null, storedProcedureName, parameterValues);
        }

        // these three method overloads currently take both connection and transaction.  In post-beta2 builds, only
        // transaction will need to be passed in, and the .Connection property will be available from that transaction

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection
        /// and SqlTransaction.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  DataSet ds = ExecuteDataset(conn, trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // pass through the call providing null for the set of SqlParameters
            return ExecuteDataset(transaction, commandType, commandText, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection and SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  DataSet ds = ExecuteDataset(conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@orderId", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // create a command and prepare it for execution
            var cmd = new SqlCommand();
            int rows = -1;

            // create the DataAdapter & DataSet
            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            try
            {
                // make sure connection is created and open
                OpenConnection();

                // setup command objects
                PrepareCommand(cmd, transaction, commandType, commandText, commandParameters);

                // fill the DataSet using default values for DataTable names, etc.
                rows = da.Fill(ds);
            }
            finally
            {
                CloseConnection();
            }

            // return the dataset
            return ds;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
        /// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        ///  DataSet ds = ExecuteDataset(conn, trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="storedProcedureName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public DataSet ExecuteDataset(SqlTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            // if we got parameter values, we need to figure out where they go
            if (parameterValues != null && parameterValues.Length > 0)
            {
                // pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                var commandParameters = SqlHelperParameterCache.GetSpParameterSet(_connectionString, storedProcedureName);

                // assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // call the overload that takes an array of SqlParameters
                return ExecuteDataset(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // otherwise we can just call the SP without params
            return ExecuteDataset(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

        #endregion ExecuteDataSet

        #region ExecuteReader

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  SqlDataReader reader = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public SqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            // pass through the call using a null transaction value
            return ExecuteReader(null, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
        /// Added 4/18/2003
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  SqlDataReader reader = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", 30);
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">Number of seconds for the command to execute -- default value is 30</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public SqlDataReader ExecuteReader(CommandType commandType, string commandText, int commandTimeout)
        {
            // pass through the call using a null transaction value
            return ExecuteReader(null, commandType, commandText, null, SqlConnectionOwnership.Internal, commandTimeout);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  SqlDataReader reader = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@orderId", 24));
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // pass through the call using a null transaction value
            return ExecuteReader(null, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
        /// using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        ///  SqlDataReader reader = ExecuteReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="storedProcedureName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public SqlDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
        {
            // pass through the call using a null transaction value
            return ExecuteReader(null, storedProcedureName, parameterValues);
        }

        // these three method overloads currently take both connection and transaction.  In post-beta2 builds, only
        // transaction will need to be passed in, and the .Connection property will be available from that transaction

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection
        /// and SqlTransaction.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  SqlDataReader reader = ExecuteReader(conn, trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // pass through the call providing null for the set of SqlParameters
            return ExecuteReader(transaction, commandType, commandText, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection and SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///   SqlDataReader reader = ExecuteReader(conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@orderId", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // pass through to private overload, indicating that the connection is owned by the caller
            return ExecuteReader(transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal, 0);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
        /// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        ///  SqlDataReader reader = ExecuteReader(conn, trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="storedProcedureName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public SqlDataReader ExecuteReader(SqlTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            // if we got parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                var commandParameters = SqlHelperParameterCache.GetSpParameterSet(_connectionString, storedProcedureName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // otherwise we can just call the SP without params
            return ExecuteReader(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
        /// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// Added 3/18/2003
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        ///  SqlDataReader reader = ExecuteReader(30, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="commandTimeout">timeout value in seconds (default is 30)</param>
        /// <param name="storedProcedureName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a SqlDataReader containing the resultset generated by the command</returns>
        public SqlDataReader ExecuteReader(int commandTimeout, string storedProcedureName, params object[] parameterValues)
        {
            // if we got parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                var commandParameters = SqlHelperParameterCache.GetSpParameterSet(_connectionString, storedProcedureName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(null, CommandType.StoredProcedure, storedProcedureName, commandParameters, SqlConnectionOwnership.Internal, commandTimeout);
            }

            // otherwise we can just call the SP without params
            return ExecuteReader(CommandType.StoredProcedure, storedProcedureName, commandTimeout);
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            // pass through the call using a null transaction value
            return ExecuteScalar(null, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@orderId", 24));
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // pass through the call using a null transaction value
            return ExecuteScalar(null, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection
        /// using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        ///  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="storedProcedureName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public object ExecuteScalar(string storedProcedureName, params object[] parameterValues)
        {
            // pass through the call using a null transaction value
            return ExecuteScalar(null, storedProcedureName, parameterValues);
        }

        // these three method overloads currently take both connection and transaction.  In post-beta2 builds, only
        // transaction will need to be passed in, and the .Connection property will be available from that transaction

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection
        /// and SqlTransaction.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  int orderCount = (int)ExecuteScalar(conn, trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // pass through the call providing null for the set of SqlParameters
            return ExecuteScalar(transaction, commandType, commandText, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection and SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  int orderCount = (int)ExecuteScalar(conn, trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@orderId", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // create a command and prepare it for execution
            var cmd = new SqlCommand();
            object result;

            try
            {
                // make sure connection is created and open
                OpenConnection();

                // setup command objects
                PrepareCommand(cmd, transaction, commandType, commandText, commandParameters);

                // execute the command & return the results
                result = cmd.ExecuteScalar();
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection
        /// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        ///  int orderCount = (int)ExecuteScalar(conn, trans, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="storedProcedureName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an object containing the value in the 1x1 resultset generated by the command</returns>
        public object ExecuteScalar(SqlTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            // if we got parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                var commandParameters = SqlHelperParameterCache.GetSpParameterSet(_connectionString, storedProcedureName);

                // assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // call the overload that takes an array of SqlParameters
                return ExecuteScalar(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // otherwise we can just call the SP without params
            return ExecuteScalar(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

        #endregion ExecuteScalar

        #region ExecuteXmlReader

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(CommandType commandType, string commandText)
        {
            // pass through the call using a null transaction value
            return ExecuteXmlReader(null, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@orderId", 24));
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // pass through the call using a null transaction value
            return ExecuteXmlReader(null, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
        /// using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        ///  XmlReader r = ExecuteXmlReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="storedProcedureName">the name of the stored procedure using "FOR XML AUTO"</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(string storedProcedureName, params object[] parameterValues)
        {
            // pass through the call using a null transaction value
            return ExecuteXmlReader(null, storedProcedureName, parameterValues);
        }

        // these three method overloads currently take both connection and transaction.  In post-beta2 builds, only
        // transaction will need to be passed in, and the .Connection property will be available from that transaction

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection
        /// and SqlTransaction.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  XmlReader r = ExecuteXmlReader(conn, trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            // pass through the call providing null for the set of SqlParameters
            return ExecuteXmlReader(transaction, commandType, commandText, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection and SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:
        ///  XmlReader r = ExecuteXmlReader(conn, trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@orderId", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
        /// <returns>an XmlReader containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // create a command and prepare it for execution
            var cmd = new SqlCommand();
            XmlReader reader;

            try
            {
                // make sure connection is created and open
                OpenConnection();

                // setup command objects
                PrepareCommand(cmd, transaction, commandType, commandText, commandParameters);

                // execute command and return reader
                reader = cmd.ExecuteXmlReader();
            }
            finally
            {
                // let's not close the connection on XmlReader. 
                // There are situations where the reader will need to reach down the connection to read more data
                // CloseConnection();
            }

            return reader;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
        /// and SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:
        ///  XmlReader r = ExecuteXmlReader(conn, trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction associated with the connection</param>
        /// <param name="storedProcedureName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public XmlReader ExecuteXmlReader(SqlTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            // if we got parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // pull the parameters for this stored procedure from the parameter cache (or discover them & populet the cache)
                var commandParameters = SqlHelperParameterCache.GetSpParameterSet(_connectionString, storedProcedureName);

                // assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // call the overload that takes an array of SqlParameters
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // otherwise we can just call the SP without params
            return ExecuteXmlReader(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

        #endregion ExecuteXmlReader

        public void ExecuteBulkCopy(DataTable t)
        {
            try
            {
                OpenConnection();
                SqlBulkCopy sbc = new SqlBulkCopy(_connection);
                sbc.DestinationTableName = t.TableName;

                // Map columns as they may not be in order
                foreach (DataColumn column in t.Columns)
                {
                    sbc.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                sbc.WriteToServer(t);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void ExecuteBulkCopy(SqlTransaction transaction, DataTable t)
        {
            OpenConnection();
            SqlBulkCopy sbc = new SqlBulkCopy(_connection, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints, transaction);
            sbc.DestinationTableName = t.TableName;

            // Map columns as they may not be in order
            foreach (DataColumn column in t.Columns)
            {
                sbc.ColumnMappings.Add(column.ColumnName, column.ColumnName);
            }

            sbc.WriteToServer(t);
        }

        #endregion

        #region Private Interface

        /// <summary>
        /// This method is used to attach array's of SqlParameters to a SqlCommand.
        /// <para />
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.
        /// <para />
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">An array of SqlParameters to be added to command</param>
        private static void AttachParameters(SqlCommand command, IEnumerable<SqlParameter> commandParameters)
        {
            foreach (var p in commandParameters)
            {
                // check for derived output value with no value assigned
                if (p.Direction == ParameterDirection.InputOutput && p.Value == null)
                {
                    p.Value = DBNull.Value;
                }

                command.Parameters.Add(p);
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of SqlParameters.
        /// </summary>
        /// <param name="commandParameters">array of SqlParameters to be assigned values</param>
        /// <param name="parameterValues">array of objects holding the values to be assigned</param>
        private static void AssignParameterValues(IList<SqlParameter> commandParameters, IList<object> parameterValues)
        {
            if (commandParameters == null || parameterValues == null)
            {
                // do nothing if we get no data
                return;
            }

            // we must have the same number of values as we pave parameters to put them in
            if (commandParameters.Count != parameterValues.Count)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // iterate through the SqlParameters, assigning the values from the corresponding position in the
            // value array
            for (int i = 0, j = commandParameters.Count; i < j; i++)
            {
                commandParameters[i].Value = parameterValues[i];
            }
        }

        /// <summary>
        /// Writes to the log.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="fileName">Name of the file.</param>
        private static void WriteLog(string msg, string fileName)
        {
            try
            {
                var strMessage = string.Format("{0} - {1}", DateTime.Now, msg);

                var fs = new FileStream(fileName, FileMode.Append);
                var sw = new StreamWriter(fs);
                sw.WriteLine(strMessage);
                sw.WriteLine("------------------------------------");
                sw.Close();
                fs.Close();
            }
            catch
            {
                // swallow it - it is better to ignore the long data call and file exceptions rather than throw errors in this case
            }
        }

        /// <summary>
        /// Generates a log text to represent a SQLCommand.
        /// </summary>
        /// <param name="cmd">The SqlCommand object.</param>
        /// <returns>The formatted text</returns>
        private static string CommandLoggingFormatter(SqlCommand cmd)
        {
            var result = cmd.CommandText;

            foreach (SqlParameter p in cmd.Parameters)
            {
                result += string.Format("; Parameter Name:{0}; Value:{1}; Direction:{2}", p.ParameterName, p.Value, p.Direction);
            }

            return result;
        }

        /// <summary>
        /// Create and prepare a SqlCommand, and call ExecuteReader with the appropriate CommandBehavior.
        /// </summary>
        /// <remarks>
        /// If we created and opened the we want the connection to be closed when the DataReader is closed.
        /// If the caller provided the we want to leave it to them to manage.
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="connectionOwnership">indicates weather the connection parameter was provided by the caller, or created by SqlHelper</param>
        /// <param name="commandTimeout">see <see cref="System.Data.SqlClient.SqlCommand.CommandTimeout"/></param>
        /// <returns>SqlDataReader containing the results of the command</returns>
        private SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters, SqlConnectionOwnership connectionOwnership, int commandTimeout)
        {
            // create a command and prepare it for execution
            var cmd = new SqlCommand();
            var previousTimeOut = -1;

            // override timeout if passed in
            if (commandTimeout > 0)
            {
                // save so we can reset it
                previousTimeOut = CommandTimeOut;
                CommandTimeOut = commandTimeout;
            }

            // create a reader
            SqlDataReader dr;

            try
            {
                // make sure connection is created and open
                OpenConnection();

                // setup command objects
                PrepareCommand(cmd, transaction, commandType, commandText, commandParameters);

                // call ExecuteReader with the appropriate CommandBehavior
                if (connectionOwnership == SqlConnectionOwnership.External || _leaveConnectionOpen)
                {
                    dr = cmd.ExecuteReader();
                }
                else
                {
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            finally
            {
                // don't want to do this on a reader.
                // Either the reader will close when it is done or we want it left open
                // CloseConnection();
            }

            // reset to previous timeout
            if (previousTimeOut >= 0 && previousTimeOut != CommandTimeOut)
            {
                CommandTimeOut = previousTimeOut;
            }

            return dr;
        }

        /// <summary>
        /// Opens the connection.
        /// </summary>
        private void OpenConnection()
        {
            if (_connection == null || string.IsNullOrEmpty(_connection.ConnectionString))
            {
                _connection = new SqlConnection { ConnectionString = _connectionString };
            }

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }

        /// <summary>
        /// Close the current connection
        /// </summary>
        private void CloseConnection()
        {
            if (!_leaveConnectionOpen && _connection != null)
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a transaction, command type and parameters
        /// to the provided command.
        /// </summary>
        /// <param name="command">the SqlCommand to be prepared</param>
        /// <param name="transaction">a valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
        private void PrepareCommand(SqlCommand command, SqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters)
        {
            // if _commandTimeout value is not set, the default is 30 seconds
            if (_commandTimeout > 0)
            {
                command.CommandTimeout = _commandTimeout;
            }

            // associate the connectoin with the command
            command.Connection = _connection;

            // set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // if we were provided a transaction, assign it.
            if (transaction != null)
            {
                _leaveConnectionOpen = true;
                command.Transaction = transaction;
            }

            // set the command type
            command.CommandType = commandType;

            // attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            // only dispose once
            if (!_disposed)
            {
                // if called from IDisposable, clear up managed resources
                if (disposing)
                {
                    // verify connection is not null
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }

                _disposed = true;
            }
        }

        #endregion

    }
}