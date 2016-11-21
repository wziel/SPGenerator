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
                TextColumnVMs = new List<TextColumnVM>() { new TextColumnVM() },
                MultilineTextColumnVMs = new List<MultilineTextColumnVM>() { new MultilineTextColumnVM() },
                ChoiceColumnVMs = new List<ChoiceColumnVM>() { new ChoiceColumnVM() },
                BooleanColumnVMs = new List<BooleanColumnVM>() {  new BooleanColumnVM() },
                DateTimeColumnVMs = new List<DateTimeColumnVM>() { new DateTimeColumnVM() },
            };
            //when
            var columns = vm.AllColumnVMs;
            //then
            Assert.AreEqual(6, columns.Count);
        }

        [TestMethod]
        public void SelectedListAbsoluteUrl_ReturnsProperListUrl()
        {
            //given
            var listVM = new ListVM() { ServerRelativeUrl = "/sites/lists/list" };
            var indexVM = new IndexVM()
            {
                SelectedListVM = listVM,
                HostWebUrl = "http://hostWebUtl/sites/testsite"
            };
            //when
            var listUrl = indexVM.SelectedListAbsoluteUrl;
            //then
            Assert.AreEqual("http://hostWebUtl/sites/lists/list", listUrl);
        }

        [TestMethod]
        public void ShowListVMs_FalseWhenNoLists()
        {
            //given
            var indexVM = new IndexVM() { ListVMs = new List<ListVM>() };
            //when
            var showLists = indexVM.ShowListVMs;
            //then
            Assert.IsFalse(showLists);
        }

        [TestMethod]
        public void ShowListVMs_FalseWhenListsIsNull()
        {
            //given
            var indexVM = new IndexVM() { ListVMs = null };
            //when
            var showLists = indexVM.ShowListVMs;
            //then
            Assert.IsFalse(showLists);
        }

        [TestMethod]
        public void ShowListVMs_TrueWhenListsNotEmpty()
        {
            //given
            var indexVM = new IndexVM() { ListVMs = new List<ListVM>() { new ListVM() } };
            //when
            var showLists = indexVM.ShowListVMs;
            //then
            Assert.IsTrue(showLists);
        }

        [TestMethod]
        public void ShowColumnVMs_FalseWhenNoColumns()
        {
            //given
            var indexVM = new IndexVM();
            //when
            var showColumns = indexVM.ShowColumnVMs;
            //then
            Assert.IsFalse(showColumns);
        }

        [TestMethod]
        public void ShowColumnVMs_TrueWhenColumnsPresent()
        {
            //given
            var indexVM = new IndexVM() { ChoiceColumnVMs = new List<ChoiceColumnVM>() { new ChoiceColumnVM() } };
            //when
            var showColumns = indexVM.ShowColumnVMs;
            //then
            Assert.IsTrue(showColumns);
        }
    }
}
