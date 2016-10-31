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
    /// Table Created : 5/25/2015 3:55:34 PM
    /// </summary>
    public partial class StandardSetting : AbstractTable
    {
        private StandardSettingData m_Current = new StandardSettingData();
        private StandardSettingData m_Original;
        
        public const string TABLE_NAME = "standard_setting";
        //private string TABLE_TYPE = "";
        //private bool HAS_LOGICAL_KEYS = false;
        
        #region Constructors
        
        /// <summary>
        /// Constructor used to create an instance of this table in AddMode.
        /// </summary>
        public StandardSetting(SqlHelper db) : base(db) {}

        /// <summary>
        /// Constructor used to create an instance of this table using an
        /// existing SqlDataReader.
        /// </summary>
        public StandardSetting(SqlHelper db, SqlDataReader dr) : base(db, dr) {}

        /// <summary>
        /// Constructor used to create an instance of this table using the
        /// primary keys of the table.
        /// </summary>		
        public StandardSetting(SqlHelper db, string code) 
            : this(db)
        {
			if (code != null)
			{
				m_Current.Code = code;

                Read();
            }
        }

        /// <summary>
        /// Constructor used to create an instance of this table using the
        /// provided data class.
        /// </summary>		
        public StandardSetting(SqlHelper db, StandardSettingData data)
            : this(db, data.Code)
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
                    case "code":
                        result = Code;
                        break;
                    case "value":
                        result = Value;
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
                        case "code":
                            if (value == null)
								Code = null;
                            else
								Code = Convert.ToString(value);
                            break;
                        case "value":
                            if (value == null)
								Value = null;
                            else
								Value = Convert.ToString(value);
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
        public StandardSettingPrimaryKey PrimaryKey
        {
            get
            {
                StandardSettingPrimaryKey key = new StandardSettingPrimaryKey();
				key.Code = m_Current.Code;
  			
                return key;
            }
        }
        /// <summary>
        /// Public property that represents the Code field.
        /// Description: 
        /// </summary>
        public string Code
        {
            get { return m_Current.Code; }
            set 
            {	
                if (value != m_Current.Code)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Code = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the Value field.
        /// Description: 
        /// </summary>
        public string Value
        {
            get { return m_Current.Value; }
            set 
            {	
                if (value != m_Current.Value)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Value = value;
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
                if (!dr.IsDBNull(dr.GetOrdinal("Code"))) m_Current.Code = dr.GetString(dr.GetOrdinal("Code"));
                if (!dr.IsDBNull(dr.GetOrdinal("Value"))) m_Current.Value = dr.GetString(dr.GetOrdinal("Value"));
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
            sql.Append("SELECT * FROM standard_setting WITH (NOLOCK) ");
            sql.Append(GenerateWhereClausePrimaryKey(context));
            return sql.ToString();
        }
		///<summary></summary>
        protected override string GenerateWhereClausePrimaryKey(int context)
        {
            StringBuilder sql = new StringBuilder();
            // primary key where clause
            sql.AppendFormat(" WHERE Code=@Code_{0}_key", context);
            return sql.ToString();
        }
        /// <summary>
        /// Generates an array of SqlParameters based on the primary key of the table
        /// </summary>
        public override SqlParameter[] GenerateSqlParametersPrimaryKey(int context)
        {
            List<SqlParameter> results = new List<SqlParameter>();
            results.Add(CreateParameter(String.Format("@Code_{0}_key", context), SqlDbType.VarChar, m_Current.Code));
            return results.ToArray();
        }
        #endregion
        
        #region INSERT
        ///<summary></summary>
        protected override string GenerateSqlInsert(int context)
        {
            StringBuilder result = new StringBuilder();
            List<string> fields = new List<string>();

            result.Append("INSERT INTO standard_setting (");

            #region Check each field to insert
            if (m_Current.Code != null) fields.Add("Code");
            if (m_Current.Value != null) fields.Add("Value");

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

            if (m_Current.Code != null)
                results.Add(CreateParameter(String.Format("@Code_{0}", context), SqlDbType.VarChar, m_Current.Code));
            if (m_Current.Value != null)
                results.Add(CreateParameter(String.Format("@Value_{0}", context), SqlDbType.VarChar, m_Current.Value));
            #endregion
            
            return results.ToArray();
        }
        ///<summary></summary>
        protected override void SetIdentity(object value)
        {
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
            sql.Append("UPDATE standard_setting SET ");
            if (m_Current.Code != m_Original.Code)
                fields.Add(String.Format("Code=@Code_{0}", context));
            if (m_Current.Value != m_Original.Value)
                fields.Add(String.Format("Value=@Value_{0}", context));
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
            if (m_Current.Code != m_Original.Code)
                results.Add(CreateParameter(String.Format("@Code_{0}", context), SqlDbType.VarChar, m_Current.Code));
            if (m_Current.Value != m_Original.Value)
                results.Add(CreateParameter(String.Format("@Value_{0}", context), SqlDbType.VarChar, m_Current.Value));
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
            sql.Append("DELETE FROM standard_setting");
            sql.Append(GenerateWhereClausePrimaryKey(context));
            
            return sql.ToString();        
        }
        #endregion
        
        #endregion   

        #region State Management
        /// <summary>
        /// Returns an instance of a class that represents the data in the table.
        /// </summary>
        public StandardSettingData GetCurrentValues()
        {
        	
            return (StandardSettingData)m_Current.Clone();
        }

        /// <summary>
        /// Sets the properties of the object from an instance of the data class.
        /// </summary>
        public void SetCurrentValues(StandardSettingData data)
        {
            // set all fields
            Code = data.Code;
            Value = data.Value;
        }

        /// <summary>
        /// Returns an instance of the original values
        /// </summary>
        public StandardSettingData GetOriginalValues()
        {
            object result = null;
            if (m_Original != null) result = m_Original.Clone();
            return (StandardSettingData)result;
        }

        /// <summary>
        /// Sets/Saves the current values to the original values class
        /// </summary>
        public override void SetOriginalValues()
        {
            m_Original = (StandardSettingData)m_Current.Clone();
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
                    
                    // Test for changes for the Code field
                    if (m_Current.Code != m_Original.Code)
                    {
                        change = new Change("Code");
                        change.NewValue = m_Current.Code != null ? m_Current.Code.ToString() : null;
                        change.OldValue = m_Original.Code != null ? m_Original.Code.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the Value field
                    if (m_Current.Value != m_Original.Value)
                    {
                        change = new Change("Value");
                        change.NewValue = m_Current.Value != null ? m_Current.Value.ToString() : null;
                        change.OldValue = m_Original.Value != null ? m_Original.Value.ToString() : null;
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
    /// Manages a list of StandardSetting objects
    /// </summary>
    public partial class StandardSettingList : AbstractTableList
    {
        #region Private Fields
        // Data structure to hold objects
        private List<StandardSetting> m_Items = new List<StandardSetting>();
        
        // Comparison<T> delegate for sorting purposes
        Comparison<StandardSetting> m_Comparison = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the list class and leaves the list empty
        /// </summary>
        public StandardSettingList (SqlHelper db) : base(db) {}
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
        public IList<StandardSetting> Items
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
                StandardSetting[] list = new StandardSetting[m_Items.Count];
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
        public void Add(StandardSetting item)
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
                    StandardSetting item = new StandardSetting(m_db, dr);
                    this.Add(item);
                }
            }

            // After populating, call the default sort method
            Sort();
        }
        
        /// <summary>
        /// Sort by custom comparison delegate
        /// </summary>
        public void Sort(Comparison<StandardSetting> comparison)
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
        public StandardSetting Find(string code)
        {
            StandardSetting result = null;
            StandardSettingPrimaryKey key = new StandardSettingPrimaryKey();
			key.Code = code;

            
            // Loop through our list until we find the match
            foreach (StandardSetting item in m_Items)
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
        public bool Remove( string code)
        {
            bool result = false;
            StandardSettingPrimaryKey key = new StandardSettingPrimaryKey();
			key.Code = code;


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
        public bool Remove(StandardSetting item)
        {
            return m_Items.Remove(item);
        }

        ///<summary></summary>
        public StandardSettingData[] GetCurrentValues()
        {
            List<StandardSettingData> results = new List<StandardSettingData>();
            foreach (StandardSetting item in m_Items)
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
    /// Class used to represent the primary key fields in the standard_setting table
    /// </summary>
    public class StandardSettingPrimaryKey : IComparable<StandardSettingPrimaryKey> 
    {
		///<summary></summary>
		public string Code;
  	
        #region IComparable<CDMCodegenPrimaryKey> Members
        ///<summary></summary>
        public int CompareTo(StandardSettingPrimaryKey other)
        {
            int result = 0;
            if (other != null)
            {
                        if (this.Code.CompareTo(other.Code) != 0)
                            result = this.Code.CompareTo(other.Code);
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
    /// Manages a Sorted list of StandardSetting objects
    /// </summary>
    public partial class StandardSettingSortedList : AbstractTableList
    {
        private SortedList<StandardSettingPrimaryKey, StandardSetting> m_Items = new SortedList<StandardSettingPrimaryKey, StandardSetting>();

        /// <summary>
        /// Creates an instance of the list class and leaves the list empty
        /// </summary>
        public StandardSettingSortedList (SqlHelper db) : base(db) {}

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
        public IList<StandardSetting> Items
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
                StandardSetting[] list = new StandardSetting[m_Items.Count];
                m_Items.Values.CopyTo(list, 0);
                results.AddRange(list);
                return results;
            }
        }

        /// <summary>
        /// Finds class by primary key
        /// </summary>
        public StandardSetting Find(string code)
        {
            StandardSetting result = null;
            StandardSettingPrimaryKey key = new StandardSettingPrimaryKey();
			key.Code = code;
  			
            if (m_Items.ContainsKey(key))
            {
                result = m_Items[key];
            }
            return result;
        }

        /// <summary>
        /// Removes class from list by primary key
        /// </summary>
        public bool Remove( string code)
        {
            bool result = false;
            StandardSettingPrimaryKey key = new StandardSettingPrimaryKey();
			key.Code = code;
  			
            if (m_Items.ContainsKey(key))
            {
                result = Remove(m_Items[key]);
            }
            return result;
        }

        /// <summary>
        /// Removes the class passed in by primary key
        /// </summary>
        public bool Remove(StandardSetting item)
        {
            bool result = false;
            result = m_Items.Remove(item.PrimaryKey);
            return result;
        }

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        public void Add(StandardSetting item)
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
                    StandardSetting item = new StandardSetting(m_db, dr);
                    this.Add(item);
                }
            }
        }
        
        ///<summary></summary>
        public StandardSettingData[] GetCurrentValues()
        {
            List<StandardSettingData> results = new List<StandardSettingData>();
            foreach (StandardSetting item in m_Items.Values)
            {
                results.Add(item.GetCurrentValues());
            }
            return results.ToArray();
        }
    }
    #endregion

    #region Data Class
    /// <summary>
    /// Represents just the data in the standard_setting table.
    /// </summary>
    public partial class StandardSettingData : ICloneable
    {
		///<summary></summary>
		public string Code ; 
		///<summary></summary>
		public string Value ; 
		
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
