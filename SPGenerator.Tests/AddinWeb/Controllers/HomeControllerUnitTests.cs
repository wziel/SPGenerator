using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SPGenerator.AddinWeb.Controllers;
using SPGenerator.AddinWeb.ViewModels.Home;
using SPGenerator.Generator;
using SPGenerator.Model;
using SPGenerator.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SPGenerator.Tests.AddinWeb.Controllers
{
    [TestClass]
    public class HomeControllerUnitTests
    {
        private IDataGenerator dataGenerator;
        private ISharePointService sharePointService;
        private IIndexVMFactory indexVMFactory;
        private HomeController homeController;

        [TestInitialize]
        public void TestInitialize()
        {
            dataGenerator = Substitute.For<IDataGenerator>();
            sharePointService = Substitute.For<ISharePointService>();
            indexVMFactory = Substitute.For<IIndexVMFactory>();
            homeController = new HomeController(sharePointService, dataGenerator, indexVMFactory);
        }

        [TestMethod]
        public void Index_ReturnsDefaultView()
        {
            //given
            //when
            var viewResult = homeController.Index() as ViewResult;
            //then
            Assert.AreEqual(string.Empty, viewResult.ViewName);
        }

        [TestMethod]
        public void Index_ReturnsViewWithDefaultVM()
        {
            //given
            var allLists = new List<ListPOCO>();
            var hostWebUrl = "host web url";
            var indexVM = new IndexVM();
            sharePointService.AllListPOCO.Returns(allLists);
            sharePointService.HostWebUrl.Returns(hostWebUrl);
            indexVMFactory.GetDefaultIndexVM(allLists, hostWebUrl).Returns(indexVM);
            //when
            var viewResult = homeController.Index() as ViewResult;
            //then
            Assert.AreEqual(indexVM, viewResult.Model);
        }

        [TestMethod]
        public void ListSelect_ReturnsIndexView()
        {
            //given
            var listVM = GetStubListVM();
            var indexVM = GetStubIndexVM(selectedListVM: listVM);
            //when
            var viewResult = homeController.ListSelect(indexVM) as ViewResult;
            //then
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void ListSelect_ReturnsViewWithSelectedListVM()
        {
            //given
            var selectedList = new ListPOCO();
            sharePointService.GetListPOCO(Arg.Any<string>()).Returns(selectedList);
            var listVM = GetStubListVM();
            var indexVM = GetStubIndexVM(selectedListVM: listVM);
            var newIndexVM = GetStubIndexVM();
            indexVMFactory.GetIndexVMWithSelectedList(indexVM, selectedList).Returns(newIndexVM);
            //when
            var viewResult = homeController.ListSelect(indexVM) as ViewResult;
            //then
            Assert.AreEqual(newIndexVM, viewResult.Model);
        }

        [TestMethod]
        public void GenerateData()
        {
            //Controller tests aren't necessary since our controllers are thin.
        }

        private ListVM GetStubListVM(string title = "")
        {
            return new ListVM()
            {
                Title = title,
            };
        }

        private IndexVM GetStubIndexVM(ListVM selectedListVM = null)
        {
            return new IndexVM()
            {
                SelectedListVM = selectedListVM,
            };
        }
    }
}
