using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using BusinessObjects.Database;

// Code Generated Classes. !!! DO NOT CHANGE !!!
// Database   : LillysFlowersNU
// Created    : 5/25/2015 6:59:07 PM
// User       : Joe
// Connection : Provider=SQLOLEDB.1;Data Source=WINDOWS-28T6H06;Initial Catalog=LillysFlowers;Integrated Security=SSPI
namespace BusinessObjects.GeneratedTables
{
    #region Business Entity Class
    /// <summary>
    /// Description : 
    /// Table Created : 5/25/2015 4:40:50 PM
    /// </summary>
    public partial class WebStatistics : AbstractTable
    {
        private WebStatisticsData m_Current = new WebStatisticsData();
        private WebStatisticsData m_Original;
        
        public const string TABLE_NAME = "web_statistics";
        //private string TABLE_TYPE = "";
        //private bool HAS_LOGICAL_KEYS = false;
        
        #region Constructors
        
        /// <summary>
        /// Constructor used to create an instance of this table in AddMode.
        /// </summary>
        public WebStatistics(SqlHelper db) : base(db) {}

        /// <summary>
        /// Constructor used to create an instance of this table using an
        /// existing SqlDataReader.
        /// </summary>
        public WebStatistics(SqlHelper db, SqlDataReader dr) : base(db, dr) {}

        /// <summary>
        /// Constructor used to create an instance of this table using the
        /// primary keys of the table.
        /// </summary>		
        public WebStatistics(SqlHelper db, int? id) 
            : this(db)
        {
			if (id != null)
			{
				m_Current.Id = id;

                Read();
            }
        }

        /// <summary>
        /// Constructor used to create an instance of this table using the
        /// provided data class.
        /// </summary>		
        public WebStatistics(SqlHelper db, WebStatisticsData data)
            : this(db, data.Id)
        {
            SetCurrentValues(data);
        }
        
        #endregion

        #region Indexer
        /// <summary>
        /// Indexer used to access each field by name.
        /// </summary>
        public override object this[string field]
        {
            get
            {
                object result;
                switch (field.ToLower())
                {
                    case "id":
                        result = Id;
                        break;
                    case "userhostaddress":
                        result = UserHostAddress;
                        break;
                    case "browser":
                        result = Browser;
                        break;
                    case "browserversion":
                        result = BrowserVersion;
                        break;
                    case "url":
                        result = Url;
                        break;
                    case "useragent":
                        result = UserAgent;
                        break;
                    case "accesstime":
                        result = AccessTime;
                        break;
                    default:
                        throw new ApplicationException(String.Format("Field {0} does not exist!", field));
                }
                return result;
            }
            set
            {
                switch (field.ToLower())
                {
                        case "id":
                            throw new ApplicationException(String.Format("Field {0} cannot be set because " +
                                "it is an Identity field or a computed field!", field));
                        case "userhostaddress":
                            if (value == null)
								UserHostAddress = null;
                            else
								UserHostAddress = Convert.ToString(value);
                            break;
                        case "browser":
                            if (value == null)
								Browser = null;
                            else
								Browser = Convert.ToString(value);
                            break;
                        case "browserversion":
                            if (value == null)
								BrowserVersion = null;
                            else
								BrowserVersion = Convert.ToString(value);
                            break;
                        case "url":
                            if (value == null)
								Url = null;
                            else
								Url = Convert.ToString(value);
                            break;
                        case "useragent":
                            if (value == null)
								UserAgent = null;
                            else
								UserAgent = Convert.ToString(value);
                            break;
                        case "accesstime":
                            if (value == null)
								AccessTime = null;
                            else
								AccessTime = Convert.ToDateTime(value);
                            break;
                    default:
                        throw new ApplicationException(String.Format("Field {0} does not exist!", field));
                }
            }
        }
        #endregion
        
        #region Public Properties
        /// <summary>
        /// Returns the name of the underlying database object used to generate the class
        /// </summary>
        public override string DatabaseObjectName
        {
            get { return TABLE_NAME; }
        }
        
