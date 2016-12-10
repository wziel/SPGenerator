using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.Generator.ColumnDataGenerator.DateTime;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.Generator.ColumnDataGenerator.DateTime
{
    [TestClass]
    public class RandomDateTimeDataGeneratorUnitTests
    {
        RandomDateTimeDataGenerator generator;

        [TestInitialize]
        public void TestInitialize()
        {
            generator = new RandomDateTimeDataGenerator();
        }

        [TestMethod]
        public void CanGenerateData_WhenGenerateDataTrue()
        {
            //given
            var columnPOCO = new DateTimeColumnPOCO() { GenerateData = true };
            //then
            Assert.IsTrue(generator.CanGenerateData(columnPOCO));
        }

        [TestMethod]
        public void CanGenerateData_WhenGenerateDataFalse()
        {
            //given
            var columnPOCO = new DateTimeColumnPOCO() { GenerateData = false };
            //then
            Assert.IsFalse(generator.CanGenerateData(columnPOCO));
        }

        [TestMethod]
        public void GenerateData_GeneratesExactNumberOfRecords()
        {
            //given
            var columnPOCO = new DateTimeColumnPOCO()
            {
                MinValue = new System.DateTime(2010, 1, 1),
                MaxValue = new System.DateTime(2020, 1, 1)
            };
            var recordsCount = 123;
            //when
            var records = generator.GenerateData(columnPOCO, recordsCount);
            //then
            Assert.AreEqual(recordsCount, records.Count());
        }

        [TestMethod]
        public void GenerateData_AllAreInSpecifiedRange()
        {
            //given
            var columnPOCO = new DateTimeColumnPOCO()
            {
                MinValue = new System.DateTime(2010, 1, 1),
                MaxValue = new System.DateTime(2020, 1, 1)
            };
            var recordsCount = 123;
            //when
            var records = generator.GenerateData(columnPOCO, recordsCount);
            //then
            foreach (var record in records)
            {
                Assert.IsInstanceOfType(record, typeof(System.DateTime));
                var dateTime = (System.DateTime)record;
                Assert.IsTrue(dateTime <= columnPOCO.MaxValue);
                Assert.IsTrue(dateTime >= columnPOCO.MinValue);
            }
        }
    }
}
