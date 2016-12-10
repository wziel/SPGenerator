using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.Generator.ColumnDataGenerator;
using SPGenerator.Generator.ColumnDataGenerator.Shared;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.Generator.ColumnDataGenerator.Shared
{
    [TestClass]
    public class NullDataGeneratorUnitTests
    {
        private ColumnPOCO column;
        private NullDataGenerator generator;

        [TestInitialize]
        public void TestInitialize()
        {
            column = new NumberColumnPOCO() { GenerateData = true };
            generator = new NullDataGenerator();
        }

        [TestMethod]
        public void CanGenerateData_FalseIfColumnRequired()
        {
            //given
            column.Required = true;
            //when
            var canGenerate = generator.CanGenerateData(column);
            //then
            Assert.IsFalse(canGenerate);
        }

        [TestMethod]
        public void CanGenerateData_FalseIfFalseGenerateData()
        {
            //given
            column.GenerateData = false;
            //when
            var canGenerate = generator.CanGenerateData(column);
            //then
            Assert.IsFalse(canGenerate);
        }

        [TestMethod]
        public void CanGenerateData_TrueIfTrueGenerateData()
        {
            //given
            column.GenerateData = true;
            //when
            var canGenerate = generator.CanGenerateData(column);
            //then
            Assert.IsTrue(canGenerate);
        }

        [TestMethod]
        public void GenerateData_ReturnsNullValuesOnly()
        {
            //given
            var recordsCount = 100;
            //when
            var data = generator.GenerateData(null, recordsCount);
            //then
            Assert.AreEqual(recordsCount, data.Count());
            foreach (var dataPiece in data)
            {
                Assert.IsNull(dataPiece);
            }
        }

        [TestMethod]
        public void GenerateData_ReturnsExactCount()
        {
            //given
            var recordsCount = 100;
            //when
            var data = generator.GenerateData(null, recordsCount);
            //then
            Assert.AreEqual(recordsCount, data.Count());
        }
    }
}
