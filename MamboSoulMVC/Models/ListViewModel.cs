using Application.Repositories;
using BusinessObjects.GeneratedTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MamboSoulMVC.Models
{
    public class ListViewModel
    {
        #region Properties
        public IList<SliderImage> sliderImgs { get; set; }
        #endregion

        public ListViewModel(HomeRepository homeRepository)
        {
            sliderImgs = homeRepository.GetSliderImages();
        }
    }
}