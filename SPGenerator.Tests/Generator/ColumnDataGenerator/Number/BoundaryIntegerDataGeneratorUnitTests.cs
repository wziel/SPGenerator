using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.Generator.ColumnDataGenerator.Number;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.Generator.ColumnDataGenerator.Number
{
    [TestClass]
    public class BoundaryIntegerDataGeneratorUnitTests
    {
        private NumberColumnPOCO column;
        private BoundaryIntegerDataGenerator generator;

        [TestInitialize]
        public void TestInitialize()
        {
            column = new NumberColumnPOCO()
            {
                MaxValue = 100,
                MinValue = -10,
                GenerateData = true
            };
            generator = new BoundaryIntegerDataGenerator();
        }

        [TestMethod]
        public void CanGenerateData_FalseIfNoIntegersInRange()
        {
            //given
            column.MaxValue = 10.9;
            column.MinValue = 10.1;
            //when
            var canGenerate = generator.CanGenerateData(column);
            //then
            Assert.IsFalse(canGenerate);
        }

        [TestMethod]
        public void CanGenerateData_TrueIfOnlyIntegers()
        {
            //given
            column.OnlyIntegers = true;
            //when
            var canGenerate = generator.CanGenerateData(column);
            //then
            Assert.IsTrue(canGenerate);
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
        public void GenerateData_ReturnsInts()
        {
            //given
            var recordsCount = 100;
            //when
            var data = generator.GenerateData(column, recordsCount);
            //then
            foreach (var dataPiece in data)
            {
                Assert.IsTrue(dataPiece is int);
            }
        }

        [TestMethod]
        public void GenerateData_ExactCount()
        {
            //given
            var recordsCount = 100;
            //when
            var data = generator.GenerateData(column, recordsCount);
            //then
            Assert.AreEqual(recordsCount, data.Count());
        }

        [TestMethod]
        public void GenerateData_DataPiecesDontExceedMinMax()
        {
            //given
            var recordsCount = 100;
            //when
            var data = generator.GenerateData(column, recordsCount);
            //then
            foreach (var dataPiece in data)
            {
                var intValue = (int)dataPiece;
                Assert.IsTrue(column.MinValue <= intValue);
                Assert.IsTrue(column.MaxValue >= intValue);
            }
        }
    }
}
