using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPGenerator.AddinWeb.ViewModels.Home;
using SPGenerator.AddinWeb.ViewModels.Home.Column;
using SPGenerator.Model;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests.AddinWeb.ViewModels.Home
{
    [TestClass]
    public class IndexVMFactoryUnitTests
    {
        private IndexVMFactory indexVMFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            indexVMFactory = new IndexVMFactory();
        }

        [TestMethod]
        public void GetDefaultIndexVM_AssignsEmptyLists()
        {
            //given
            var lists = new List<Model.ListPOCO>();
            var hostWebUrl = "";
            //when
            var indexVM = indexVMFactory.GetDefaultIndexVM(lists, hostWebUrl);
            //
            Assert.IsFalse(indexVM.ListVMs.Any());
        }

        [TestMethod]
        public void GetDefaultIndexVM_AssignsNotEmptyLists()
        {
            //given
            var lists = new List<Model.ListPOCO>() { new Model.ListPOCO() };
            var hostWebUrl = "";
            //when
            var indexVM = indexVMFactory.GetDefaultIndexVM(lists, hostWebUrl);
            //
            Assert.AreEqual(1, indexVM.ListVMs.Count);
        }

        [TestMethod]
        public void GetDefaultIndexVM_AssignsHostWebUrl()
        {
            //given
            var lists = new List<Model.ListPOCO>() { new Model.ListPOCO() };
            var hostWebUrl = "host web url test";
            //when
            var indexVM = indexVMFactory.GetDefaultIndexVM(lists, hostWebUrl);
            //
            Assert.AreEqual(hostWebUrl, indexVM.HostWebUrl);
        }

        [TestMethod]
        public void GetDefaultIndexVM_AssignsRecordsToGenerateDefaultValue()
        {
            //given
            var lists = new List<Model.ListPOCO>() { new Model.ListPOCO() };
            var hostWebUrl = "";
            //when
            var indexVM = indexVMFactory.GetDefaultIndexVM(lists, hostWebUrl);
            //
            Assert.AreEqual(10, indexVM.RecordsToGenerateCount);
        }

        [TestMethod]
        public void GetIndexVMWithSelectedList_AssignsProperties()
        {
            //given
            var list = new ListPOCO()
            {
                Title = "test title",
                ColumnPOCOList = new List<Model.Column.ColumnPOCO>()
                {
                    new NumberColumnPOCO() { InternalName = "column pooc name" }
                }
            };
            var indexVM = new IndexVM();
            //when
            indexVM = indexVMFactory.GetIndexVMWithSelectedList(indexVM, list);
            //then
            Assert.AreEqual(list.Title, indexVM.SelectedListVM.Title);
            Assert.AreEqual(indexVM.NumberColumnVMs[0].InternalName, list.ColumnPOCOList[0].InternalName);
        }
    }
}
