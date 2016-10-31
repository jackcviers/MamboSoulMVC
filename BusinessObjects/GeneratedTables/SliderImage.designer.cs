using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using BusinessObjects.Database;

// Code Generated Classes. !!! DO NOT CHANGE !!!
// Database   : MamboSoul
// Created    : 9/26/2015 1:22:56 PM
// User       : Joe
// Connection : Provider=SQLOLEDB.1;Data Source=WINDOWS-28T6H06;Initial Catalog=MamboSoul;Integrated Security=SSPI
namespace BusinessObjects.GeneratedTables
{
    #region Business Entity Class
    /// <summary>
    /// Description : 
    /// Table Created : 9/26/2015 1:22:05 PM
    /// </summary>
    public partial class SliderImage : AbstractTable
    {
        private SliderImageData m_Current = new SliderImageData();
        private SliderImageData m_Original;
        
        public const string TABLE_NAME = "slider_image";
        //private string TABLE_TYPE = "";
        //private bool HAS_LOGICAL_KEYS = false;
        
        #region Constructors
        
        /// <summary>
        /// Constructor used to create an instance of this table in AddMode.
        /// </summary>
        public SliderImage(SqlHelper db) : base(db) {}

        /// <summary>
        /// Constructor used to create an instance of this table using an
        /// existing SqlDataReader.
        /// </summary>
        public SliderImage(SqlHelper db, SqlDataReader dr) : base(db, dr) {}

        /// <summary>
        /// Constructor used to create an instance of this table using the
        /// primary keys of the table.
        /// </summary>		
        public SliderImage(SqlHelper db, int? imageId) 
            : this(db)
        {
			if (imageId != null)
			{
				m_Current.ImageId = imageId;

                Read();
            }
        }

