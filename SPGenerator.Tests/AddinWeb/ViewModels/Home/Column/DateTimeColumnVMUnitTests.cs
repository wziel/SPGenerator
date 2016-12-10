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
    public class DateTimeColumnVMUnitTests
    {
        [TestMethod]
        public void DateTimeColumnVM_FromPOCO()
        {
            //given
            var columnPOCO = new DateTimeColumnPOCO()
            {
                MinValue = new DateTime(2000, 1, 1),
                MaxValue = new DateTime(2010, 1, 1)
            };
            //when
            var columnVM = new DateTimeColumnVM(columnPOCO);
            //then
            Assert.AreEqual(columnPOCO.MinValue, columnVM.MinValue);
            Assert.AreEqual(columnPOCO.MaxValue, columnVM.MaxValue);
        }

        [TestMethod]
        public void ApplyTo()
        {
            //given
            var columnPOCO = new DateTimeColumnPOCO();
            var columnVM = new DateTimeColumnVM()
            {
                MinValue = new DateTime(2000, 1, 1),
                MaxValue = new DateTime(2010, 1, 1)
            };
            //when
            columnVM.ApplyTo(columnPOCO);
            //then
            Assert.AreEqual(columnVM.MinValue, columnPOCO.MinValue);
            Assert.AreEqual(columnVM.MaxValue, columnPOCO.MaxValue);
        }

        [TestMethod]
        public void Validate_MinMaxCondition()
        {
            //given
            var columnVM = new DateTimeColumnVM()
            {
                MinValue = new DateTime(2010, 1, 1),
                MaxValue = new DateTime(200, 1, 1)
            };
            //when
            var validationResults = columnVM.Validate(null);
            //then
            Assert.AreEqual(1, validationResults.Count());
            Assert.IsTrue(validationResults.First().MemberNames.Contains(nameof(DateTimeColumnVM.MaxValue)));
            Assert.IsTrue(validationResults.First().MemberNames.Contains(nameof(DateTimeColumnVM.MinValue)));
        }

        [TestMethod]
        public void Validate_GenerateDataWhenRequired()
        {
            //given
            var columnPOCO = new DateTimeColumnPOCO()
            {
                Required = true
            };
            var columnVM = new DateTimeColumnVM()
            {
                InternalName = "test column name",
                GenerateData = false,
                MinValue = new DateTime(2000, 1, 1),
                MaxValue = new DateTime(2010, 1, 1)
            };
            columnVM.ApplyTo(columnPOCO);
            //when
            var validationResults = columnVM.Validate(null);
            //then
            Assert.AreEqual(1, validationResults.Count());
            Assert.IsTrue(validationResults.First().MemberNames.Contains(nameof(DateTimeColumnVM.GenerateData)));
        }
    }
}
