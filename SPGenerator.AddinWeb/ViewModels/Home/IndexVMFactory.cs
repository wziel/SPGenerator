﻿using SPGenerator.AddinWeb.ViewModels.Home.Column;
using SPGenerator.Model;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPGenerator.AddinWeb.ViewModels.Home
{
    public class IndexVMFactory : IIndexVMFactory
    {
        private static Dictionary<Type, Action<IndexVM, ColumnPOCO>> addColumnStrategies
            = new Dictionary<Type, Action<IndexVM, ColumnPOCO>>()
            {
                { typeof(NumberColumnPOCO), (indexVM, column) => indexVM.NumberColumnVMs.Add(new NumberColumnVM((NumberColumnPOCO) column)) },
                { typeof(TextColumnPOCO), (indexVM, column) => indexVM.TextColumnVMs.Add(new TextColumnVM((TextColumnPOCO) column)) },
            };

        public IndexVM GetDefaultIndexVM(List<ListPOCO> allLists, string hostWebUrl)
        {
            return new IndexVM()
            {
                ListVMs = allLists.Select(l => new ListVM(l)).ToList(),
                HostWebUrl = hostWebUrl,
                RecordsToGenerateCount = 10,
            };
        }
        public IndexVM GetIndexVMWithSelectedList(IndexVM indexVM, ListPOCO selectedList)
        {
            indexVM.SelectedListVM = new ListVM(selectedList);
            selectedList.ColumnPOCOList.ForEach(c => addColumnStrategies[c.GetType()].Invoke(indexVM, c));
            return indexVM;
        }
    }

    public interface IIndexVMFactory
    {
        IndexVM GetDefaultIndexVM(List<ListPOCO> allLists, string hostWebUrl);
        IndexVM GetIndexVMWithSelectedList(IndexVM indexVM, ListPOCO selectedList);
    }
}