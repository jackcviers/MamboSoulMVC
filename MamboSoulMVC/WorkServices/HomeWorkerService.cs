using Application.Repositories;
using MamboSoulMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MamboSoulMVC.WorkServices
{
    public class HomeWorkerService
    {
        private HomeRepository m_HomeRepository = new HomeRepository();

        internal ListViewModel LoadImages()
        {
            ListViewModel result = new ListViewModel(m_HomeRepository);
            return result;
        }
    }
}