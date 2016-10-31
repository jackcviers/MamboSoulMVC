using BusinessObjects.Database;
using BusinessObjects.GeneratedTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        public BlogRepository()
        {
            using(SqlHelper db = new SqlHelper())
            {

            }
        }

        public IList<BlogPost> Posts(int pageNumber, int pageSize)
        {
            using(SqlHelper db = new SqlHelper())
            {
                IList<BlogPost> posts = new BlogPostList(db, DateTime.UtcNow.AddDays(-10)).Items;

                return posts;
            }
        }

        public int TotalPosts()
        {
            using (SqlHelper db = new SqlHelper())
            {
                return new BlogPostList(db, true).Items.Count;
            }
        }

        public void SavePost(string title, string description, string body, string category)
        {
            using (SqlHelper db = new SqlHelper())
            {
                BlogPost post = new BlogPost(db);
                post.Title = title;
                post.Body = body;
                post.ShortDesc = description;
                post.Description = description;
                post.Meta = "filler";
                post.UrlSlug = "filler";
                post.Published = true;
                post.PostedOn = DateTime.UtcNow;
                post.Modified = null;
                post.Category = "filler";
                post.Enabled = true;

                post.Save();
            }
        }
    }
}