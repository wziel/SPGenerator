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
    public class NegativeRandomDoubleDataGeneratorUnitTests
    {
        private NumberColumnPOCO column;
        private NegativeRandomDoubleDataGenerator generator;

        [TestInitialize]
        public void TestInitialize()
        {
            column = new NumberColumnPOCO()
            {
                MaxValue = 100,
                MinValue = -10
            };
            generator = new NegativeRandomDoubleDataGenerator();
        }

        [TestMethod]
        public void CanGenerateData_FalseIfColumnMinGreaterThanZero()
        {
            //given
            column.MinValue = 0;
            //when
            var canGenerate = generator.CanGenerateData(column);
            //then
            Assert.IsFalse(canGenerate);
        }

        [TestMethod]
        public void CanGenerateData_FalseIfOnlyIntegers()
        {
            //given
            column.OnlyIntegers = true;
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
        public void GenerateData_ReturnsDoubles()
        {
            //given
            var recordsCount = 100;
            //when
            var data = generator.GenerateData(column, recordsCount);
            //then
            foreach (var dataPiece in data)
            {
                Assert.IsTrue(dataPiece is double);
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
                var doubleVale = (double)dataPiece;
                Assert.IsTrue(column.MinValue <= doubleVale);
                Assert.IsTrue(0 > doubleVale);
            }
        }
    }
}
