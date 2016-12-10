using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SPGenerator.Generator.ColumnDataGenerator.MultilineText;
using SPGenerator.Generator.ColumnDataGenerator.Text;
using SPGenerator.Generator.DAO;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.Generator.ColumnDataGenerator.Text
{
    [TestClass]
    public class TextDataGeneratorUnitTests
    {
        ITextDAO mockTextDAO;
        TextDataGenerator generator;

        [TestInitialize]
        public void TestInitialize()
        {
            mockTextDAO = Substitute.For<ITextDAO>();
            mockTextDAO.GetRandomTexts(Arg.Any<int>()).Returns(new List<string>
            {
                "test test test testsets ets ",
                "text1123423 4123 41234 1234 123 4",
                "żźćóś ńę adf asg ew gh sf ąśążśąóąśóą",
            });
            generator = new TextDataGenerator(mockTextDAO);
        }

        [TestMethod]
        public void CanGenerateData_WhenGenerateDataTrue()
        {
            //given
            var columnPOCO = new TextColumnPOCO() { GenerateData = true };
            //then
            Assert.IsTrue(generator.CanGenerateData(columnPOCO));
        }

        [TestMethod]
        public void CanGenerateData_WhenGenerateDataFalse()
        {
            //given
            var columnPOCO = new TextColumnPOCO() { GenerateData = false };
            //then
            Assert.IsFalse(generator.CanGenerateData(columnPOCO));
        }

        [TestMethod]
        public void GenerateData_ReturnsExactNumberOfRecords()
        {
            //given
            var columnPOCO = new TextColumnPOCO()
            {
                MinLength = 5,
                MaxLength = 15
            };
            var recordsCount = 123;
            //when
            var data = generator.GenerateData(columnPOCO, recordsCount);
            //then
            Assert.AreEqual(recordsCount, data.Count());
        }

        [TestMethod]
        public void GenerateData_ReturnsOnlyTextOfLengthSpecifiedInColumn()
        {
            //given
            var columnPOCO = new TextColumnPOCO()
            {
                MinLength = 5,
                MaxLength = 15
            };
            var recordsCount = 123;
            //when
            var data = generator.GenerateData(columnPOCO, recordsCount);
            //then
            foreach (var dataPiece in data)
            {
                Assert.IsInstanceOfType(dataPiece, typeof(string));
                var stringData = (string)dataPiece;
                Assert.IsTrue(stringData.Length <= columnPOCO.MaxLength);
                Assert.IsTrue(stringData.Length >= columnPOCO.MinLength);
            }
        }
    }
}