        /// <summary>
        /// Constructor used to create an instance of this table using the
        /// provided data class.
        /// </summary>		
        public SliderImage(SqlHelper db, SliderImageData data)
            : this(db, data.ImageId)
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
                    case "imageid":
                        result = ImageId;
                        break;
                    case "filename":
                        result = FileName;
                        break;
                    case "extension":
                        result = Extension;
                        break;
                    case "projectfiledirectory":
                        result = ProjectFileDirectory;
                        break;
                    case "description":
                        result = Description;
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
                        case "imageid":
                            throw new ApplicationException(String.Format("Field {0} cannot be set because " +
                                "it is an Identity field or a computed field!", field));
                        case "filename":
                            if (value == null)
								FileName = null;
                            else
								FileName = Convert.ToString(value);
                            break;
                        case "extension":
                            if (value == null)
								Extension = null;
                            else
								Extension = Convert.ToString(value);
                            break;
                        case "projectfiledirectory":
                            if (value == null)
								ProjectFileDirectory = null;
                            else
								ProjectFileDirectory = Convert.ToString(value);
                            break;
                        case "description":
                            if (value == null)
								Description = null;
                            else
								Description = Convert.ToString(value);
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
        public SliderImagePrimaryKey PrimaryKey
        {
            get
            {
                SliderImagePrimaryKey key = new SliderImagePrimaryKey();
				key.ImageId = m_Current.ImageId.GetValueOrDefault();
  			
                return key;
            }
        }
        /// <summary>
        /// Public property that represents the ImageId field.
        /// Description: 
        /// </summary>
        public int? ImageId
        {
            get { return m_Current.ImageId; }
        }
        /// <summary>
        /// Public property that represents the FileName field.
        /// Description: 
        /// </summary>
        public string FileName
        {
            get { return m_Current.FileName; }
            set 
            {	
                if (value != m_Current.FileName)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.FileName = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the Extension field.
        /// Description: 
        /// </summary>
        public string Extension
        {
            get { return m_Current.Extension; }
            set 
            {	
                if (value != m_Current.Extension)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.Extension = value;
                }
            }
        }
        /// <summary>
        /// Public property that represents the ProjectFileDirectory field.
        /// Description: 
        /// </summary>
        public string ProjectFileDirectory
        {
            get { return m_Current.ProjectFileDirectory; }
            set 
            {	
                if (value != m_Current.ProjectFileDirectory)
                {
                    base.Mode = EditMode.Edit;
                    m_Current.ProjectFileDirectory = value;
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
                if (!dr.IsDBNull(dr.GetOrdinal("ImageId"))) m_Current.ImageId = dr.GetInt32(dr.GetOrdinal("ImageId"));
                if (!dr.IsDBNull(dr.GetOrdinal("FileName"))) m_Current.FileName = dr.GetString(dr.GetOrdinal("FileName"));
                if (!dr.IsDBNull(dr.GetOrdinal("Extension"))) m_Current.Extension = dr.GetString(dr.GetOrdinal("Extension"));
                if (!dr.IsDBNull(dr.GetOrdinal("ProjectFileDirectory"))) m_Current.ProjectFileDirectory = dr.GetString(dr.GetOrdinal("ProjectFileDirectory"));
                if (!dr.IsDBNull(dr.GetOrdinal("Description"))) m_Current.Description = dr.GetString(dr.GetOrdinal("Description"));
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
            sql.Append("SELECT * FROM slider_image WITH (NOLOCK) ");
            sql.Append(GenerateWhereClausePrimaryKey(context));
            return sql.ToString();
        }
		///<summary></summary>
        protected override string GenerateWhereClausePrimaryKey(int context)
        {
            StringBuilder sql = new StringBuilder();
            // primary key where clause
            sql.AppendFormat(" WHERE ImageId=@ImageId_{0}_key", context);
            return sql.ToString();
        }
        /// <summary>
        /// Generates an array of SqlParameters based on the primary key of the table
        /// </summary>
        public override SqlParameter[] GenerateSqlParametersPrimaryKey(int context)
        {
            List<SqlParameter> results = new List<SqlParameter>();
            results.Add(CreateParameter(String.Format("@ImageId_{0}_key", context), SqlDbType.Int, m_Current.ImageId));
            return results.ToArray();
        }
        #endregion
        
        #region INSERT
        ///<summary></summary>
        protected override string GenerateSqlInsert(int context)
        {
            StringBuilder result = new StringBuilder();
            List<string> fields = new List<string>();

            result.Append("INSERT INTO slider_image (");

            #region Check each field to insert
            if (m_Current.FileName != null) fields.Add("FileName");
            if (m_Current.Extension != null) fields.Add("Extension");
            if (m_Current.ProjectFileDirectory != null) fields.Add("ProjectFileDirectory");
            if (m_Current.Description != null) fields.Add("Description");
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

            if (m_Current.FileName != null)
                results.Add(CreateParameter(String.Format("@FileName_{0}", context), SqlDbType.VarChar, m_Current.FileName));
            if (m_Current.Extension != null)
                results.Add(CreateParameter(String.Format("@Extension_{0}", context), SqlDbType.VarChar, m_Current.Extension));
            if (m_Current.ProjectFileDirectory != null)
                results.Add(CreateParameter(String.Format("@ProjectFileDirectory_{0}", context), SqlDbType.VarChar, m_Current.ProjectFileDirectory));
            if (m_Current.Description != null)
                results.Add(CreateParameter(String.Format("@Description_{0}", context), SqlDbType.VarChar, m_Current.Description));
            if (m_Current.Enabled != null)
                results.Add(CreateParameter(String.Format("@Enabled_{0}", context), SqlDbType.Bit, m_Current.Enabled));
            #endregion
            
            return results.ToArray();
        }
        ///<summary></summary>
        protected override void SetIdentity(object value)
        {
        		m_Current.ImageId = Convert.ToInt32(value);
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
            sql.Append("UPDATE slider_image SET ");
            if (m_Current.FileName != m_Original.FileName)
                fields.Add(String.Format("FileName=@FileName_{0}", context));
            if (m_Current.Extension != m_Original.Extension)
                fields.Add(String.Format("Extension=@Extension_{0}", context));
            if (m_Current.ProjectFileDirectory != m_Original.ProjectFileDirectory)
                fields.Add(String.Format("ProjectFileDirectory=@ProjectFileDirectory_{0}", context));
            if (m_Current.Description != m_Original.Description)
                fields.Add(String.Format("Description=@Description_{0}", context));
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
            if (m_Current.FileName != m_Original.FileName)
                results.Add(CreateParameter(String.Format("@FileName_{0}", context), SqlDbType.VarChar, m_Current.FileName));
            if (m_Current.Extension != m_Original.Extension)
                results.Add(CreateParameter(String.Format("@Extension_{0}", context), SqlDbType.VarChar, m_Current.Extension));
            if (m_Current.ProjectFileDirectory != m_Original.ProjectFileDirectory)
                results.Add(CreateParameter(String.Format("@ProjectFileDirectory_{0}", context), SqlDbType.VarChar, m_Current.ProjectFileDirectory));
            if (m_Current.Description != m_Original.Description)
                results.Add(CreateParameter(String.Format("@Description_{0}", context), SqlDbType.VarChar, m_Current.Description));
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
            sql.Append("DELETE FROM slider_image");
            sql.Append(GenerateWhereClausePrimaryKey(context));
            
            return sql.ToString();        
        }
        #endregion
        
        #endregion   

        #region State Management
        /// <summary>
        /// Returns an instance of a class that represents the data in the table.
        /// </summary>
        public SliderImageData GetCurrentValues()
        {
        	
            return (SliderImageData)m_Current.Clone();
        }

        /// <summary>
        /// Sets the properties of the object from an instance of the data class.
        /// </summary>
        public void SetCurrentValues(SliderImageData data)
        {
            // set all fields
            FileName = data.FileName;
            Extension = data.Extension;
            ProjectFileDirectory = data.ProjectFileDirectory;
            Description = data.Description;
            Enabled = data.Enabled;
        }

        /// <summary>
        /// Returns an instance of the original values
        /// </summary>
        public SliderImageData GetOriginalValues()
        {
            object result = null;
            if (m_Original != null) result = m_Original.Clone();
            return (SliderImageData)result;
        }

        /// <summary>
        /// Sets/Saves the current values to the original values class
        /// </summary>
        public override void SetOriginalValues()
        {
            m_Original = (SliderImageData)m_Current.Clone();
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
                    
                    // Test for changes for the ImageId field
                    if (m_Current.ImageId != m_Original.ImageId)
                    {
                        change = new Change("ImageId");
                        change.NewValue = m_Current.ImageId != null ? m_Current.ImageId.ToString() : null;
                        change.OldValue = m_Original.ImageId != null ? m_Original.ImageId.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the FileName field
                    if (m_Current.FileName != m_Original.FileName)
                    {
                        change = new Change("FileName");
                        change.NewValue = m_Current.FileName != null ? m_Current.FileName.ToString() : null;
                        change.OldValue = m_Original.FileName != null ? m_Original.FileName.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the Extension field
                    if (m_Current.Extension != m_Original.Extension)
                    {
                        change = new Change("Extension");
                        change.NewValue = m_Current.Extension != null ? m_Current.Extension.ToString() : null;
                        change.OldValue = m_Original.Extension != null ? m_Original.Extension.ToString() : null;
                        result.Add(change);
                    }
                    // Test for changes for the ProjectFileDirectory field
                    if (m_Current.ProjectFileDirectory != m_Original.ProjectFileDirectory)
                    {
                        change = new Change("ProjectFileDirectory");
                        change.NewValue = m_Current.ProjectFileDirectory != null ? m_Current.ProjectFileDirectory.ToString() : null;
                        change.OldValue = m_Original.ProjectFileDirectory != null ? m_Original.ProjectFileDirectory.ToString() : null;
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
    /// Manages a list of SliderImage objects
    /// </summary>
    public partial class SliderImageList : AbstractTableList
    {
        #region Private Fields
        // Data structure to hold objects
        private List<SliderImage> m_Items = new List<SliderImage>();
        
        // Comparison<T> delegate for sorting purposes
        Comparison<SliderImage> m_Comparison = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the list class and leaves the list empty
        /// </summary>
        public SliderImageList (SqlHelper db) : base(db) {}
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
        public IList<SliderImage> Items
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
                SliderImage[] list = new SliderImage[m_Items.Count];
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
        public void Add(SliderImage item)
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
                    SliderImage item = new SliderImage(m_db, dr);
                    this.Add(item);
                }
            }

            // After populating, call the default sort method
            Sort();
        }
        
        /// <summary>
        /// Sort by custom comparison delegate
        /// </summary>
        public void Sort(Comparison<SliderImage> comparison)
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
        public SliderImage Find(int imageId)
        {
            SliderImage result = null;
            SliderImagePrimaryKey key = new SliderImagePrimaryKey();
			key.ImageId = imageId;

            
            // Loop through our list until we find the match
            foreach (SliderImage item in m_Items)
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
        public bool Remove( int imageId)
        {
            bool result = false;
            SliderImagePrimaryKey key = new SliderImagePrimaryKey();
			key.ImageId = imageId;


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
        public bool Remove(SliderImage item)
        {
            return m_Items.Remove(item);
        }

        ///<summary></summary>
        public SliderImageData[] GetCurrentValues()
        {
            List<SliderImageData> results = new List<SliderImageData>();
            foreach (SliderImage item in m_Items)
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
    /// Class used to represent the primary key fields in the slider_image table
    /// </summary>
    public class SliderImagePrimaryKey : IComparable<SliderImagePrimaryKey> 
    {
		///<summary></summary>
		public int ImageId;
  	
        #region IComparable<CDMCodegenPrimaryKey> Members
        ///<summary></summary>
        public int CompareTo(SliderImagePrimaryKey other)
        {
            int result = 0;
            if (other != null)
            {
                        if (this.ImageId.CompareTo(other.ImageId) != 0)
                            result = this.ImageId.CompareTo(other.ImageId);
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
    /// Manages a Sorted list of SliderImage objects
    /// </summary>
    public partial class SliderImageSortedList : AbstractTableList
    {
        private SortedList<SliderImagePrimaryKey, SliderImage> m_Items = new SortedList<SliderImagePrimaryKey, SliderImage>();

        /// <summary>
        /// Creates an instance of the list class and leaves the list empty
        /// </summary>
        public SliderImageSortedList (SqlHelper db) : base(db) {}

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
        public IList<SliderImage> Items
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
                SliderImage[] list = new SliderImage[m_Items.Count];
                m_Items.Values.CopyTo(list, 0);
                results.AddRange(list);
                return results;
            }
        }

        /// <summary>
        /// Finds class by primary key
        /// </summary>
        public SliderImage Find(int imageId)
        {
            SliderImage result = null;
            SliderImagePrimaryKey key = new SliderImagePrimaryKey();
			key.ImageId = imageId;
  			
            if (m_Items.ContainsKey(key))
            {
                result = m_Items[key];
            }
            return result;
        }

        /// <summary>
        /// Removes class from list by primary key
        /// </summary>
        public bool Remove( int imageId)
        {
            bool result = false;
            SliderImagePrimaryKey key = new SliderImagePrimaryKey();
			key.ImageId = imageId;
  			
            if (m_Items.ContainsKey(key))
            {
                result = Remove(m_Items[key]);
            }
            return result;
        }

        /// <summary>
        /// Removes the class passed in by primary key
        /// </summary>
        public bool Remove(SliderImage item)
        {
            bool result = false;
            result = m_Items.Remove(item.PrimaryKey);
            return result;
        }

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        public void Add(SliderImage item)
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
                    SliderImage item = new SliderImage(m_db, dr);
                    this.Add(item);
                }
            }
        }
        
        ///<summary></summary>
        public SliderImageData[] GetCurrentValues()
        {
            List<SliderImageData> results = new List<SliderImageData>();
            foreach (SliderImage item in m_Items.Values)
            {
                results.Add(item.GetCurrentValues());
            }
            return results.ToArray();
        }
    }
    #endregion

    #region Data Class
    /// <summary>
    /// Represents just the data in the slider_image table.
    /// </summary>
    public partial class SliderImageData : ICloneable
    {
		///<summary></summary>
		public int? ImageId ; 
		///<summary></summary>
		public string FileName ; 
		///<summary></summary>
		public string Extension ; 
		///<summary></summary>
		public string ProjectFileDirectory ; 
		///<summary></summary>
		public string Description ; 
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
