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
    /// Table Created : 5/25/2015 4:08:41 PM
    /// </summary>
    public partial class BlogTag : AbstractTable
    {
        private BlogTagData m_Current = new BlogTagData();
        private BlogTagData m_Original;
        
        public const string TABLE_NAME = "blog_tag";
        //private string TABLE_TYPE = "";
        //private bool HAS_LOGICAL_KEYS = false;
        
        #region Constructors
        
        /// <summary>
        /// Constructor used to create an instance of this table in AddMode.
        /// </summary>
        public BlogTag(SqlHelper db) : base(db) {}

        /// <summary>
        /// Constructor used to create an instance of this table using an
        /// existing SqlDataReader.
        /// </summary>
        public BlogTag(SqlHelper db, SqlDataReader dr) : base(db, dr) {}

        /// <summary>
        /// Constructor used to create an instance of this table using the
        /// primary keys of the table.
        /// </summary>		
        public BlogTag(SqlHelper db, int? id) 
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
        public BlogTag(SqlHelper db, BlogTagData data)
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
                    case "name":
                        result = Name;
                        break;
                    case "urlslug":
                        result = UrlSlug;
                        break;
                    case "description":
                        result = Description;
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
                            if (value == null)
								Id = null;
                            else
								Id = Convert.ToInt32(value);
                            break;
                        case "name":
                            if (value == null)
								Name = null;
                            else
								Name = Convert.ToString(value);
                            break;
                        case "urlslug":
                            if (value == null)
								UrlSlug = null;
                            else
								UrlSlug = Convert.ToString(value);
                            break;
                        case "description":
                            if (value == null)
								Description = null;
                            else
								Description = Convert.ToString(value);
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
        public BlogTagPrimaryKey PrimaryKey
        {
            get
            {
                BlogTagPrimaryKey key = new BlogTagPrimaryKey();
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
            set 
            {	
                if (value != m_Current.Id)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Id = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the Name field.
        /// Description: 
        /// </summary>
        public string Name
        {
            get { return m_Current.Name; }
            set 
            {	
                if (value != m_Current.Name)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Name = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the UrlSlug field.
        /// Description: 
        /// </summary>
        public string UrlSlug
        {
            get { return m_Current.UrlSlug; }
            set 
            {	
                if (value != m_Current.UrlSlug)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.UrlSlug = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the Description field.
        /// Description: 
        /// </summary>
        public string Description
        {
            get { return m_Current.Description; }
            set 
            {	
                if (value != m_Current.Description)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Description = value;
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
                if (!dr.IsDBNull(dr.GetOrdinal("Name"))) m_Current.Name = dr.GetString(dr.GetOrdinal("Name"));
                if (!dr.IsDBNull(dr.GetOrdinal("UrlSlug"))) m_Current.UrlSlug = dr.GetString(dr.GetOrdinal("UrlSlug"));
                if (!dr.IsDBNull(dr.GetOrdinal("Description"))) m_Current.Description = dr.GetString(dr.GetOrdinal("Description"));
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
            sql.Append("SELECT * FROM blog_tag WITH (NOLOCK) ");
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

            result.Append("INSERT INTO blog_tag (");

            #region Check each field to insert
            if (m_Current.Id != null) fields.Add("Id");
            if (m_Current.Name != null) fields.Add("Name");
            if (m_Current.UrlSlug != null) fields.Add("UrlSlug");
            if (m_Current.Description != null) fields.Add("Description");

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

            if (m_Current.Id != null)
                results.Add(CreateParameter(String.Format("@Id_{0}", context), SqlDbType.Int, m_Current.Id));
            if (m_Current.Name != null)
                results.Add(CreateParameter(String.Format("@Name_{0}", context), SqlDbType.VarChar, m_Current.Name));
            if (m_Current.UrlSlug != null)
                results.Add(CreateParameter(String.Format("@UrlSlug_{0}", context), SqlDbType.VarChar, m_Current.UrlSlug));
            if (m_Current.Description != null)
                results.Add(CreateParameter(String.Format("@Description_{0}", context), SqlDbType.VarChar, m_Current.Description));
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
            sql.Append("UPDATE blog_tag SET ");
            if (m_Current.Id != m_Original.Id)
                fields.Add(String.Format("Id=@Id_{0}", context));
            if (m_Current.Name != m_Original.Name)
                fields.Add(String.Format("Name=@Name_{0}", context));
            if (m_Current.UrlSlug != m_Original.UrlSlug)
                fields.Add(String.Format("UrlSlug=@UrlSlug_{0}", context));
            if (m_Current.Description != m_Original.Description)
                fields.Add(String.Format("Description=@Description_{0}", context));
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
            if (m_Current.Id != m_Original.Id)
                results.Add(CreateParameter(String.Format("@Id_{0}", context), SqlDbType.Int, m_Current.Id));
            if (m_Current.Name != m_Original.Name)
                results.Add(CreateParameter(String.Format("@Name_{0}", context), SqlDbType.VarChar, m_Current.Name));
            if (m_Current.UrlSlug != m_Original.UrlSlug)
                results.Add(CreateParameter(String.Format("@UrlSlug_{0}", context), SqlDbType.VarChar, m_Current.UrlSlug));
            if (m_Current.Description != m_Original.Description)
                results.Add(CreateParameter(String.Format("@Description_{0}", context), SqlDbType.VarChar, m_Current.Description));
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
            sql.Append("DELETE FROM blog_tag");
            sql.Append(GenerateWhereClausePrimaryKey(context));
            
            return sql.ToString();        
        }
        #endregion
        
        #endregion   

        #region State Management
        /// <summary>
        /// Returns an instance of a class that represents the data in the table.
        /// </summary>
        public BlogTagData GetCurrentValues()
        {
        	
            return (BlogTagData)m_Current.Clone();
        }

        /// <summary>
        /// Sets the properties of the object from an instance of the data class.
        /// </summary>
        public void SetCurrentValues(BlogTagData data)
        {
            // set all fields
            Id = data.Id;
            Name = data.Name;
            UrlSlug = data.UrlSlug;
            Description = data.Description;
        }

        /// <summary>
        /// Returns an instance of the original values
        /// </summary>
        public BlogTagData GetOriginalValues()
        {
            object result = null;
            if (m_Original != null) result = m_Original.Clone();
            return (BlogTagData)result;
        }

        /// <summary>
        /// Sets/Saves the current values to the original values class
        /// </summary>
        public override void SetOriginalValues()
        {
            m_Original = (BlogTagData)m_Current.Clone();
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
                    // Test for changes for the Name field
                    if (m_Current.Name != m_Original.Name)
                    {
                        change = new Change("Name");
                        change.NewValue = m_Current.Name != null ? m_Current.Name.ToString() : null;
                        change.OldValue = m_Original.Name != null ? m_Original.Name.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the UrlSlug field
                    if (m_Current.UrlSlug != m_Original.UrlSlug)
                    {
                        change = new Change("UrlSlug");
                        change.NewValue = m_Current.UrlSlug != null ? m_Current.UrlSlug.ToString() : null;
                        change.OldValue = m_Original.UrlSlug != null ? m_Original.UrlSlug.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the Description field
                    if (m_Current.Description != m_Original.Description)
                    {
                        change = new Change("Description");
                        change.NewValue = m_Current.Description != null ? m_Current.Description.ToString() : null;
                        change.OldValue = m_Original.Description != null ? m_Original.Description.ToString() : null;
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
    /// Manages a list of BlogTag objects
    /// </summary>
    public partial class BlogTagList : AbstractTableList
    {
        #region Private Fields
        // Data structure to hold objects
        private List<BlogTag> m_Items = new List<BlogTag>();
        
        // Comparison<T> delegate for sorting purposes
        Comparison<BlogTag> m_Comparison = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the list class and leaves the list empty
        /// </summary>
        public BlogTagList (SqlHelper db) : base(db) {}
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
        public IList<BlogTag> Items
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
                BlogTag[] list = new BlogTag[m_Items.Count];
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
        public void Add(BlogTag item)
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
                    BlogTag item = new BlogTag(m_db, dr);
                    this.Add(item);
                }
            }

            // After populating, call the default sort method
            Sort();
        }
        
        /// <summary>
        /// Sort by custom comparison delegate
        /// </summary>
        public void Sort(Comparison<BlogTag> comparison)
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
        public BlogTag Find(int id)
        {
            BlogTag result = null;
            BlogTagPrimaryKey key = new BlogTagPrimaryKey();
			key.Id = id;

            
            // Loop through our list until we find the match
            foreach (BlogTag item in m_Items)
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
            BlogTagPrimaryKey key = new BlogTagPrimaryKey();
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
        public bool Remove(BlogTag item)
        {
            return m_Items.Remove(item);
        }

        ///<summary></summary>
        public BlogTagData[] GetCurrentValues()
        {
            List<BlogTagData> results = new List<BlogTagData>();
            foreach (BlogTag item in m_Items)
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
    /// Class used to represent the primary key fields in the blog_tag table
    /// </summary>
    public class BlogTagPrimaryKey : IComparable<BlogTagPrimaryKey> 
    {
		///<summary></summary>
		public int Id;
  	
        #region IComparable<CDMCodegenPrimaryKey> Members
        ///<summary></summary>
        public int CompareTo(BlogTagPrimaryKey other)
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
    /// Manages a Sorted list of BlogTag objects
    /// </summary>
    public partial class BlogTagSortedList : AbstractTableList
    {
        private SortedList<BlogTagPrimaryKey, BlogTag> m_Items = new SortedList<BlogTagPrimaryKey, BlogTag>();

        /// <summary>
        /// Creates an instance of the list class and leaves the list empty
        /// </summary>
        public BlogTagSortedList (SqlHelper db) : base(db) {}

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
        public IList<BlogTag> Items
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
                BlogTag[] list = new BlogTag[m_Items.Count];
                m_Items.Values.CopyTo(list, 0);
                results.AddRange(list);
                return results;
            }
        }

        /// <summary>
        /// Finds class by primary key
        /// </summary>
        public BlogTag Find(int id)
        {
            BlogTag result = null;
            BlogTagPrimaryKey key = new BlogTagPrimaryKey();
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
            BlogTagPrimaryKey key = new BlogTagPrimaryKey();
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
        public bool Remove(BlogTag item)
        {
            bool result = false;
            result = m_Items.Remove(item.PrimaryKey);
            return result;
        }

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        public void Add(BlogTag item)
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
                    BlogTag item = new BlogTag(m_db, dr);
                    this.Add(item);
                }
            }
        }
        
        ///<summary></summary>
        public BlogTagData[] GetCurrentValues()
        {
            List<BlogTagData> results = new List<BlogTagData>();
            foreach (BlogTag item in m_Items.Values)
            {
                results.Add(item.GetCurrentValues());
            }
            return results.ToArray();
        }
    }
    #endregion

    #region Data Class
    /// <summary>
    /// Represents just the data in the blog_tag table.
    /// </summary>
    public partial class BlogTagData : ICloneable
    {
		///<summary></summary>
		public int? Id ; 
		///<summary></summary>
		public string Name ; 
		///<summary></summary>
		public string UrlSlug ; 
		///<summary></summary>
		public string Description ; 
		
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
