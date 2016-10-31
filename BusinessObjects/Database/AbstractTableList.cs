using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BusinessObjects.Database
{
    /// <summary>
    /// AbstractTable list class
    /// </summary>
    public abstract class AbstractTableList : BaseSqlToCS
    {
        
        #region Protected Fields
        
        /// <summary></summary>
        protected int m_BatchSize = 10; 

        #endregion
        
        #region Constructors
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public AbstractTableList(SqlHelper db) : base(db)
        {
        }
        
        #endregion
        
        #region Public Properties
        
        /// <summary>
        /// Retrieves the Count of items in the list
        /// </summary>
        public abstract int Count { get; }
        
        /// <summary>
        /// Determines if any of the items are in Add or Edit mode
        /// </summary>
        public override EditMode Mode
        {
            get
            {
                var result = EditMode.None;
                foreach (AbstractTable item in TableItems)
                {
                    if (item.Mode == EditMode.Dirty)
                    {
                        // if any item is Dirty, then the whole list is. 
                        result = EditMode.Dirty;
                        // save some processing, get out of list
                        break;
                    }
                    if (item.Mode == EditMode.Add)
                    {
                        // as long as we haven't found it Dirty, then we are in Add
                        result = EditMode.Add;
                    }
                    if (result == EditMode.None && item.Mode == EditMode.Edit)
                    {
                        // if we are not Add/Dirty, then we are edit
                        result = EditMode.Edit;
                    }
                }
                return result;
            }
        }
        
        /// <summary>
        /// Retrieves the list of AbstractTable items to iterate through
        /// </summary>
        public virtual IList<AbstractTable> TableItems
        {
            get
            {
                return null;
            }
        }
        
        #endregion
        
        #region Public Methods
        
        /// <summary>
        /// Saves all items in the list as one batch using a SqlTransaction
        /// </summary>
        public override void Save()
        {
            SqlTransaction tx = null;
            try
            {
                tx = m_db.BeginTransaction();
                Save(tx);
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
        /// Saves all items in the list as one batch using a SqlTransaction
        /// </summary>
        public override void Save(SqlTransaction tx)
        {
            StringBuilder sql = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();
            int context = 0;
            
            if (!this.IsValid)
            {
                throw new ApplicationException("Can't do that");
            }
            
            foreach (AbstractTable item in TableItems)
            {
                string s = item.GenerateSql(context);
                if (s.Length > 0)
                {
                    SqlParameter[] p = item.GenerateSqlParameters(context);
                    if (p.Length > 0)
                    {
                        sql.AppendLine(s);
                        parameters.AddRange(p);
                        context++;
                        item.Mode = EditMode.Dirty;
                    }
                    else
                    {
                        // todo sql without parameters
                        throw new ApplicationException("Invalid SQL without parameters:");
                    }
                    
                    // never save more than 10 records at a time
                    // todo configurable
                    if (context > m_BatchSize)
                    {
                        ExecuteBatchSql(tx, sql, parameters);
                        sql = new StringBuilder();
                        parameters.Clear();
                        context = 0;
                    }
                }
            }
            ExecuteBatchSql(tx, sql, parameters);
        }
        
        /// <summary>
        /// Deletes all items in the list from the database using a single transaction
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
        /// Deletes all items in the list from the database using the supplied transaction
        /// </summary>
        public override void Delete(SqlTransaction tx)
        {
            StringBuilder sql = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();
            int context = 0;
            foreach (AbstractTable item in TableItems)
            {
                string s = item.GenerateSqlDelete(context);
                if (s.Length > 0)
                {
                    SqlParameter[] p = item.GenerateSqlParametersPrimaryKey(context);
                    if (p.Length > 0)
                    {
                        sql.AppendLine(s);
                        parameters.AddRange(p);
                        context++;
                    }
                    else
                    {
                        // todo sql without parameters
                        throw new ApplicationException("Invalid SQL without parameters:");
                    }
                }
            }
            ExecuteBatchSql(tx, sql, parameters);
        }
        
        /// <summary>
        /// Provides a default sorting routine for the list
        /// </summary>
        public virtual void Sort()
        {
        }
        
        #endregion
        
        #region Validation Checks
        
        /// <summary>
        /// Determine if any of ALL the items in the list are valid
        /// </summary>
        public virtual bool IsValid
        {
            get
            {
                bool result = true;
                foreach (AbstractTable item in TableItems)
                {
                    if (item != null)
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            }
        }
        
        /// <summary>
        /// Return a list of invalid items
        /// </summary>
        public virtual List<AbstractTable> InvalidItems
        {
            get
            {
                List<AbstractTable> result = new List<AbstractTable>();
                foreach (AbstractTable item in TableItems)
                {
                    if (item != null)
                        result.Add(item);
                }
                return result;
            }
        }

        #endregion

    }
}