using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.Generator.ColumnDataGenerator.Boolean;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.Generator.ColumnDataGenerator.Boolean
{
    [TestClass]
    public class TrueBooleanDataGeneratorUnitTests
    {
        TrueBooleanDataGenerator generator;

        [TestInitialize]
        public void TestInitialize()
        {
            generator = new TrueBooleanDataGenerator();
        }

        [TestMethod]
        public void CanGenerateData_WhenGenerateDataTrue()
        {
            //given
            var columnPOCO = new BooleanColumnPOCO() { GenerateData = true };
            //then
            Assert.IsTrue(generator.CanGenerateData(columnPOCO));
        }

        [TestMethod]
        public void CanGenerateData_WhenGenerateDataFalse()
        {
            //given
            var columnPOCO = new BooleanColumnPOCO() { GenerateData = false };
            //then
            Assert.IsFalse(generator.CanGenerateData(columnPOCO));
        }

        [TestMethod]
        public void GenerateData_ReturnsExactNumberOfRecords()
        {
            //given
            var recordsCount = 123;
            //when
            var records = generator.GenerateData(null, recordsCount);
            //then
            Assert.AreEqual(recordsCount, records.Count());
        }

        [TestMethod]
        public void GenerateData_ReturnsTrueOnly()
        {
            //given
            var recordsCount = 123;
            //when
            var records = generator.GenerateData(null, recordsCount);
            //then
            foreach (var record in records)
            {
                Assert.IsInstanceOfType(record, typeof(bool));
                var value = (bool)record;
                Assert.IsTrue(value);
            }
        }
    }
}
