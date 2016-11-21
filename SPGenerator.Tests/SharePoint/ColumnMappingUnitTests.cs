using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.SharePoint.ColumnMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.SharePoint
{
    /// <summary>
    /// Unit tests for ColumnMappingResolver.
    /// </summary>
    [TestClass]
    public class ColumnMappingUnitTests : ShimTests
    {
        private ColumnMappingResolver columnMappingResolver;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            columnMappingResolver = new ColumnMappingResolver();
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
        }

        [TestMethod]
        public void NumberColumn_TranslatedProperly()
        {
            //given
            FieldNumber field = new ShimFieldNumber();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Number;
            setUpShimField(internalName: "test title");
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.AreEqual(field.InternalName, column.InternalName);
        }

        [TestMethod]
        public void TextColumn_TranslatedProperly()
        {
            //given
            Field field = new ShimFieldText();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Text;
            setUpShimField(internalName: "test title");
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.AreEqual(field.InternalName, column.InternalName);
        }

        [TestMethod]
        public void MultilineTextColumn_TranslatedProperly()
        {
            //given
            Field field = new ShimFieldMultiLineText();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Note;
            setUpShimField(internalName: "test title");
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.AreEqual(field.InternalName, column.InternalName);
        }

        /// <summary>
        /// Sets up all ShimField objects to return given properties.
        /// </summary>
        /// <param name="title">title of shim field</param>
        private void setUpShimField(string title = "", string internalName = "")
        {
            ShimField.AllInstances.TitleGet = (a) => title;
            ShimField.AllInstances.InternalNameGet = (a) => internalName;
        }
    }
}
