using BusinessObjects.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessObjects.GeneratedTables
{
    public partial class BlogPost
    {
    }

    public partial class BlogPostList
    {
        public BlogPostList(SqlHelper db, DateTime asOfDate)
            : base(db)
        {
            List<SqlParameter> results = new List<SqlParameter>();
            results.Add(CreateParameter(String.Format("@AsOfDate"), SqlDbType.DateTime, asOfDate));
            string sql = "SELECT * FROM blog_post WITH (NOLOCK) WHERE PostedOn >= @AsOfDate";
            Populate(sql, results.ToArray());
        }

        public BlogPostList(SqlHelper db, bool enabled)
            : base(db)
        {
            List<SqlParameter> results = new List<SqlParameter>();
            string sql = enabled ? "SELECT * FROM blog_post WITH (NOLOCK) WHERE Enabled = 1" : "SELECT * FROM blog_post WITH (NOLOCK) WHERE Enabled = 0";
            Populate(sql, results.ToArray());
        }
    }
}