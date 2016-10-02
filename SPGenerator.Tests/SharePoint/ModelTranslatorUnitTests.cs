using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.SharePoint;
using System.Collections.Generic;
using System.Linq;

namespace SPGenerator.Tests.SharePoint
{
    /// <summary>
    /// Unit tests for ModelTranslator.
    /// </summary>
    [TestClass]
    public class ModelTranslatorUnitTests : ShimTests
    {
        private ModelTranslator modelTranslator;

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            modelTranslator = new ModelTranslator();
        }

        /// <summary>
        /// Test that checks if during translation of IEnumerable of Lists the result
        /// collection is of the same size as input collection.
        /// </summary>
        [TestMethod]
        public void TestTranslateToAppDomain_IEnumerableOfList_CountIsTheSame()
        {
            //given
            var listCollection = new List<List>()
            {
                new ShimList() { TitleGet = () => "", DefaultViewUrlGet = () => "" },
                new ShimList() { TitleGet = () => "Abc", DefaultViewUrlGet = () => "Test" },
                new ShimList() { TitleGet = () => "QWertźć śźć", DefaultViewUrlGet = () => "asdfRE REŹć" },
            };
            //when
            var translated = modelTranslator.TranslateToAppDomain(listCollection);
            //then
            Assert.AreEqual(listCollection.Count, translated.Count);
        }

        /// <summary>
        /// Test that checks if during translation of IEnumerable of Lists the result
        /// collection contains elements with the same field values as input elements.
        /// </summary>
        [TestMethod]
        public void TestTranslateToAppDomain_IEnumerableOfList_FieldsAreTheSame()
        {
            //given
            var listCollection = new List<List>()
            {
                new ShimList() { TitleGet = () => "Title1 test", DefaultViewUrlGet = () => "url test" }
            };
            //when
            var translated = modelTranslator.TranslateToAppDomain(listCollection);
            //then
            var spgList = translated.First();
            var spList = listCollection.First();
            Assert.AreEqual(spgList.ServerRelativeUrl, spList.DefaultViewUrl);
            Assert.AreEqual(spgList.Title, spList.Title);
        }
    }
}
