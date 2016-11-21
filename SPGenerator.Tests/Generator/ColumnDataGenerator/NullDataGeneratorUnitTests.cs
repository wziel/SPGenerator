using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.Generator.ColumnDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.Generator.ColumnDataGenerator
{
    [TestClass]
    public class NullDataGeneratorUnitTests
    {
        private NullDataGenerator generator;

        [TestInitialize]
        public void TestInitialize()
        {
            generator = new NullDataGenerator();
        }

        [TestMethod]
        public void GenerateData_ReturnsNullValuesOnly()
        {
            //given
            var recordsCount = 10;
            //when
            var data = generator.GenerateData(null, recordsCount);
            //then
            Assert.AreEqual(recordsCount, data.Count());
            foreach(var dataPiece in data)
            {
                Assert.IsNull(dataPiece);
            }
        }
    }
}