        /// <summary>
        /// Gets an instance of a class that represents the fields in the Primary Key
        /// </summary>
        public WebStatisticsPrimaryKey PrimaryKey
        {
            get
            {
                WebStatisticsPrimaryKey key = new WebStatisticsPrimaryKey();
				key.Id = m_Current.Id.GetValueOrDefault();
  			
                return key;
            }
        }
        /// <summary>
        /// Public property that represents the Id field.
        /// Description: 
        /// </summary>
        public int? Id
        {
            get { return m_Current.Id; }
        }
        /// <summary>
        /// Public property that represents the UserHostAddress field.
        /// Description: 
        /// </summary>
        public string UserHostAddress
        {
            get { return m_Current.UserHostAddress; }
            set 
            {	
                if (value != m_Current.UserHostAddress)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.UserHostAddress = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the Browser field.
        /// Description: 
        /// </summary>
        public string Browser
        {
            get { return m_Current.Browser; }
            set 
            {	
                if (value != m_Current.Browser)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Browser = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the BrowserVersion field.
        /// Description: 
        /// </summary>
        public string BrowserVersion
        {
            get { return m_Current.BrowserVersion; }
            set 
            {	
                if (value != m_Current.BrowserVersion)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.BrowserVersion = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the Url field.
        /// Description: 
        /// </summary>
        public string Url
        {
            get { return m_Current.Url; }
            set 
            {	
                if (value != m_Current.Url)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Url = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the UserAgent field.
        /// Description: 
        /// </summary>
        public string UserAgent
        {
            get { return m_Current.UserAgent; }
            set 
            {	
                if (value != m_Current.UserAgent)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.UserAgent = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the AccessTime field.
        /// Description: 
        /// </summary>
        public DateTime? AccessTime
        {
            get { return m_Current.AccessTime; }
            set 
            {	
                if (value != m_Current.AccessTime)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.AccessTime = value;
                }
            }
        }
        #endregion    

        #region Database operations
        ///<summary></summary>
        protected override void ReadExecuted(System.Data.SqlClient.SqlDataReader dr)
        {
            if (dr != null && !dr.IsClosed)
            {
                #region Read Each Field
                if (!dr.IsDBNull(dr.GetOrdinal("Id"))) m_Current.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                if (!dr.IsDBNull(dr.GetOrdinal("UserHostAddress"))) m_Current.UserHostAddress = dr.GetString(dr.GetOrdinal("UserHostAddress"));
                if (!dr.IsDBNull(dr.GetOrdinal("Browser"))) m_Current.Browser = dr.GetString(dr.GetOrdinal("Browser"));
                if (!dr.IsDBNull(dr.GetOrdinal("BrowserVersion"))) m_Current.BrowserVersion = dr.GetString(dr.GetOrdinal("BrowserVersion"));
                if (!dr.IsDBNull(dr.GetOrdinal("Url"))) m_Current.Url = dr.GetString(dr.GetOrdinal("Url"));
                if (!dr.IsDBNull(dr.GetOrdinal("UserAgent"))) m_Current.UserAgent = dr.GetString(dr.GetOrdinal("UserAgent"));
                if (!dr.IsDBNull(dr.GetOrdinal("AccessTime"))) m_Current.AccessTime = dr.GetDateTime(dr.GetOrdinal("AccessTime"));
                #endregion
                ExistsInDB = true;
                Mode = EditMode.None;
                SetOriginalValues();
            }
        }
        
        #region SELECT
        ///<summary></summary>
        protected override string GenerateSqlSelect(int context)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM web_statistics WITH (NOLOCK) ");
            sql.Append(GenerateWhereClausePrimaryKey(context));
            return sql.ToString();
        }
		///<summary></summary>
        protected override string GenerateWhereClausePrimaryKey(int context)
        {
            StringBuilder sql = new StringBuilder();
            // primary key where clause
            sql.AppendFormat(" WHERE Id=@Id_{0}_key", context);
            return sql.ToString();
        }
        /// <summary>
        /// Generates an array of SqlParameters based on the primary key of the table
        /// </summary>
        public override SqlParameter[] GenerateSqlParametersPrimaryKey(int context)
        {
            List<SqlParameter> results = new List<SqlParameter>();
            results.Add(CreateParameter(String.Format("@Id_{0}_key", context), SqlDbType.Int, m_Current.Id));
            return results.ToArray();
        }
        #endregion
        
        #region INSERT
        ///<summary></summary>
        protected override string GenerateSqlInsert(int context)
        {
            StringBuilder result = new StringBuilder();
            List<string> fields = new List<string>();

            result.Append("INSERT INTO web_statistics (");

            #region Check each field to insert
            if (m_Current.UserHostAddress != null) fields.Add("UserHostAddress");
            if (m_Current.Browser != null) fields.Add("Browser");
            if (m_Current.BrowserVersion != null) fields.Add("BrowserVersion");
            if (m_Current.Url != null) fields.Add("Url");
            if (m_Current.UserAgent != null) fields.Add("UserAgent");
            if (m_Current.AccessTime != null) fields.Add("AccessTime");

            #endregion
            
            result.Append(string.Join(",", fields.ToArray()));
            result.Append(") VALUES (");
            for (int x=0; x < fields.Count; x++)
                fields[x] = String.Format("@{0}_{1}", fields[x], context);
            result.Append(string.Join(",", fields.ToArray()));
            result.Append(")");

            return result.ToString();
        }
        ///<summary></summary>
        protected override SqlParameter[] GenerateSqlParametersInsert(int context)
        {
            List<SqlParameter> results = new List<SqlParameter>();

            #region Check each field to insert

            if (m_Current.UserHostAddress != null)
                results.Add(CreateParameter(String.Format("@UserHostAddress_{0}", context), SqlDbType.VarChar, m_Current.UserHostAddress));
            if (m_Current.Browser != null)
                results.Add(CreateParameter(String.Format("@Browser_{0}", context), SqlDbType.VarChar, m_Current.Browser));
            if (m_Current.BrowserVersion != null)
                results.Add(CreateParameter(String.Format("@BrowserVersion_{0}", context), SqlDbType.VarChar, m_Current.BrowserVersion));
            if (m_Current.Url != null)
                results.Add(CreateParameter(String.Format("@Url_{0}", context), SqlDbType.VarChar, m_Current.Url));
            if (m_Current.UserAgent != null)
                results.Add(CreateParameter(String.Format("@UserAgent_{0}", context), SqlDbType.VarChar, m_Current.UserAgent));
            if (m_Current.AccessTime != null)
                results.Add(CreateParameter(String.Format("@AccessTime_{0}", context), SqlDbType.DateTime, m_Current.AccessTime));
            #endregion
            
            return results.ToArray();
        }
        ///<summary></summary>
        protected override void SetIdentity(object value)
        {
        		m_Current.Id = Convert.ToInt32(value);
      }
        ///<summary></summary>
        protected override bool HasIdentity
        {
            get {return true;}
        }

        #endregion
        
        #region UPDATE
        ///<summary></summary>
        protected override string GenerateSqlUpdate(int context)
        {
            StringBuilder sql = new StringBuilder();
            List<string> fields = new List<string>();
            
        
            // run update only
            #region Check each field to update
            sql.Append("UPDATE web_statistics SET ");
            if (m_Current.UserHostAddress != m_Original.UserHostAddress)
                fields.Add(String.Format("UserHostAddress=@UserHostAddress_{0}", context));
            if (m_Current.Browser != m_Original.Browser)
                fields.Add(String.Format("Browser=@Browser_{0}", context));
            if (m_Current.BrowserVersion != m_Original.BrowserVersion)
                fields.Add(String.Format("BrowserVersion=@BrowserVersion_{0}", context));
            if (m_Current.Url != m_Original.Url)
                fields.Add(String.Format("Url=@Url_{0}", context));
            if (m_Current.UserAgent != m_Original.UserAgent)
                fields.Add(String.Format("UserAgent=@UserAgent_{0}", context));
            if (m_Current.AccessTime != m_Original.AccessTime)
                fields.Add(String.Format("AccessTime=@AccessTime_{0}", context));
            if (fields.Count == 0) return "";
            
            sql.Append(String.Join(",", fields.ToArray()));
            // primary key where clause
            sql.Append(GenerateWhereClausePrimaryKey(context));
            #endregion
        

            return sql.ToString();
        }
        ///<summary></summary>
        protected override SqlParameter[] GenerateSqlParametersUpdate(int context)
        {
            List<SqlParameter> results = new List<SqlParameter>();

        
            // run update only
            #region Check each field to update
            if (m_Current.UserHostAddress != m_Original.UserHostAddress)
                results.Add(CreateParameter(String.Format("@UserHostAddress_{0}", context), SqlDbType.VarChar, m_Current.UserHostAddress));
            if (m_Current.Browser != m_Original.Browser)
                results.Add(CreateParameter(String.Format("@Browser_{0}", context), SqlDbType.VarChar, m_Current.Browser));
            if (m_Current.BrowserVersion != m_Original.BrowserVersion)
                results.Add(CreateParameter(String.Format("@BrowserVersion_{0}", context), SqlDbType.VarChar, m_Current.BrowserVersion));
            if (m_Current.Url != m_Original.Url)
                results.Add(CreateParameter(String.Format("@Url_{0}", context), SqlDbType.VarChar, m_Current.Url));
            if (m_Current.UserAgent != m_Original.UserAgent)
                results.Add(CreateParameter(String.Format("@UserAgent_{0}", context), SqlDbType.VarChar, m_Current.UserAgent));
            if (m_Current.AccessTime != m_Original.AccessTime)
                results.Add(CreateParameter(String.Format("@AccessTime_{0}", context), SqlDbType.DateTime, m_Current.AccessTime));
            results.AddRange(GenerateSqlParametersPrimaryKey(context));
            #endregion
        
            return results.ToArray();
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Generates a parameterized sql string to delete a row by the primary key of the table
        /// </summary>
        public override string GenerateSqlDelete(int context)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM web_statistics");
            sql.Append(GenerateWhereClausePrimaryKey(context));
            
            return sql.ToString();        
        }
        #endregion
        
        #endregion   

        #region State Management
        /// <summary>
        /// Returns an instance of a class that represents the data in the table.
        /// </summary>
        public WebStatisticsData GetCurrentValues()
        {
        	
            return (WebStatisticsData)m_Current.Clone();
        }

        /// <summary>
        /// Sets the properties of the object from an instance of the data class.
        /// </summary>
        public void SetCurrentValues(WebStatisticsData data)
        {
            // set all fields
            UserHostAddress = data.UserHostAddress;
            Browser = data.Browser;
            BrowserVersion = data.BrowserVersion;
            Url = data.Url;
            UserAgent = data.UserAgent;
            AccessTime = data.AccessTime;
        }

        /// <summary>
        /// Returns an instance of the original values
        /// </summary>
        public WebStatisticsData GetOriginalValues()
        {
            object result = null;
            if (m_Original != null) result = m_Original.Clone();
            return (WebStatisticsData)result;
        }

        /// <summary>
        /// Sets/Saves the current values to the original values class
        /// </summary>
        public override void SetOriginalValues()
        {
            m_Original = (WebStatisticsData)m_Current.Clone();
        }

        #endregion

        #region Changes List
        /// <summary>
        /// Get field changes by comparing new value to old value, build list of changes.
        /// </summary>
        public ChangesList FieldChanges
        {
            get
            {
                ChangesList result = new ChangesList();

                #region Compare Values

                if (m_Original != null)
                {                    
                    Change change = null;
                    
                    // Test for changes for the Id field
                    if (m_Current.Id != m_Original.Id)
                    {
                        change = new Change("Id");
                        change.NewValue = m_Current.Id != null ? m_Current.Id.ToString() : null;
                        change.OldValue = m_Original.Id != null ? m_Original.Id.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the UserHostAddress field
                    if (m_Current.UserHostAddress != m_Original.UserHostAddress)
                    {
                        change = new Change("UserHostAddress");
                        change.NewValue = m_Current.UserHostAddress != null ? m_Current.UserHostAddress.ToString() : null;
                        change.OldValue = m_Original.UserHostAddress != null ? m_Original.UserHostAddress.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the Browser field
                    if (m_Current.Browser != m_Original.Browser)
                    {
                        change = new Change("Browser");
                        change.NewValue = m_Current.Browser != null ? m_Current.Browser.ToString() : null;
                        change.OldValue = m_Original.Browser != null ? m_Original.Browser.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the BrowserVersion field
                    if (m_Current.BrowserVersion != m_Original.BrowserVersion)
                    {
                        change = new Change("BrowserVersion");
                        change.NewValue = m_Current.BrowserVersion != null ? m_Current.BrowserVersion.ToString() : null;
                        change.OldValue = m_Original.BrowserVersion != null ? m_Original.BrowserVersion.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the Url field
                    if (m_Current.Url != m_Original.Url)
                    {
                        change = new Change("Url");
                        change.NewValue = m_Current.Url != null ? m_Current.Url.ToString() : null;
                        change.OldValue = m_Original.Url != null ? m_Original.Url.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the UserAgent field
                    if (m_Current.UserAgent != m_Original.UserAgent)
                    {
                        change = new Change("UserAgent");
                        change.NewValue = m_Current.UserAgent != null ? m_Current.UserAgent.ToString() : null;
                        change.OldValue = m_Original.UserAgent != null ? m_Original.UserAgent.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the AccessTime field
                    if (m_Current.AccessTime != m_Original.AccessTime)
                    {
                        change = new Change("AccessTime");
                        change.NewValue = m_Current.AccessTime != null ? m_Current.AccessTime.ToString() : null;
                        change.OldValue = m_Original.AccessTime != null ? m_Original.AccessTime.ToString() : null;
                        result.Add(change);
                    }
        
                }
                #endregion

                return result;
            }
        }
        
        #endregion
        
        #region Changedby

        #endregion

    }
    
    #endregion
    
    #region List Class
    /// <summary>
    /// Manages a list of WebStatistics objects
    /// </summary>
    public partial class WebStatisticsList : AbstractTableList
    {
        #region Private Fields
        // Data structure to hold objects
        private List<WebStatistics> m_Items = new List<WebStatistics>();
        
        // Comparison<T> delegate for sorting purposes
        Comparison<WebStatistics> m_Comparison = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the list class and leaves the list empty
        /// </summary>
        public WebStatisticsList (SqlHelper db) : base(db) {}
        #endregion
        
        #region Public Properties
        /// <summary>
        /// Retrieves the Count of items in the list
        /// </summary>
        public override int Count
        {
            get
            {
                return m_Items.Count;
            }
        }

        /// <summary>
        /// Retrieves the list of items to iterate through
        /// </summary>
        public IList<WebStatistics> Items
        {
            get
            {
                return m_Items;
            }
        }

        ///<summary></summary>
        public override IList<AbstractTable> TableItems
        {
            get
            {
                List<AbstractTable> results = new List<AbstractTable>();
                WebStatistics[] list = new WebStatistics[m_Items.Count];
                m_Items.CopyTo(list, 0);
                results.AddRange(list);
                return results;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds an item to the list
        /// </summary>
        public void Add(WebStatistics item)
        {
            m_Items.Add(item);

            // Do a sort if we have a comparison delegate
            if ( m_Comparison != null )
            {
                Sort(m_Comparison);
            }
        }

        /// <summary>
        /// Loads the list class based on provided sql and commands.
        /// </summary>
        public void Populate(string sql, SqlParameter[] commandParameters)
        {
            m_Items.Clear();

            using (SqlDataReader dr = m_db.ExecuteReader(System.Data.CommandType.Text, sql, commandParameters))
            {
                while (dr.Read())
                {
                    WebStatistics item = new WebStatistics(m_db, dr);
                    this.Add(item);
                }
            }

            // After populating, call the default sort method
            Sort();
        }
        
        /// <summary>
        /// Sort by custom comparison delegate
        /// </summary>
        public void Sort(Comparison<WebStatistics> comparison)
        {
            // Only sort if our delegate has changed
            if ( comparison != m_Comparison )
            {
                m_Items.Sort(comparison);
                
                // Save a reference to the delegate 
                m_Comparison = comparison;
            }
        }

        /// <summary>
        /// Finds class by primary key
        /// </summary>
        public WebStatistics Find(int id)
        {
            WebStatistics result = null;
            WebStatisticsPrimaryKey key = new WebStatisticsPrimaryKey();
			key.Id = id;

            
            // Loop through our list until we find the match
            foreach (WebStatistics item in m_Items)
            {
                if (item.PrimaryKey.CompareTo(key) == 0)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Removes class from list by primary key values
        /// </summary>
        public bool Remove( int id)
        {
            bool result = false;
            WebStatisticsPrimaryKey key = new WebStatisticsPrimaryKey();
			key.Id = id;


            // Loop through our list until we find the match
            for (int i = 0; i < m_Items.Count; i++)
            {
                if (m_Items[i].PrimaryKey.CompareTo(key) == 0)
                {
                    m_Items.RemoveAt(i);
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Removes the class passed in by object reference
        /// </summary>
        public bool Remove(WebStatistics item)
        {
            return m_Items.Remove(item);
        }

        ///<summary></summary>
        public WebStatisticsData[] GetCurrentValues()
        {
            List<WebStatisticsData> results = new List<WebStatisticsData>();
            foreach (WebStatistics item in m_Items)
            {
                results.Add(item.GetCurrentValues());
            }
            return results.ToArray();
        }
        #endregion
    }
    #endregion
    
    #region Primary Key
    /// <summary>
    /// Class used to represent the primary key fields in the web_statistics table
    /// </summary>
    public class WebStatisticsPrimaryKey : IComparable<WebStatisticsPrimaryKey> 
    {
		///<summary></summary>
		public int Id;
  	
        #region IComparable<CDMCodegenPrimaryKey> Members
        ///<summary></summary>
        public int CompareTo(WebStatisticsPrimaryKey other)
        {
            int result = 0;
            if (other != null)
            {
                        if (this.Id.CompareTo(other.Id) != 0)
                            result = this.Id.CompareTo(other.Id);
            }
            // By definition, an object always compares greater than NULL
            else
            {
                result = 1;
            }
            return result;
        }
        #endregion
    }
    #endregion

    #region SortedList Class
    /// <summary>
    /// Manages a Sorted list of WebStatistics objects
    /// </summary>
    public partial class WebStatisticsSortedList : AbstractTableList
    {
        private SortedList<WebStatisticsPrimaryKey, WebStatistics> m_Items = new SortedList<WebStatisticsPrimaryKey, WebStatistics>();

        /// <summary>
        /// Creates an instance of the list class and leaves the list empty
        /// </summary>
        public WebStatisticsSortedList (SqlHelper db) : base(db) {}

        /// <summary>
        /// Retrieves the Count of items in the list
        /// </summary>
        public override int Count
        {
            get
            {
                return m_Items.Count;
            }
        }

        /// <summary>
        /// Retrieves the list of items to iterate through
        /// </summary>
        public IList<WebStatistics> Items
        {
            get
            {
                return m_Items.Values;
            }
        }
        
        ///<summary></summary>
        public override IList<AbstractTable> TableItems
        {
            get
            {
                List<AbstractTable> results = new List<AbstractTable>();
                WebStatistics[] list = new WebStatistics[m_Items.Count];
                m_Items.Values.CopyTo(list, 0);
                results.AddRange(list);
                return results;
            }
        }

        /// <summary>
        /// Finds class by primary key
        /// </summary>
        public WebStatistics Find(int id)
        {
            WebStatistics result = null;
            WebStatisticsPrimaryKey key = new WebStatisticsPrimaryKey();
			key.Id = id;
  			
            if (m_Items.ContainsKey(key))
            {
                result = m_Items[key];
            }
            return result;
        }

        /// <summary>
        /// Removes class from list by primary key
        /// </summary>
        public bool Remove( int id)
        {
            bool result = false;
            WebStatisticsPrimaryKey key = new WebStatisticsPrimaryKey();
			key.Id = id;
  			
            if (m_Items.ContainsKey(key))
            {
                result = Remove(m_Items[key]);
            }
            return result;
        }

        /// <summary>
        /// Removes the class passed in by primary key
        /// </summary>
        public bool Remove(WebStatistics item)
        {
            bool result = false;
            result = m_Items.Remove(item.PrimaryKey);
            return result;
        }

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        public void Add(WebStatistics item)
        {
            m_Items.Add(item.PrimaryKey, item);
        }

        /// <summary>
        /// Loads the list class based on provided sql and commands.
        /// </summary>
        public void Populate(string sql, SqlParameter[] commandParameters)
        {
            m_Items.Clear();

            using (SqlDataReader dr = m_db.ExecuteReader(System.Data.CommandType.Text, sql, commandParameters))
            {
                while (dr.Read())
                {
                    WebStatistics item = new WebStatistics(m_db, dr);
                    this.Add(item);
                }
            }
        }
        
        ///<summary></summary>
        public WebStatisticsData[] GetCurrentValues()
        {
            List<WebStatisticsData> results = new List<WebStatisticsData>();
            foreach (WebStatistics item in m_Items.Values)
            {
                results.Add(item.GetCurrentValues());
            }
            return results.ToArray();
        }
    }
    #endregion

    #region Data Class
    /// <summary>
    /// Represents just the data in the web_statistics table.
    /// </summary>
    public partial class WebStatisticsData : ICloneable
    {
		///<summary></summary>
		public int? Id ; 
		///<summary></summary>
		public string UserHostAddress ; 
		///<summary></summary>
		public string Browser ; 
		///<summary></summary>
		public string BrowserVersion ; 
		///<summary></summary>
		public string Url ; 
		///<summary></summary>
		public string UserAgent ; 
		///<summary></summary>
		public DateTime? AccessTime ; 
		
        #region ICloneable Members
        /// <summary>
        /// Performs a "shallow" copy of this object.
        /// </summary>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion	
    }
    #endregion
}
