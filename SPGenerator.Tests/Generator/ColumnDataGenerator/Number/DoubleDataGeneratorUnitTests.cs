﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class DoubleDataGeneratorUnitTests
    {
        private NumberColumnPOCO column;
        private DoubleDataGenerator generator;

        [TestInitialize]
        public void TestInitialize()
        {
            column = new NumberColumnPOCO()
            {
                MaxValue = 100,
                MinValue = -10
            };
            generator = new DoubleDataGenerator(column);
        }

        [TestMethod]
        public void GenerateData_ReturnsDoublesOnly()
        {
            //given
            var recordsCount = 100;
            //when
            var data = generator.GenerateData(recordsCount);
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
            var data = generator.GenerateData(recordsCount);
            //then
            Assert.AreEqual(recordsCount, data.Count());
        }

        [TestMethod]
        public void GenerateData_DataPiecesDontExceedMinMax()
        {
            //given
            var recordsCount = 100;
            //when
            var data = generator.GenerateData(recordsCount);
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
