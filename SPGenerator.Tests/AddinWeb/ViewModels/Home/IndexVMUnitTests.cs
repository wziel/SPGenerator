using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.AddinWeb.ViewModels.Home;
using SPGenerator.AddinWeb.ViewModels.Home.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.AddinWeb.ViewModels.Home
{
    [TestClass]
    public class IndexVMUnitTests
    {
        [TestMethod]
        public void AllColumnVMs_ReturnsAllColumns()
        {
            //given
            var vm = new IndexVM()
            {
                NumberColumnVMs = new List<NumberColumnVM>() { new NumberColumnVM() },
                TextColumnVMs = new List<TextColumnVM>() { new TextColumnVM() }
            };
            //when
            var columns = vm.AllColumnVMs;
            //then
            Assert.AreEqual(2, columns.Count);
        }
    }
}
