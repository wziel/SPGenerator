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
    public class MultilineTextColumnVMUnitTests
    {
        [TestMethod]
        public void TextColumnVM_FromPOCO()
        {
            //given
            var columnPOCO = new MultilineTextColumnPOCO()
            {
                InternalName = "test column name",
                DisplayName = "test column display name",
                Required = true,
                MaxLength = 100,
                MinLength = 10,
            };
            //when
            var columnVM = new MultilineTextColumnVM(columnPOCO);
            //then
            Assert.AreEqual(columnPOCO.InternalName, columnVM.InternalName);
            Assert.AreEqual(columnPOCO.DisplayName, columnVM.DisplayName);
            Assert.AreEqual(columnPOCO.Required, columnVM.Required);
            Assert.AreEqual(columnPOCO.MaxLength, columnVM.MaxLength);
            Assert.AreEqual(columnPOCO.MinLength, columnVM.MinLength);
        }

        [TestMethod]
        public void Validate_MinMaxCondition()
        {
            //given
            var columnVM = new MultilineTextColumnVM()
            {
                InternalName = "test column name",
                MaxLength = 10,
                MinLength = 100
            };
            //when
            var validationResults = columnVM.Validate(null);
            //then
            Assert.AreEqual(1, validationResults.Count());
            Assert.IsTrue(validationResults.First().MemberNames.Contains(nameof(MultilineTextColumnVM.MinLength)));
            Assert.IsTrue(validationResults.First().MemberNames.Contains(nameof(MultilineTextColumnVM.MaxLength)));
        }

        [TestMethod]
        public void Validate_MinCondition()
        {
            //given
            var columnVM = new MultilineTextColumnVM()
            {
                InternalName = "test column name",
                MaxLength = 100,
                MinLength = MultilineTextColumnPOCO.MIN_LENGTH - 1
            };
            //when
            var validationResults = columnVM.Validate(null);
            //then
            Assert.AreEqual(1, validationResults.Count());
            Assert.IsTrue(validationResults.First().MemberNames.Contains(nameof(MultilineTextColumnVM.MinLength)));
        }

        [TestMethod]
        public void Validate_MaxCondition()
        {
            //given
            var columnVM = new MultilineTextColumnVM()
            {
                InternalName = "test column name",
                MaxLength = MultilineTextColumnPOCO.MAX_LENGTH + 1,
                MinLength = 1
            };
            //when
            var validationResults = columnVM.Validate(null);
            //then
            Assert.AreEqual(1, validationResults.Count());
            Assert.IsTrue(validationResults.First().MemberNames.Contains(nameof(MultilineTextColumnVM.MaxLength)));
        }

        [TestMethod]
        public void Validate_GenerateDataWhenRequired()
        {
            //given
            var columnPOCO = new MultilineTextColumnPOCO()
            {
                Required = true
            };
            var columnVM = new MultilineTextColumnVM()
            {
                InternalName = "test column name",
                GenerateData = false,
                MaxLength = 100,
                MinLength = 10
            };
            columnVM.SyncModels(columnPOCO);
            //when
            var validationResults = columnVM.Validate(null);
            //then
            Assert.AreEqual(1, validationResults.Count());
            Assert.IsTrue(validationResults.First().MemberNames.Contains(nameof(MultilineTextColumnVM.GenerateData)));
        }

        [TestMethod]
        public void SyncModels()
        {
            //given
            var columnPOCO = new MultilineTextColumnPOCO();
            var columnVM = new MultilineTextColumnVM()
            {
                MaxLength = 100,
                MinLength = 10
            };
            //when
            columnVM.SyncModels(columnPOCO);
            //then
            Assert.AreEqual(columnPOCO.MinLength, columnVM.MinLength);
            Assert.AreEqual(columnPOCO.MaxLength, columnVM.MaxLength);
        }
    }
}