using MamboSoulMVC.Models;
using MamboSoulMVC.WorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MamboSoulMVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private HomeWorkerService m_HomeWorkerService;

        public HomeController()
        {
            m_HomeWorkerService = new HomeWorkerService();
        }

        public ActionResult Index()
        {
            ListViewModel viewModel = m_HomeWorkerService.LoadImages();
            return View(viewModel);
        }

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
