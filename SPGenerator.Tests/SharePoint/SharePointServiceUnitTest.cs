using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.SharePoint;
using SPGenerator.SharePoint.Fakes;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Fakes;
using Microsoft.QualityTools.Testing.Fakes;

namespace SPGenerator.Tests.SharePoint
{
    [TestClass]
    public class SharePointServiceUnitTest : ShimTest
    {
        private SharePointService sharePointSerivce;
        private ShimClientContext shimContext;
        private ShimClientRuntimeContext shimRuntimeContext;

        [TestCleanup]
        public override void CleanUpClass()
        {
            base.CleanUpClass();
        }

        [TestInitialize]
        public override void InitializeClass()
        {
            base.InitializeClass();
            InitializeShimClientContext();
            InitializeSharePointSerivce();
        }

        private void InitializeShimClientContext()
        {
            shimContext = new ShimClientContext();
            shimRuntimeContext = new ShimClientRuntimeContext(shimContext);
            shimContext.ExecuteQuery = () => { };
            shimRuntimeContext.LoadQueryOf1ClientObjectCollectionOfM0<ClientObject>(delegate { return null; });
            var shimWeb = new ShimWeb();
            var shimListCollection = new ShimListCollection();
            shimContext.WebGet = () => shimWeb;
            shimWeb.ListsGet = () => shimListCollection;
        }

        private void InitializeSharePointSerivce()
        {
            var shimCtxHelper = new ShimSharePointContextHelper
            {
                ClientContextGet = () => shimContext
            };
            var modelTranslator = new ModelTranslator();
            sharePointSerivce = new SharePointService(shimCtxHelper, modelTranslator);
        }

        /// <summary>
        /// Test that checks if GetAllSPGLists method calls ClientContext to
        /// get all SPGLists and then returns those lists as SPGLists using
        /// ModelTranslator.
        /// </summary>
        [TestMethod]
        public void GetAllSPGLists()
        {
            //given
            var lists = new System.Collections.Generic.List<List>()
            {
                new ShimList() { TitleGet = () => "List1", DefaultViewUrlGet = () => "DefaultView1" }
            };
            shimRuntimeContext.LoadQueryOf1IQueryableOfM0<ClientObject>((query) => lists);
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
        public void GetHostWebUrl()
        {
            throw new NotImplementedException();
        }
    }
}