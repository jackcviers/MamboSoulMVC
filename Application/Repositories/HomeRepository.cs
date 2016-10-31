using BusinessObjects.Database;
using BusinessObjects.GeneratedTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Application.Repositories
{
    public class HomeRepository
    {
        public IList<SliderImage> GetSliderImages()
        {
            using(SqlHelper db = new SqlHelper())
            {
                IList<SliderImage> result = new SliderImageList(db, true).Items;

                return result;
            }
        }
    }
}