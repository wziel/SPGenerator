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
        private ShimModelTranslator shimModelTranslator;

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
            var shimRuntimeContext = new ShimClientRuntimeContext(shimContext);
            shimContext.ExecuteQuery = () => { };
            shimRuntimeContext.LoadQueryOf1ClientObjectCollectionOfM0<ClientObject>(delegate { return null; });
        }

        private void InitializeSharePointSerivce()
        {
            var shimCtxHelper = new ShimSharePointContextHelper
            {
                ClientContextGet = () => shimContext
            };
            shimModelTranslator = new ShimModelTranslator();
            sharePointSerivce = new SharePointService(shimCtxHelper, shimModelTranslator);
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
            ListCollection toTranslate = null;
            var translated = new System.Collections.Generic.List<Model.SPGList>();
            var shimWeb = new ShimWeb();
            var shimListCollection = new ShimListCollection();
            shimContext.WebGet = () => shimWeb;
            shimWeb.ListsGet = () => shimListCollection;
            shimModelTranslator.TranslateToAppDomainListCollection = (listCollection) =>
            {
                toTranslate = listCollection;
                return translated;
            };
            //when
            var allSPGLists = sharePointSerivce.GetAllSPGLists();
            //then
            Assert.AreEqual(shimListCollection, toTranslate);
            Assert.AreEqual(allSPGLists, translated);
        }
    }
}