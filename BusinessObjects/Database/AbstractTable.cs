using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Database
{
    /// <summary>
    /// EditMode enumeration used by the AbstractTable class
    /// </summary>
    public enum EditMode
    {
        /// <summary>
        /// State has not been altered
        /// </summary>
        None,
        /// <summary>
        /// In add mode. When save is called, the object will be inserted
        /// </summary>
        Add,
        /// <summary>
        /// in edit mode. When save is called, the object will be updated
        /// </summary>
        Edit,
        /// <summary>
        /// Dirty mode. Save has been called, and the row has not been re-read from the db yet.
        /// </summary>
        Dirty
    }

    /// <summary>
    /// Abastract base class used by MySqlToC#.
    /// </summary>
    public abstract class AbstractTable : BaseSqlToCS
    {
        
        #region Private Fields

        private bool m_existsInDB = false; 

        #endregion
        
        #region Constructors
        
        /// <summary>
        /// Constructor used to create an instance of this table in AddMode.
        /// </summary>
        public AbstractTable(SqlHelper db) : base(db)
        {
        }
        
        /// <summary>
        /// Constructor used to create an instance of this table using an
        /// existing SqlDataReader.
        /// </summary>
        public AbstractTable(SqlHelper db, SqlDataReader dr) : this(db)
        {
            ReadExecuted(dr);
        }
        
        #endregion
        
        #region Indexer
        
        /// <summary>
        /// Codegen should override this indexer with strongly-typed code.
        /// </summary>
        public virtual object this[string field]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        
        #endregion
        
        #region Public Properties
        
        /// <summary>
        /// Gets a value indicating whether or not the record was read from the database
        /// </summary>
        public bool ExistsInDB
        {
            get
            {
                return m_existsInDB;
            }
            protected set
            {
                m_existsInDB = value;
            }
        }
        
        /// <summary>
        /// Returns the name of the underlying database object used to generate the class
        /// </summary>
        public virtual string DatabaseObjectName
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        
        #endregion
        
        #region Database Methods
        
        /// <summary>
        /// Saves the current row using a single transaction
        /// </summary>
        public override void Save()
        {
            SqlTransaction tx = null;
            try
            {
                tx = m_db.BeginTransaction();
                Save(tx);
                tx.Commit();
                //Read(); let's not do this right now
            }
            catch (Exception ex)
            {
                if (tx != null)
                    tx.Rollback();
                throw new ApplicationException(ex.Message, ex);
            }
        }
        
        /// <summary>
        /// Saves the current row using the supplied transaction
        /// </summary>
        public override void Save(SqlTransaction tx)
        {            
            if (Mode == EditMode.Add)
            {
                ExecuteInsert(tx);
                Mode = EditMode.Dirty;
            }
            else if (Mode == EditMode.Edit)
            {
                ExecuteUpdate(tx);
                Mode = EditMode.Dirty;
            }
        }
        
        private void ExecuteInsert(SqlTransaction tx)
        {
            // GenerateSql
            int context = 0;
            string sql = GenerateSql(context);
            SqlParameter[] parameters = GenerateSqlParameters(context);
            // manage identies here
            if (HasIdentity)
                sql += " SELECT SCOPE_IDENTITY()";
            object result = m_db.ExecuteScalar(tx, CommandType.Text, sql, parameters);
            if (HasIdentity && result != null)
                SetIdentity(result);
        }
        
        private void ExecuteUpdate(SqlTransaction tx)
        {
            // GenerateSql
            int context = 0;
            string sql = GenerateSql(context);
            SqlParameter[] parameters = GenerateSqlParameters(context);
            if (!string.IsNullOrEmpty(sql))
            {
                m_db.ExecuteNonQuery(tx, CommandType.Text, sql, parameters);
            }
        }
        
        /// <summary>
        /// Set's the identity column. Called after an insert
        /// </summary>
        protected abstract void SetIdentity(object value);
        
        /// <summary>
        /// determines if a table has an identity. set by the codegen
        /// </summary>
        protected virtual bool HasIdentity
        {
            get
            {
                return false;
            }
        }
        
        /// <summary>
        /// Read using default select and primary key
        /// </summary>
        public virtual void Read()
        {
            Read(null, GenerateSqlSelect(0), GenerateSqlParametersPrimaryKey(0));
        }
        
        /// <summary>
        /// Read using default select and primary key while in a Transaction
        /// </summary>
        public virtual void Read(SqlTransaction tx)
        {
            Read(tx, GenerateSqlSelect(0), GenerateSqlParametersPrimaryKey(0));
        }
        
        /// <summary>
        /// Read using custom sql and parameters
        /// </summary>
        public virtual void Read(string sql, SqlParameter[] commandParameters)
        {
            Read(null, sql, commandParameters);
        }
        
        /// <summary>
        /// Read a single record
        /// </summary>
        public virtual void Read(SqlTransaction tx, string sql, SqlParameter[] commandParameters)
        {
            using (SqlDataReader dr = m_db.ExecuteReader(tx, System.Data.CommandType.Text, sql, commandParameters))
            {
                // read first row
                if (dr.Read())
                {
                    ReadExecuted(dr);
                    if (dr.Read())
                        throw new ApplicationException("More than one row returned by Read operation!");
                }
                else
                {
                    ExistsInDB = false;
                    Mode = EditMode.Add;
                    SetOriginalValues();
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        protected abstract void ReadExecuted(System.Data.SqlClient.SqlDataReader datareader);
        
        /// <summary>
        /// 
        /// </summary>
        protected abstract string GenerateSqlSelect(int context);
        
        /// <summary>
        /// 
        /// </summary>
        protected abstract string GenerateWhereClausePrimaryKey(int context);
        
        /// <summary>
        /// Generates an array of SqlParameters based on the primary key of the table
        /// </summary>
        public abstract SqlParameter[] GenerateSqlParametersPrimaryKey(int context);
        
        /// <summary>
        /// 
        /// </summary>
        protected abstract string GenerateSqlInsert(int context);
        
        /// <summary>
        /// 
        /// </summary>
        protected abstract SqlParameter[] GenerateSqlParametersInsert(int context);
        
        /// <summary>
        /// 
        /// </summary>
        protected abstract string GenerateSqlUpdate(int context);
        
        /// <summary>
        /// 
        /// </summary>
        protected abstract SqlParameter[] GenerateSqlParametersUpdate(int context);
        
        /// <summary>
        /// Determines based on editmode wich sql to generate
        /// </summary>
        public virtual string GenerateSql(int context)
        {
            string result = "";
            if (Mode == EditMode.Add)
            {
                result = GenerateSqlInsert(context);
            }
            else if (Mode == EditMode.Edit)
            {
                result = GenerateSqlUpdate(context);
            }
            return result;
        }
        
        /// <summary>
        /// Determines based on editmode which sql parameters to generate
        /// </summary>
        public virtual SqlParameter[] GenerateSqlParameters(int context)
        {
            // determine mode
            SqlParameter[] results = new SqlParameter[0];
            if (Mode == EditMode.Add)
            {
                results = GenerateSqlParametersInsert(context);
            }
            else if (Mode == EditMode.Edit)
            {
                results = GenerateSqlParametersUpdate(context);
            }
            return results;
        }
        
        /// <summary>
        /// Deletes a row from the database using the supplied transaction
        /// </summary>
        public override void Delete(SqlTransaction tx)
        {
            if (ExistsInDB)
            {
                // GenerateSql
                int context = 0;
                string sql = GenerateSqlDelete(context);
                SqlParameter[] parameters = GenerateSqlParametersPrimaryKey(context);
                int results = m_db.ExecuteNonQuery(tx, CommandType.Text, sql, parameters);
                if (results > 1)
                    throw new ApplicationException("Delete resulted in more then one row being deleted");
            }
        }
        
        /// <summary>
        /// Deletes a row from the database using a single transaction
        /// </summary>
        public override void Delete()
        {
            SqlTransaction tx = null;
            try
            {
                tx = m_db.BeginTransaction();
                Delete(tx);
                tx.Commit();
            }
            catch (Exception ex)
            {
                if (tx != null)
                    tx.Rollback();
                throw new ApplicationException(ex.Message);
            }
        }
        
        /// <summary>
        /// Generates a parameterized sql string to delete a row by the primary key of the table
        /// </summary>
        public abstract string GenerateSqlDelete(int context); 
        
        #endregion

        #region Original Values
        
        /// <summary>
        /// Sets/Saves the original values
        /// </summary>
        public abstract void SetOriginalValues(); 

        #endregion
    }
}