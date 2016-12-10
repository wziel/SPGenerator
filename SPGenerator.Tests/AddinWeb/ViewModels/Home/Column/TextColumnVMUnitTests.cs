using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.AddinWeb.ViewModels.Home.Column;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.AddinWeb.ViewModels.Home.Column
{
    [TestClass]
    public class TextColumnVMUnitTests
    {
        [TestMethod]
        public void TextColumnVM_FromPOCO()
        {
            //given
            var columnPOCO = new TextColumnPOCO()
            {
                InternalName = "test column name",
                DisplayName = "test column display name",
                Required = true,
                MaxLength = 100,
                MinLength = 10,
                InternalMaxLength = 100
            };
            //when
            var columnVM = new TextColumnVM(columnPOCO);
            //then
            Assert.AreEqual(columnPOCO.InternalName, columnVM.InternalName);
            Assert.AreEqual(columnPOCO.DisplayName, columnVM.DisplayName);
            Assert.AreEqual(columnPOCO.Required, columnVM.Required);
            Assert.AreEqual(columnPOCO.MaxLength, columnVM.MaxLength);
            Assert.AreEqual(columnPOCO.MinLength, columnVM.MinLength);
            Assert.AreEqual(columnPOCO.InternalMaxLength, columnVM.InternalMaxLength);
        }

        [TestMethod]
        public void Validate_MinMaxCondition()
        {
            //given
            var columnVM = new TextColumnVM()
            {
                InternalName = "test column name",
                MaxLength = 10,
                MinLength = 100
            };
            //when
            var validationResults = columnVM.Validate(null);
            //then
            Assert.AreEqual(validationResults.Count(), 1);
            Assert.IsTrue(validationResults.First().MemberNames.Contains(nameof(TextColumnVM.MaxLength)));
            Assert.IsTrue(validationResults.First().MemberNames.Contains(nameof(TextColumnVM.MinLength)));
        }
    }
}
