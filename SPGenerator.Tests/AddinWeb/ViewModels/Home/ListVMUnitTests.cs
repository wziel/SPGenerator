using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.AddinWeb.ViewModels.Home;
using SPGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.AddinWeb.ViewModels.Home
{
    [TestClass]
    public class ListVMUnitTests
    {
        [TestMethod]
        public void ListVM_FromPOCO()
        {
            //given
            var listPOCO = new ListPOCO()
            {
                Title = "list test title",
                ServerRelativeUrl = "server realtive url of list"
            };
            //when
            var listVM = new ListVM(listPOCO);
            //then
            Assert.AreEqual(listVM.Title, listPOCO.Title);
            Assert.AreEqual(listVM.ServerRelativeUrl, listPOCO.ServerRelativeUrl);
        }

        [TestMethod]
        public void ListPOCO_Get()
        {
            //given
            var listVM = new ListVM()
            {
                Title = "list test title",
                ServerRelativeUrl = "server realtive url of list"
            };
            //when
            var listPOCO = listVM.ListPOCO;
            //then
            Assert.AreEqual(listVM.Title, listPOCO.Title);
            Assert.AreEqual(listVM.ServerRelativeUrl, listPOCO.ServerRelativeUrl);
        }
    }
}
