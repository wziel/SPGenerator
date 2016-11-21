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
    public class RandomDoubleDataGeneratorUnitTests
    {
        private NumberColumnPOCO column;
        private AbstractRandomDoubleDataGenerator generator;

        [TestInitialize]
        public void TestInitialize()
        {
            column = new NumberColumnPOCO()
            {
                MaxValue = 100,
                MinValue = -10
            };
            generator = new AbstractRandomDoubleDataGenerator();
        }

        [TestMethod]
        public void GenerateData_ReturnsDoublesOnly()
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
            foreach(var dataPiece in data)
            {
                var doubleVale = (double)dataPiece;
                Assert.IsTrue(column.MinValue <= doubleVale);
                Assert.IsTrue(column.MaxValue >= doubleVale);
            }
        }
    }
}
