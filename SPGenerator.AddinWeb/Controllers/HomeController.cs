using SPGenerator.AddinWeb.ViewModels.Home;
using SPGenerator.Generator;
using SPGenerator.Model;
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
        private readonly IIndexVMFactory indexVMFactory;

        public HomeController(ISharePointService sharePointService, IDataGenerator dataGenerator,
            IIndexVMFactory indexVMFactory)
        {
            this.sharePointService = sharePointService;
            this.dataGenerator = dataGenerator;
            this.indexVMFactory = indexVMFactory;
        }

        /// <summary>
        /// Initial view that allows to select list to display.
        /// </summary>
        /// <returns></returns>
        [SharePointContextFilter]
        public ActionResult Index()
        {
            var indexVM = GetDefaultIndexVM();
            return View(indexVM);
        }

        private IndexVM GetDefaultIndexVM()
        {
            var allLists = sharePointService.AllListPOCO;
            var hostWebUrl = sharePointService.HostWebUrl;
            return indexVMFactory.GetDefaultIndexVM(allLists, hostWebUrl);
        }

        /// <summary>
        /// Reacts to list selection and downloads list fields to display;
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ListSelect(IndexVM indexVM)
        {
            var selectedList = sharePointService.GetListPOCO(indexVM.SelectedListVM.Title);
            indexVM = GetDefaultIndexVM();
            if(selectedList != null)
            {
                indexVMFactory.GetIndexVMWithSelectedList(indexVM, selectedList);
            }
            ModelState.Clear();
            return View("Index", indexVM);
        }

        /// <summary>
        /// Initiates data generation for list and columns specified in VM.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GenerateData(IndexVM indexVM)
        {
            var listPOCO = sharePointService.GetListPOCO(indexVM.SelectedListVM.Title);
            indexVM.SyncModels(listPOCO);
            ModelState.Clear();
            TryValidateModel(indexVM);
            if (ModelState.IsValid)
            {
                var data = dataGenerator.GenerateData(listPOCO, indexVM.RecordsToGenerateCount);
                sharePointService.Save(data, listPOCO);
            }
            indexVM = GetDefaultIndexVM();
            indexVM = indexVMFactory.GetIndexVMWithSelectedList(indexVM, listPOCO);
            indexVM.ShowSuccessGeneration = ModelState.IsValid;
            indexVM.ShowFailedGeneration = !ModelState.IsValid;
            return View("Index", indexVM);
        }
    }
}