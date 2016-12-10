using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.Generator.ColumnDataGenerator.Choice;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.Generator.ColumnDataGenerator.Choice
{
    [TestClass]
    public class RandomChoiceDataGeneratorUnitTests
    {
        RandomChoiceDataGenerator generator;

        [TestInitialize]
        public void TestInitialize()
        {
            generator = new RandomChoiceDataGenerator();
        }

        [TestMethod]
        public void CanGenerateData_WhenGenerateDataTrue()
        {
            //given
            var columnPOCO = new ChoiceColumnPOCO() { GenerateData = true };
            //then
            Assert.IsTrue(generator.CanGenerateData(columnPOCO));
        }

        [TestMethod]
        public void CanGenerateData_WhenGenerateDataFalse()
        {
            //given
            var columnPOCO = new ChoiceColumnPOCO() { GenerateData = false };
            //then
            Assert.IsFalse(generator.CanGenerateData(columnPOCO));
        }

        [TestMethod]
        public void GenerateData_ReturnsExactNumberOfRecords()
        {
            //given
            var columnPOCO = new ChoiceColumnPOCO()
            {
                Choices = new string[] { "test", "choice test", "źśćż ó", "" }
            };
            var recordsCount = 123;
            //when
            var data = generator.GenerateData(columnPOCO, recordsCount);
            //then
            Assert.AreEqual(recordsCount, data.Count());
        }

        [TestMethod]
        public void GenerateData_ReturnsChoiceOptionsOnly()
        {
            //given
            var columnPOCO = new ChoiceColumnPOCO()
            {
                Choices = new string[] { "test", "choice test", "źśćż ó", "" }
            };
            var recordsCount = 123;
            //when
            var data = generator.GenerateData(columnPOCO, recordsCount);
            //then
            foreach (var dataPiece in data)
            {
                Assert.IsInstanceOfType(dataPiece, typeof(string));
                var text = (string)dataPiece;
                Assert.IsTrue(columnPOCO.Choices.Contains(text));
            }
        }
    }
}
