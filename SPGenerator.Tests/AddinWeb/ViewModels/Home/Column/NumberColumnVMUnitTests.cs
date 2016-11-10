﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class NumberColumnVMUnitTests
    {
        [TestMethod]
        public void NumberColumnVM_FromPOCO()
        {
            //given
            var columnPOCO = new NumberColumnPOCO()
            {
                InternalName = "test column name",
                DisplayName = "test column display name",
                Required = true,
                MinValue = 10,
                MaxValue = 100,
                OnlyIntegers = true,
                InternalMinValue = 10,
                InternalMaxValue = 100
            };
            //when
            var columnVM = new NumberColumnVM(columnPOCO);
            //then
            Assert.AreEqual(columnPOCO.InternalName, columnVM.InternalName);
            Assert.AreEqual(columnPOCO.DisplayName, columnVM.DisplayName);
            Assert.AreEqual(columnPOCO.Required, columnVM.Required);
            Assert.AreEqual(columnPOCO.MinValue, columnVM.MinValue);
            Assert.AreEqual(columnPOCO.MaxValue, columnVM.MaxValue);
            Assert.AreEqual(columnPOCO.OnlyIntegers, columnVM.OnlyIntegers);
            Assert.AreEqual(columnPOCO.InternalMinValue, columnVM.InternalMinValue);
            Assert.AreEqual(columnPOCO.InternalMaxValue, columnVM.InternalMaxValue);
        }

        [TestMethod]
        public void ColumnPOCO_TranslatesSelectedProperties()
        {
            //given
            var columnVM = new NumberColumnVM()
            {
                InternalName = "test column name",
                DisplayName = "test column display name",
                MinValue = 10,
                MaxValue = 100,
                OnlyIntegers = true,
            };
            //when
            var columnPOCO = columnVM.ColumnPOCO as NumberColumnPOCO;
            //then
            Assert.AreEqual(columnPOCO.InternalName, columnVM.InternalName);
            Assert.AreEqual(columnPOCO.DisplayName, columnVM.DisplayName);
            Assert.AreEqual(columnPOCO.MinValue, columnVM.MinValue);
            Assert.AreEqual(columnPOCO.MaxValue, columnVM.MaxValue);
            Assert.AreEqual(columnPOCO.OnlyIntegers, columnVM.OnlyIntegers);
        }

        [TestMethod]
        public void Validate_MinMaxCondition()
        {
            //given
            var columnVM = new NumberColumnVM()
            {
                InternalName = "test column name",
                MinValue = 100,
                MaxValue = 10,
                OnlyIntegers = true,
            };
            //when
            var validationResults = columnVM.Validate(null);
            //then
            Assert.AreEqual(validationResults.Count(), 1);
            Assert.AreEqual(validationResults.First().ErrorMessage, "Minimalna wartość nie może być większa niż maksymalna");
        }
    }
}
