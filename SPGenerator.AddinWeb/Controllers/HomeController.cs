using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPGenerator.AddinWeb.Controllers
{
    /// <summary>
    /// Controller that is responsible for displaying main view of the application,
    /// handling user configuring data generation properties and initializing
    /// data generation.
    /// </summary>
    public class HomeController : Controller
    {
        [SharePointContextFilter]
        public ActionResult Index()
        {
            ViewBag.UserName = "Mock user name";
            return View();
        }
    }
}
