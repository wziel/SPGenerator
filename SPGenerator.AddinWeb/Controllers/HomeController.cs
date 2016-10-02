using SPGenerator.AddinWeb.ViewModels.Home;
using SPGenerator.Generator;
using SPGenerator.SharePoint;
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
        private readonly DataGenerator dataGenerator;
        private readonly SharePointService sharePointService;

        public HomeController(SharePointService sharePointService, DataGenerator dataGenerator)
        {
            this.sharePointService = sharePointService;
            this.dataGenerator = dataGenerator;
        }

        /// <summary>
        /// View for selection on SPGLists.
        /// </summary>
        /// <returns></returns>
        [SharePointContextFilter]
        public ActionResult Index()
        {
            var allLists = sharePointService.AllSPGLists;
            var hostWebUrl = sharePointService.HostWebUrl;
            return View(new IndexVM()
            {
                SPGLists = allLists,
                HostWebUrl = hostWebUrl,
                SelectedSPGList = allLists.FirstOrDefault()
            });
        }
    }
}
