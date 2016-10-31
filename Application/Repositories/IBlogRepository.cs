using BusinessObjects.GeneratedTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Repositories
{
    public interface IBlogRepository
    {
        IList<BlogPost> Posts(int pageNumber, int pageSize);
        int TotalPosts();
    }
}
