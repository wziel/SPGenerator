using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.Model.Column;
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
    public class ColumnMappingResolverUnitTests : ShimTests
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
        public void BooleanColumn_Map_ReturnsBooleanColumnPOCO()
        {
            //given
            Field field = new ShimField();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Boolean;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.IsInstanceOfType(column, typeof(BooleanColumnPOCO));
        }

        [TestMethod]
        public void BooleanColumn_Map_AppliesAllProperties()
        {
            //given
            Field field = new ShimField();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Boolean;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.AreEqual(field.Title, column.DisplayName);
            Assert.AreEqual(field.InternalName, column.InternalName);
            Assert.AreEqual(field.Required, column.Required);
        }

        [TestMethod]
        public void ChoiceColumn_Map_ReturnsBooleanColumnPOCO()
        {
            //given
            FieldChoice field = new ShimFieldChoice();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Choice;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.IsInstanceOfType(column, typeof(ChoiceColumnPOCO));
        }

        [TestMethod]
        public void ChoiceColumn_Map_AppliesAllProperties()
        {
            //given
            FieldChoice field = new ShimFieldChoice();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Choice;
            setUpShimField();
            ShimFieldMultiChoice.AllInstances.ChoicesGet = (a) => new string[] { "choice1", "zźćó ń", "qwer" };
            //when
            var column = columnMappingResolver.Map(field);
            var specificColumn = column as ChoiceColumnPOCO;
            //then
            Assert.AreEqual(field.Title, column.DisplayName);
            Assert.AreEqual(field.InternalName, column.InternalName);
            Assert.AreEqual(field.Required, column.Required);
            Assert.AreEqual(field.Choices[0], specificColumn.Choices[0]);
            Assert.AreEqual(field.Choices[1], specificColumn.Choices[1]);
            Assert.AreEqual(field.Choices[2], specificColumn.Choices[2]);
        }

        [TestMethod]
        public void CurrencyColumn_Map_ReturnsBooleanColumnPOCO()
        {
            //given
            FieldCurrency field = new ShimFieldCurrency();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Currency;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.IsInstanceOfType(column, typeof(CurrencyColumnPOCO));
        }

        [TestMethod]
        public void CurrencyColumn_Map_AppliesAllProperties()
        {
            //given
            FieldCurrency field = new ShimFieldCurrency();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Currency;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            var specificColumn = column as CurrencyColumnPOCO;
            //then
            Assert.AreEqual(field.Title, column.DisplayName);
            Assert.AreEqual(field.InternalName, column.InternalName);
            Assert.AreEqual(field.Required, column.Required);
            Assert.AreEqual(field.MaximumValue, specificColumn.InternalMaxValue);
            Assert.AreEqual(field.MinimumValue, specificColumn.InternalMinValue);
        }

        [TestMethod]
        public void DateTimeColumn_Map_ReturnsBooleanColumnPOCO()
        {
            //given
            FieldDateTime field = new ShimFieldDateTime();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.DateTime;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.IsInstanceOfType(column, typeof(DateTimeColumnPOCO));
        }

        [TestMethod]
        public void DateTimeColumn_Map_AppliesAllProperties()
        {
            //given
            FieldDateTime field = new ShimFieldDateTime();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.DateTime;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            var specificColumn = column as DateTimeColumnPOCO;
            //then
            Assert.AreEqual(field.Title, column.DisplayName);
            Assert.AreEqual(field.InternalName, column.InternalName);
            Assert.AreEqual(field.Required, column.Required);
        }

        [TestMethod]
        public void MultilineTextColumn_Map_ReturnsBooleanColumnPOCO()
        {
            //given
            FieldMultiLineText field = new ShimFieldMultiLineText();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Note;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.IsInstanceOfType(column, typeof(MultilineTextColumnPOCO));
        }

        [TestMethod]
        public void MultilineTextColumn_Map_AppliesAllProperties()
        {
            //given
            FieldMultiLineText field = new ShimFieldMultiLineText();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Note;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            var specificColumn = column as MultilineTextColumnPOCO;
            //then
            Assert.AreEqual(field.Title, column.DisplayName);
            Assert.AreEqual(field.InternalName, column.InternalName);
            Assert.AreEqual(field.Required, column.Required);
        }

        [TestMethod]
        public void NumberColumn_Map_ReturnsBooleanColumnPOCO()
        {
            //given
            FieldNumber field = new ShimFieldNumber();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Number;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.IsInstanceOfType(column, typeof(NumberColumnPOCO));
        }

        [TestMethod]
        public void NumberColumn_Map_AppliesAllProperties()
        {
            //given
            FieldNumber field = new ShimFieldNumber();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Number;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            var specificColumn = column as NumberColumnPOCO;
            //then
            Assert.AreEqual(field.Title, column.DisplayName);
            Assert.AreEqual(field.InternalName, column.InternalName);
            Assert.AreEqual(field.Required, column.Required);
            Assert.AreEqual(field.MaximumValue, specificColumn.InternalMaxValue);
            Assert.AreEqual(field.MinimumValue, specificColumn.InternalMinValue);
        }

        [TestMethod]
        public void TextColumn_Map_ReturnsBooleanColumnPOCO()
        {
            //given
            FieldText field = new ShimFieldText();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Text;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            //then
            Assert.IsInstanceOfType(column, typeof(TextColumnPOCO));
        }

        [TestMethod]
        public void TextColumn_Map_AppliesAllProperties()
        {
            //given
            FieldText field = new ShimFieldText();
            ShimField.AllInstances.FieldTypeKindGet = (a) => FieldType.Text;
            setUpShimField();
            //when
            var column = columnMappingResolver.Map(field);
            var specificColumn = column as TextColumnPOCO;
            //then
            Assert.AreEqual(field.Title, column.DisplayName);
            Assert.AreEqual(field.InternalName, column.InternalName);
            Assert.AreEqual(field.Required, column.Required);
            Assert.AreEqual(field.MaxLength, specificColumn.InternalMaxLength);
        }

        /// <summary>
        /// Sets up all ShimField objects to return given properties.
        /// </summary>
        /// <param name="title">title of shim field</param>
        private void setUpShimField(string title = "test title", string internalName = "test internal name", bool required = false)
        {
            ShimField.AllInstances.TitleGet = (a) => title;
            ShimField.AllInstances.InternalNameGet = (a) => internalName;
            ShimField.AllInstances.RequiredGet = (a) => required;
        }
    }
}
