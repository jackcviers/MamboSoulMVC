using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BusinessObjects.Database
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseSqlToCS
    {
        /// <summary></summary>
        protected SqlHelper m_db;
        private EditMode m_editMode = EditMode.Add;

        /// <summary>
        /// Constructor used to create an instance of this table in AddMode.
        /// </summary>
        public BaseSqlToCS(SqlHelper db)
        {
            m_db = db;
            Initialize();
        }

        /// <summary>
        /// Gets a value indicating the "EditMode" of this record.
        /// </summary>
        public virtual EditMode Mode
        {
            get
            {
                return m_editMode;
            }
            set
            {
                if ((m_editMode == EditMode.None && (value == EditMode.Add || value == EditMode.Edit)) ||
                    (m_editMode != EditMode.None && (value == EditMode.None || value == EditMode.Dirty)))
                {
                    m_editMode = value;
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public abstract void Delete(SqlTransaction tx);

        /// <summary>
        /// 
        /// </summary>
        public abstract void Delete();

        /// <summary>
        /// 
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// 
        /// </summary>
        public abstract void Save(SqlTransaction tx);

        /// <summary>
        /// Initialization method used by the custom versio of the partial class
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected SqlParameter CreateParameter(string name, System.Data.SqlDbType type, object value)
        {
            SqlParameter result = new SqlParameter();
            result.ParameterName = name;
            result.SqlDbType = type;
            if (value == null)
                result.Value = DBNull.Value;
            else
                result.Value = value;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void ExecuteBatchSql(SqlTransaction tx, StringBuilder sql, List<SqlParameter> parameters)
        {
            // todo how much do we want to try to validate that sql and parameters are correct
            if (sql.Length > 0 && parameters.Count > 0)
            {
                m_db.ExecuteNonQuery(tx, CommandType.Text, sql.ToString(), parameters.ToArray());
            }
        }
    }
}