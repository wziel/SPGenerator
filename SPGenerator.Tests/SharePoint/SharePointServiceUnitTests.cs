using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.SharePoint;
using SPGenerator.SharePoint.Fakes;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Fakes;
using System.Linq.Fakes;
using System.Linq;

namespace SPGenerator.Tests.SharePoint
{
    /// <summary>
    /// Unit tests for SharePointService.
    /// </summary>
    [TestClass]
    public class SharePointServiceUnitTests : ShimTests
    {
        private SharePointService sharePointSerivce;

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            ShimClientContext.AllInstances.ExecuteQuery = (a) => { };
            ShimClientContext.AllInstances.WebGet = (a) => new ShimWeb();
            ShimWeb.AllInstances.ListsGet = (a) => new ShimListCollection();
            ShimSharePointContextHelper.AllInstances.ClientContextGet = (a) => new ShimClientContext();
            sharePointSerivce = new SharePointService(new ShimSharePointContextHelper(), new ModelTranslator());
        }

        /// <summary>
        /// Initialization for TestAllSPGLists tests.
        /// </summary>
        /// <param name="lists">Lists available to return from shim context.</param>
        private static void TestAllSPGLists_Initialize(System.Collections.Generic.List<List> lists)
        {
            ShimQueryable.SelectOf2IQueryableOfM0ExpressionOfFuncOfM0M1<List, List>((a, b) => lists.AsQueryable());
            ShimClientObjectQueryableExtension.IncludeOf1IQueryableOfM0ExpressionOfFuncOfM0ObjectArray<List>((list, incld) => list);
            ShimClientRuntimeContext.AllInstances.LoadQueryOf1IQueryableOfM0<List>((ctx, query) => query.AsEnumerable());
        }

        /// <summary>
        /// Test that checks if hidden lists are not returned from SharePoint.
        /// </summary>
        [TestMethod]
        public void TestAllSPGLists_FiltersOutHiddenLists()
        {
            //given
            var lists = new List<List>()
            {
                new ShimList() { TitleGet = () => "", DefaultViewUrlGet = () => "", HiddenGet = () => true }
            };
            TestAllSPGLists_Initialize(lists);
            //when
            var allSPGLists = sharePointSerivce.AllSPGLists;
            //then
            Assert.IsFalse(allSPGLists.Any());
        }

        /// <summary>
        /// Test that checks if not-hidden lists are returned from SharePoint.
        /// fetching all lists.
        /// </summary>
        [TestMethod]
        public void TestAllSPGLists_LeavesVisibleLists()
        {
            //given
            var lists = new List<List>()
            {
                new ShimList() { TitleGet = () => "", DefaultViewUrlGet = () => "", HiddenGet = () => false }
            };
            TestAllSPGLists_Initialize(lists);
            //when
            var allSPGLists = sharePointSerivce.AllSPGLists;
            //then
            Assert.AreEqual(1, allSPGLists.Count);
        }

        /// <summary>
        /// Test that checks if lists from SharePoint are properly translated
        /// to application domain models.
        /// </summary>
        [TestMethod]
        public void TestAllSPGLists_ProperlyTranslatesModels()
        {
            //given
            var lists = new List<List>()
            {
                new ShimList() { TitleGet = () => "Test title", DefaultViewUrlGet = () => "Test url", HiddenGet = () => false }
            };
            TestAllSPGLists_Initialize(lists);
            //when
            var allSPGLists = sharePointSerivce.AllSPGLists;
            //then
            Assert.AreEqual(lists[0].Title, allSPGLists[0].Title);
            Assert.AreEqual(lists[0].DefaultViewUrl, allSPGLists[0].ServerRelativeUrl);
        }

        /// <summary>
        /// Test if SharePointService retrieves host web url properly.
        /// </summary>
        [TestMethod]
        public void TestHostWebUrl()
        {
            //given
            var url = "Test url";
            ShimWeb.AllInstances.UrlGet = (a) => url;
            ShimClientRuntimeContext.AllInstances.LoadQueryOf1IQueryableOfM0<List>((a, qb) => null);
            //when
            var hostWebUrl = sharePointSerivce.HostWebUrl;
            //then
            Assert.AreEqual(url, hostWebUrl);
        }
    }
}