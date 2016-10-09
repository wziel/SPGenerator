using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.SharePoint;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Fakes;
using System.Linq.Fakes;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using NSubstitute;

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
            var spContextHelper = Substitute.For<ISharePointContextHelper>();
            spContextHelper.ClientContext.Returns(new ShimClientContext());
            sharePointSerivce = new SharePointService(spContextHelper);
        }

        /// <summary>
        /// Test that checks if hidden lists are not returned from SharePoint.
        /// </summary>
        [TestMethod]
        public void TestAllSPGLists_DoesntReturnHiddenLists()
        {
            //given
            var lists = new List<List>()
            {
                GetShimList(hidden: true)
            };
            InitializeShimLinq(lists);
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
        public void TestAllSPGLists_ReturnsVisibleLists()
        {
            //given
            var lists = new List<List>()
            {
                GetShimList(hidden: false)
            };
            InitializeShimLinq(lists);
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
        public void TestAllSPGLists_TranslatesLists()
        {
            //given
            var lists = new List<List>()
            {
                GetShimList("test title", "test url", false)
            };
            InitializeShimLinq(lists);
            //when
            var allSPGLists = sharePointSerivce.AllSPGLists;
            //then
            Assert.AreEqual(lists[0].Title, allSPGLists[0].Title);
            Assert.AreEqual(lists[0].DefaultViewUrl, allSPGLists[0].ServerRelativeUrl);
        }

        /// <summary>
        /// Test that checks that nothing is returned if list is not found.
        /// </summary>
        [TestMethod]
        public void TestGetSPGList_DoesntReturnNonExistentList()
        {
            //given
            var otherListTitle = "OtherListTitle";
            var listTitle = "ListTitle";
            var nonExistentList = GetShimList(listTitle: listTitle);
            InitializeShimLinq(new List[] { nonExistentList });
            //when
            var spgList = sharePointSerivce.GetSPGList(otherListTitle);
            //then
            Assert.IsNull(spgList);
        }
        
        /// <summary>
        /// Test that checks that hidden list is not returned from SharePoint.
        /// </summary>
        [TestMethod]
        public void TestGetSPGList_DoesntReturnHiddenList()
        {
            //given
            var listTitle = "ListTitle";
            var hiddenList = GetShimList(hidden: true, listTitle: listTitle);
            InitializeShimLinq(new List[] { hiddenList });
            //when
            var spgList = sharePointSerivce.GetSPGList(listTitle);
            //then
            Assert.IsNull(spgList);
        }

        /// <summary>
        /// Test that checks that visible list is returned from SharePoint.
        /// </summary>
        [TestMethod]
        public void TestGetSPGList_ReturnsVisibleList()
        {
            //given
            var listTitle = "ListTitle";
            var list = GetShimList(listTitle: listTitle, hidden: false);
            InitializeShimLinq(new List[] { list });
            InitializeShimLinq(new Field[] { });
            //when
            var spgList = sharePointSerivce.GetSPGList(listTitle);
            //then
            Assert.IsNotNull(spgList);
        }

        /// <summary>
        /// Test that checks if non-required fields from base list type are not returned.
        /// </summary>
        [TestMethod]
        public void TestGetSPGList_DoesntReturnNonRequiredFieldsFromBaseListType()
        {
            //given
            var listTitle = "ListTitle";
            InitializeShimLinq(new List[] { GetShimList(listTitle) });
            InitializeShimLinq(new Field[] { GetShimField(required: false, fromBaseType: true) });
            //when
            var spgList = sharePointSerivce.GetSPGList(listTitle);
            //then
            Assert.IsFalse(spgList.SPGColumns.Any());
        }

        /// <summary>
        /// Test that checks if required fields from base type are returned.
        /// </summary>
        [TestMethod]
        public void TestGetSPGList_ReturnsRequiredFieldsFromBaseListType()
        {
            //given
            var listTitle = "listTitle";
            InitializeShimLinq(new List[] { GetShimList(listTitle) });
            InitializeShimLinq(new Field[] { GetShimField(required: true, fromBaseType: true) });
            //when
            var spgList = sharePointSerivce.GetSPGList(listTitle);
            //then
            Assert.IsTrue(spgList.SPGColumns.Any());
        }

        /// <summary>
        /// Test that checks if not required fields not from base type are returned.
        /// </summary>
        [TestMethod]
        public void TestGetSPGList_ReturnsNotRequiredFieldsNotFromBaseType()
        {
            //given
            var listTitle = "listTitle";
            InitializeShimLinq(new List[] { GetShimList(listTitle) });
            InitializeShimLinq(new Field[] { GetShimField(required: false, fromBaseType: false) });
            //when
            var spgList = sharePointSerivce.GetSPGList(listTitle);
            //then
            Assert.IsTrue(spgList.SPGColumns.Any());
        }

        /// <summary>
        /// Test that checks if required fields not from base type are returned.
        /// </summary>
        [TestMethod]
        public void TestGetSPGList_ReturnsRequiredFieldsNotFromBaseType()
        {
            //given
            var listTitle = "listTitle";
            InitializeShimLinq(new List[] { GetShimList(listTitle) });
            InitializeShimLinq(new Field[] { GetShimField(required: true, fromBaseType: false) });
            //when
            var spgList = sharePointSerivce.GetSPGList(listTitle);
            //then
            Assert.IsTrue(spgList.SPGColumns.Any());
        }

        /// <summary>
        /// Tests that list properties are properly translated.
        /// </summary>
        [TestMethod]
        public void TestGetSPGList_TranslatesList()
        {
            //given
            var listTitle = "listTitle";
            string viewUrl = "view url";
            List shimList = GetShimList(listTitle, viewUrl);
            InitializeShimLinq(new List[] { shimList });
            InitializeShimLinq(new Field[] { });
            //when
            var spgList = sharePointSerivce.GetSPGList(listTitle);
            //then
            Assert.AreEqual(shimList.Title, spgList.Title);
            Assert.AreEqual(shimList.DefaultViewUrl, spgList.ServerRelativeUrl);
        }

        /// <summary>
        /// Tests that fields properties of list are properly translated.
        /// </summary>
        [TestMethod]
        public void TestGetSPGList_TranslatesFields()
        {
            //given
            var listTitle = "listTitle";
            var fieldName = "field name";
            Field shimField = GetShimField(fieldName);
            InitializeShimLinq(new List[] { GetShimList(listTitle) });
            InitializeShimLinq(new Field[] { shimField });
            //when
            var spgList = sharePointSerivce.GetSPGList(listTitle);
            //then
            Assert.AreEqual(spgList.SPGColumns.First().ColumnName, shimField.Title);
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
            ShimClientRuntimeContext.AllInstances.LoadQueryOf1IQueryableOfM0<List>((a, b) => null);
            //when
            var hostWebUrl = sharePointSerivce.HostWebUrl;
            //then
            Assert.AreEqual(url, hostWebUrl);
        }

        /// <summary>
        /// Returns Shim List with specified properties.
        /// </summary>
        /// <param name="listTitle">Name of the list.</param>
        /// <param name="defaultViewUrl">Url to the view of this list in site collection domain.</param>
        /// <param name="hidden">Is this list hidden from sharepoint users.</param>
        /// <returns>Shim List with specified properties.</returns>
        private static ShimList GetShimList(string listTitle = "", string defaultViewUrl = "", bool hidden = false)
        {
            return new ShimList
            {
                TitleGet = () => listTitle,
                DefaultViewUrlGet = () => defaultViewUrl,
                HiddenGet = () => hidden,
                FieldsGet = () => new ShimFieldCollection()
            };
        }

        /// <summary>
        /// Returns Shim Field with specified properties.
        /// </summary>
        /// <param name="fieldTitle">Name of the column.</param>
        /// <param name="fromBaseType">Is the column from the base type (not defined in this list directly).</param>
        /// <param name="required">Is this field required in the list.</param>
        /// <returns>Shim Field with specified properties.</returns>
        private static ShimField GetShimField(string fieldTitle = "", bool fromBaseType = false, bool required = false)
        {
            return new ShimField
            {
                TitleGet = () => fieldTitle,
                FromBaseTypeGet = () => fromBaseType,
                RequiredGet = () => required
            };
        }

        /// <summary>
        /// Initialization of mocking linq queries to sharepoint for objects of type T.
        /// </summary>
        /// <param name="enumerable">Return collection for query.</param>
        private static void InitializeShimLinq<T>(IEnumerable<T> enumerable) where T : ClientObject
        {
            ShimQueryable.SelectOf2IQueryableOfM0ExpressionOfFuncOfM0M1<T, T>((a, b) => enumerable.AsQueryable());
            ShimClientObjectQueryableExtension.IncludeOf1IQueryableOfM0ExpressionOfFuncOfM0ObjectArray<T>((coll, incld) => coll);
            ShimClientRuntimeContext.AllInstances.LoadQueryOf1IQueryableOfM0<T>((ctx, query) => query.AsEnumerable());
            ShimClientRuntimeContext.AllInstances.LoadOf1M0ExpressionOfFuncOfM0ObjectArray<T>((a, b, c) => { });
        }
    }
}