using BusinessObjects.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessObjects.GeneratedTables
{
    public partial class SliderImageList
    {
        public SliderImageList(SqlHelper db, bool enabled)
            : base(db)
        {
            List<SqlParameter> results = new List<SqlParameter>();
            results.Add(CreateParameter(String.Format("@Enabled"), SqlDbType.Bit, enabled));
            string sql = "SELECT * FROM slider_image WITH (NOLOCK) WHERE Enabled = @Enabled";
            Populate(sql, results.ToArray());
        }
    }
}