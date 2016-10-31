﻿using SPGenerator.AddinWeb.ViewModels.Home;
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
        private readonly IDataGenerator dataGenerator;
        private readonly ISharePointService sharePointService;

        public HomeController(ISharePointService sharePointService, IDataGenerator dataGenerator)
        {
            this.sharePointService = sharePointService;
            this.dataGenerator = dataGenerator;
        }

        /// <summary>
        /// Initial view that allows to select list to display.
        /// </summary>
        /// <returns></returns>
        [SharePointContextFilter]
        public ActionResult Index()
        {
            var allLists = sharePointService.AllListPOCO;
            var hostWebUrl = sharePointService.HostWebUrl;
            var indexVM = new IndexVM()
            {
                ListVMs = allLists.Select(l => new ListVM(l)).ToList(),
                HostWebUrl = hostWebUrl,
                RecordsToGenerateCount = 10,
            };
            return View(indexVM);
        }

        /// <summary>
        /// Reacts to list selection and downloads list fields to display;
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ListSelect(IndexVM indexVM)
        {
            indexVM.SelectedListVM = new ListVM(sharePointService.GetListPOCO(indexVM.SelectedListVM.Title));
            return View("Index", indexVM);
        }

        /// <summary>
        /// Initiates data generation for list and columns specified in VM.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GenerateData(IndexVM indexVM)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View("Index", indexVM);
        }
    }
}