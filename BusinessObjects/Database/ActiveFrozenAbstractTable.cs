using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessObjects.Database
{
    public abstract class ActiveFrozenAbstractTable : AbstractTable
    {
        #region Constructors

        /// <summary>
        /// Constructor used to create an instance of this table in AddMode.
        /// </summary>
        public ActiveFrozenAbstractTable(SqlHelper db)
            : base(db)
        {
        }

        /// <summary>
        /// Constructor used to create an instance of this table using an
        /// existing SqlDataReader.
        /// </summary>
        public ActiveFrozenAbstractTable(SqlHelper db, SqlDataReader dr)
            : base(db, dr)
        {
        }

        #endregion

        public abstract string RecordStatus { set; get; }

        protected abstract string GenerateSqlActiveKeys(int context);

        protected abstract SqlParameter[] GenerateSqlParametersActiveKeys(int context);

        protected virtual string UpdateActiveToFrozen(int context)
        {
            var result = new StringBuilder();
            // run freeze 
            result.Append("UPDATE ");
            result.Append(DatabaseObjectName);
            result.Append(" SET RecordStatus='F'");
            result.AppendLine(GenerateSqlActiveKeys(context));
            // then insert "A"ctive
            RecordStatus = "A";
            return result.ToString();
        }

    }

}
