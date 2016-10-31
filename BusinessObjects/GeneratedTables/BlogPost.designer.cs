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
    /// Table Created : 5/25/2015 6:59:03 PM
    /// </summary>
    public partial class BlogPost : AbstractTable
    {
        private BlogPostData m_Current = new BlogPostData();
        private BlogPostData m_Original;
        
        public const string TABLE_NAME = "blog_post";
        //private string TABLE_TYPE = "";
        //private bool HAS_LOGICAL_KEYS = false;
        
        #region Constructors
        
        /// <summary>
        /// Constructor used to create an instance of this table in AddMode.
        /// </summary>
        public BlogPost(SqlHelper db) : base(db) {}

        /// <summary>
        /// Constructor used to create an instance of this table using an
        /// existing SqlDataReader.
        /// </summary>
        public BlogPost(SqlHelper db, SqlDataReader dr) : base(db, dr) {}

        /// <summary>
        /// Constructor used to create an instance of this table using the
        /// primary keys of the table.
        /// </summary>		
        public BlogPost(SqlHelper db, int? id) 
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
        public BlogPost(SqlHelper db, BlogPostData data)
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
                    case "title":
                        result = Title;
                        break;
                    case "body":
                        result = Body;
                        break;
                    case "shortdesc":
                        result = ShortDesc;
                        break;
                    case "description":
                        result = Description;
                        break;
                    case "meta":
                        result = Meta;
                        break;
                    case "urlslug":
                        result = UrlSlug;
                        break;
                    case "published":
                        result = Published;
                        break;
                    case "postedon":
                        result = PostedOn;
                        break;
                    case "modified":
                        result = Modified;
                        break;
                    case "category":
                        result = Category;
                        break;
                    case "enabled":
                        result = Enabled;
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
                        case "title":
                            if (value == null)
								Title = null;
                            else
								Title = Convert.ToString(value);
                            break;
                        case "body":
                            if (value == null)
								Body = null;
                            else
								Body = Convert.ToString(value);
                            break;
                        case "shortdesc":
                            if (value == null)
								ShortDesc = null;
                            else
								ShortDesc = Convert.ToString(value);
                            break;
                        case "description":
                            if (value == null)
								Description = null;
                            else
								Description = Convert.ToString(value);
                            break;
                        case "meta":
                            if (value == null)
								Meta = null;
                            else
								Meta = Convert.ToString(value);
                            break;
                        case "urlslug":
                            if (value == null)
								UrlSlug = null;
                            else
								UrlSlug = Convert.ToString(value);
                            break;
                        case "published":
                            if (value == null)
								Published = null;
                            else
								Published = Convert.ToBoolean(value);
                            break;
                        case "postedon":
                            if (value == null)
								PostedOn = null;
                            else
								PostedOn = Convert.ToDateTime(value);
                            break;
                        case "modified":
                            if (value == null)
								Modified = null;
                            else
								Modified = Convert.ToDateTime(value);
                            break;
                        case "category":
                            if (value == null)
								Category = null;
                            else
								Category = Convert.ToString(value);
                            break;
                        case "enabled":
                            if (value == null)
								Enabled = null;
                            else
								Enabled = Convert.ToBoolean(value);
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
        public BlogPostPrimaryKey PrimaryKey
        {
            get
            {
                BlogPostPrimaryKey key = new BlogPostPrimaryKey();
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
        /// Public property that represents the Title field.
        /// Description: 
        /// </summary>
        public string Title
        {
            get { return m_Current.Title; }
            set 
            {	
                if (value != m_Current.Title)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Title = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the Body field.
        /// Description: 
        /// </summary>
        public string Body
        {
            get { return m_Current.Body; }
            set 
            {	
                if (value != m_Current.Body)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Body = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the ShortDesc field.
        /// Description: 
        /// </summary>
        public string ShortDesc
        {
            get { return m_Current.ShortDesc; }
            set 
            {	
                if (value != m_Current.ShortDesc)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.ShortDesc = value;
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
        /// <summary>
        /// Public property that represents the Meta field.
        /// Description: 
        /// </summary>
        public string Meta
        {
            get { return m_Current.Meta; }
            set 
            {	
                if (value != m_Current.Meta)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Meta = value;
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
        /// Public property that represents the Published field.
        /// Description: 
        /// </summary>
        public bool? Published
        {
            get { return m_Current.Published; }
            set 
            {	
                if (value != m_Current.Published)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Published = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the PostedOn field.
        /// Description: 
        /// </summary>
        public DateTime? PostedOn
        {
            get { return m_Current.PostedOn; }
            set 
            {	
                if (value != m_Current.PostedOn)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.PostedOn = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the Modified field.
        /// Description: 
        /// </summary>
        public DateTime? Modified
        {
            get { return m_Current.Modified; }
            set 
            {	
                if (value != m_Current.Modified)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Modified = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the Category field.
        /// Description: 
        /// </summary>
        public string Category
        {
            get { return m_Current.Category; }
            set 
            {	
                if (value != m_Current.Category)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Category = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the Enabled field.
        /// Description: 
        /// </summary>
        public bool? Enabled
        {
            get { return m_Current.Enabled; }
            set 
            {	
                if (value != m_Current.Enabled)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Enabled = value;
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
                if (!dr.IsDBNull(dr.GetOrdinal("Title"))) m_Current.Title = dr.GetString(dr.GetOrdinal("Title"));
                if (!dr.IsDBNull(dr.GetOrdinal("Body"))) m_Current.Body = dr.GetString(dr.GetOrdinal("Body"));
                if (!dr.IsDBNull(dr.GetOrdinal("ShortDesc"))) m_Current.ShortDesc = dr.GetString(dr.GetOrdinal("ShortDesc"));
                if (!dr.IsDBNull(dr.GetOrdinal("Description"))) m_Current.Description = dr.GetString(dr.GetOrdinal("Description"));
                if (!dr.IsDBNull(dr.GetOrdinal("Meta"))) m_Current.Meta = dr.GetString(dr.GetOrdinal("Meta"));
                if (!dr.IsDBNull(dr.GetOrdinal("UrlSlug"))) m_Current.UrlSlug = dr.GetString(dr.GetOrdinal("UrlSlug"));
                if (!dr.IsDBNull(dr.GetOrdinal("Published"))) m_Current.Published = dr.GetBoolean(dr.GetOrdinal("Published"));
                if (!dr.IsDBNull(dr.GetOrdinal("PostedOn"))) m_Current.PostedOn = dr.GetDateTime(dr.GetOrdinal("PostedOn"));
                if (!dr.IsDBNull(dr.GetOrdinal("Modified"))) m_Current.Modified = dr.GetDateTime(dr.GetOrdinal("Modified"));
                if (!dr.IsDBNull(dr.GetOrdinal("Category"))) m_Current.Category = dr.GetString(dr.GetOrdinal("Category"));
                if (!dr.IsDBNull(dr.GetOrdinal("Enabled"))) m_Current.Enabled = dr.GetBoolean(dr.GetOrdinal("Enabled"));
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
            sql.Append("SELECT * FROM blog_post WITH (NOLOCK) ");
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

            result.Append("INSERT INTO blog_post (");

            #region Check each field to insert
            if (m_Current.Title != null) fields.Add("Title");
            if (m_Current.Body != null) fields.Add("Body");
            if (m_Current.ShortDesc != null) fields.Add("ShortDesc");
            if (m_Current.Description != null) fields.Add("Description");
            if (m_Current.Meta != null) fields.Add("Meta");
            if (m_Current.UrlSlug != null) fields.Add("UrlSlug");
            if (m_Current.Published != null) fields.Add("Published");
            if (m_Current.PostedOn != null) fields.Add("PostedOn");
            if (m_Current.Modified != null) fields.Add("Modified");
            if (m_Current.Category != null) fields.Add("Category");
            if (m_Current.Enabled != null) fields.Add("Enabled");

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

            if (m_Current.Title != null)
                results.Add(CreateParameter(String.Format("@Title_{0}", context), SqlDbType.VarChar, m_Current.Title));
            if (m_Current.Body != null)
                results.Add(CreateParameter(String.Format("@Body_{0}", context), SqlDbType.VarChar, m_Current.Body));
            if (m_Current.ShortDesc != null)
                results.Add(CreateParameter(String.Format("@ShortDesc_{0}", context), SqlDbType.VarChar, m_Current.ShortDesc));
            if (m_Current.Description != null)
                results.Add(CreateParameter(String.Format("@Description_{0}", context), SqlDbType.VarChar, m_Current.Description));
            if (m_Current.Meta != null)
                results.Add(CreateParameter(String.Format("@Meta_{0}", context), SqlDbType.VarChar, m_Current.Meta));
            if (m_Current.UrlSlug != null)
                results.Add(CreateParameter(String.Format("@UrlSlug_{0}", context), SqlDbType.VarChar, m_Current.UrlSlug));
            if (m_Current.Published != null)
                results.Add(CreateParameter(String.Format("@Published_{0}", context), SqlDbType.Bit, m_Current.Published));
            if (m_Current.PostedOn != null)
                results.Add(CreateParameter(String.Format("@PostedOn_{0}", context), SqlDbType.DateTime, m_Current.PostedOn));
            if (m_Current.Modified != null)
                results.Add(CreateParameter(String.Format("@Modified_{0}", context), SqlDbType.DateTime, m_Current.Modified));
            if (m_Current.Category != null)
                results.Add(CreateParameter(String.Format("@Category_{0}", context), SqlDbType.VarChar, m_Current.Category));
            if (m_Current.Enabled != null)
                results.Add(CreateParameter(String.Format("@Enabled_{0}", context), SqlDbType.Bit, m_Current.Enabled));
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
            sql.Append("UPDATE blog_post SET ");
            if (m_Current.Title != m_Original.Title)
                fields.Add(String.Format("Title=@Title_{0}", context));
            if (m_Current.Body != m_Original.Body)
                fields.Add(String.Format("Body=@Body_{0}", context));
            if (m_Current.ShortDesc != m_Original.ShortDesc)
                fields.Add(String.Format("ShortDesc=@ShortDesc_{0}", context));
            if (m_Current.Description != m_Original.Description)
                fields.Add(String.Format("Description=@Description_{0}", context));
            if (m_Current.Meta != m_Original.Meta)
                fields.Add(String.Format("Meta=@Meta_{0}", context));
            if (m_Current.UrlSlug != m_Original.UrlSlug)
                fields.Add(String.Format("UrlSlug=@UrlSlug_{0}", context));
            if (m_Current.Published != m_Original.Published)
                fields.Add(String.Format("Published=@Published_{0}", context));
            if (m_Current.PostedOn != m_Original.PostedOn)
                fields.Add(String.Format("PostedOn=@PostedOn_{0}", context));
            if (m_Current.Modified != m_Original.Modified)
                fields.Add(String.Format("Modified=@Modified_{0}", context));
            if (m_Current.Category != m_Original.Category)
                fields.Add(String.Format("Category=@Category_{0}", context));
            if (m_Current.Enabled != m_Original.Enabled)
                fields.Add(String.Format("Enabled=@Enabled_{0}", context));
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
            if (m_Current.Title != m_Original.Title)
                results.Add(CreateParameter(String.Format("@Title_{0}", context), SqlDbType.VarChar, m_Current.Title));
            if (m_Current.Body != m_Original.Body)
                results.Add(CreateParameter(String.Format("@Body_{0}", context), SqlDbType.VarChar, m_Current.Body));
            if (m_Current.ShortDesc != m_Original.ShortDesc)
                results.Add(CreateParameter(String.Format("@ShortDesc_{0}", context), SqlDbType.VarChar, m_Current.ShortDesc));
            if (m_Current.Description != m_Original.Description)
                results.Add(CreateParameter(String.Format("@Description_{0}", context), SqlDbType.VarChar, m_Current.Description));
            if (m_Current.Meta != m_Original.Meta)
                results.Add(CreateParameter(String.Format("@Meta_{0}", context), SqlDbType.VarChar, m_Current.Meta));
            if (m_Current.UrlSlug != m_Original.UrlSlug)
                results.Add(CreateParameter(String.Format("@UrlSlug_{0}", context), SqlDbType.VarChar, m_Current.UrlSlug));
            if (m_Current.Published != m_Original.Published)
                results.Add(CreateParameter(String.Format("@Published_{0}", context), SqlDbType.Bit, m_Current.Published));
            if (m_Current.PostedOn != m_Original.PostedOn)
                results.Add(CreateParameter(String.Format("@PostedOn_{0}", context), SqlDbType.DateTime, m_Current.PostedOn));
            if (m_Current.Modified != m_Original.Modified)
                results.Add(CreateParameter(String.Format("@Modified_{0}", context), SqlDbType.DateTime, m_Current.Modified));
            if (m_Current.Category != m_Original.Category)
                results.Add(CreateParameter(String.Format("@Category_{0}", context), SqlDbType.VarChar, m_Current.Category));
            if (m_Current.Enabled != m_Original.Enabled)
                results.Add(CreateParameter(String.Format("@Enabled_{0}", context), SqlDbType.Bit, m_Current.Enabled));
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
            sql.Append("DELETE FROM blog_post");
            sql.Append(GenerateWhereClausePrimaryKey(context));
            
            return sql.ToString();        
        }
        #endregion
        
        #endregion   

        #region State Management
        /// <summary>
        /// Returns an instance of a class that represents the data in the table.
        /// </summary>
        public BlogPostData GetCurrentValues()
        {
        	
            return (BlogPostData)m_Current.Clone();
        }

        /// <summary>
        /// Sets the properties of the object from an instance of the data class.
        /// </summary>
        public void SetCurrentValues(BlogPostData data)
        {
            // set all fields
            Title = data.Title;
            Body = data.Body;
            ShortDesc = data.ShortDesc;
            Description = data.Description;
            Meta = data.Meta;
            UrlSlug = data.UrlSlug;
            Published = data.Published;
            PostedOn = data.PostedOn;
            Modified = data.Modified;
            Category = data.Category;
            Enabled = data.Enabled;
        }

        /// <summary>
        /// Returns an instance of the original values
        /// </summary>
        public BlogPostData GetOriginalValues()
        {
            object result = null;
            if (m_Original != null) result = m_Original.Clone();
            return (BlogPostData)result;
        }

        /// <summary>
        /// Sets/Saves the current values to the original values class
        /// </summary>
        public override void SetOriginalValues()
        {
            m_Original = (BlogPostData)m_Current.Clone();
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
                    // Test for changes for the Title field
                    if (m_Current.Title != m_Original.Title)
                    {
                        change = new Change("Title");
                        change.NewValue = m_Current.Title != null ? m_Current.Title.ToString() : null;
                        change.OldValue = m_Original.Title != null ? m_Original.Title.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the Body field
                    if (m_Current.Body != m_Original.Body)
                    {
                        change = new Change("Body");
                        change.NewValue = m_Current.Body != null ? m_Current.Body.ToString() : null;
                        change.OldValue = m_Original.Body != null ? m_Original.Body.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the ShortDesc field
                    if (m_Current.ShortDesc != m_Original.ShortDesc)
                    {
                        change = new Change("ShortDesc");
                        change.NewValue = m_Current.ShortDesc != null ? m_Current.ShortDesc.ToString() : null;
                        change.OldValue = m_Original.ShortDesc != null ? m_Original.ShortDesc.ToString() : null;
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
                    // Test for changes for the Meta field
                    if (m_Current.Meta != m_Original.Meta)
                    {
                        change = new Change("Meta");
                        change.NewValue = m_Current.Meta != null ? m_Current.Meta.ToString() : null;
                        change.OldValue = m_Original.Meta != null ? m_Original.Meta.ToString() : null;
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
                    // Test for changes for the Published field
                    if (m_Current.Published != m_Original.Published)
                    {
                        change = new Change("Published");
                        change.NewValue = m_Current.Published != null ? m_Current.Published.ToString() : null;
                        change.OldValue = m_Original.Published != null ? m_Original.Published.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the PostedOn field
                    if (m_Current.PostedOn != m_Original.PostedOn)
                    {
                        change = new Change("PostedOn");
                        change.NewValue = m_Current.PostedOn != null ? m_Current.PostedOn.ToString() : null;
                        change.OldValue = m_Original.PostedOn != null ? m_Original.PostedOn.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the Modified field
                    if (m_Current.Modified != m_Original.Modified)
                    {
                        change = new Change("Modified");
                        change.NewValue = m_Current.Modified != null ? m_Current.Modified.ToString() : null;
                        change.OldValue = m_Original.Modified != null ? m_Original.Modified.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the Category field
                    if (m_Current.Category != m_Original.Category)
                    {
                        change = new Change("Category");
                        change.NewValue = m_Current.Category != null ? m_Current.Category.ToString() : null;
                        change.OldValue = m_Original.Category != null ? m_Original.Category.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the Enabled field
                    if (m_Current.Enabled != m_Original.Enabled)
                    {
                        change = new Change("Enabled");
                        change.NewValue = m_Current.Enabled != null ? m_Current.Enabled.ToString() : null;
                        change.OldValue = m_Original.Enabled != null ? m_Original.Enabled.ToString() : null;
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
    /// Manages a list of BlogPost objects
    /// </summary>
    public partial class BlogPostList : AbstractTableList
    {
        #region Private Fields
        // Data structure to hold objects
        private List<BlogPost> m_Items = new List<BlogPost>();
        
        // Comparison<T> delegate for sorting purposes
        Comparison<BlogPost> m_Comparison = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the list class and leaves the list empty
        /// </summary>
        public BlogPostList (SqlHelper db) : base(db) {}
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
        public IList<BlogPost> Items
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
                BlogPost[] list = new BlogPost[m_Items.Count];
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
        public void Add(BlogPost item)
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
                    BlogPost item = new BlogPost(m_db, dr);
                    this.Add(item);
                }
            }

            // After populating, call the default sort method
            Sort();
        }
        
        /// <summary>
        /// Sort by custom comparison delegate
        /// </summary>
        public void Sort(Comparison<BlogPost> comparison)
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
        public BlogPost Find(int id)
        {
            BlogPost result = null;
            BlogPostPrimaryKey key = new BlogPostPrimaryKey();
			key.Id = id;

            
            // Loop through our list until we find the match
            foreach (BlogPost item in m_Items)
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
            BlogPostPrimaryKey key = new BlogPostPrimaryKey();
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
        public bool Remove(BlogPost item)
        {
            return m_Items.Remove(item);
        }

        ///<summary></summary>
        public BlogPostData[] GetCurrentValues()
        {
            List<BlogPostData> results = new List<BlogPostData>();
            foreach (BlogPost item in m_Items)
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
    /// Class used to represent the primary key fields in the blog_post table
    /// </summary>
    public class BlogPostPrimaryKey : IComparable<BlogPostPrimaryKey> 
    {
		///<summary></summary>
		public int Id;
  	
        #region IComparable<CDMCodegenPrimaryKey> Members
        ///<summary></summary>
        public int CompareTo(BlogPostPrimaryKey other)
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
    /// Manages a Sorted list of BlogPost objects
    /// </summary>
    public partial class BlogPostSortedList : AbstractTableList
    {
        private SortedList<BlogPostPrimaryKey, BlogPost> m_Items = new SortedList<BlogPostPrimaryKey, BlogPost>();

        /// <summary>
        /// Creates an instance of the list class and leaves the list empty
        /// </summary>
        public BlogPostSortedList (SqlHelper db) : base(db) {}

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
        public IList<BlogPost> Items
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
                BlogPost[] list = new BlogPost[m_Items.Count];
                m_Items.Values.CopyTo(list, 0);
                results.AddRange(list);
                return results;
            }
        }

        /// <summary>
        /// Finds class by primary key
        /// </summary>
        public BlogPost Find(int id)
        {
            BlogPost result = null;
            BlogPostPrimaryKey key = new BlogPostPrimaryKey();
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
            BlogPostPrimaryKey key = new BlogPostPrimaryKey();
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
        public bool Remove(BlogPost item)
        {
            bool result = false;
            result = m_Items.Remove(item.PrimaryKey);
            return result;
        }

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        public void Add(BlogPost item)
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
                    BlogPost item = new BlogPost(m_db, dr);
                    this.Add(item);
                }
            }
        }
        
        ///<summary></summary>
        public BlogPostData[] GetCurrentValues()
        {
            List<BlogPostData> results = new List<BlogPostData>();
            foreach (BlogPost item in m_Items.Values)
            {
                results.Add(item.GetCurrentValues());
            }
            return results.ToArray();
        }
    }
    #endregion

    #region Data Class
    /// <summary>
    /// Represents just the data in the blog_post table.
    /// </summary>
    public partial class BlogPostData : ICloneable
    {
		///<summary></summary>
		public int? Id ; 
		///<summary></summary>
		public string Title ; 
		///<summary></summary>
		public string Body ; 
		///<summary></summary>
		public string ShortDesc ; 
		///<summary></summary>
		public string Description ; 
		///<summary></summary>
		public string Meta ; 
		///<summary></summary>
		public string UrlSlug ; 
		///<summary></summary>
		public bool? Published ; 
		///<summary></summary>
		public DateTime? PostedOn ; 
		///<summary></summary>
		public DateTime? Modified ; 
		///<summary></summary>
		public string Category ; 
		///<summary></summary>
		public bool? Enabled ; 
		
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
