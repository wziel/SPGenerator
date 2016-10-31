using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class ColumnVMFactory
    {
        private static Dictionary<Type, Func<ColumnPOCO, ColumnVM>> mapping =
            new Dictionary<Type, Func<ColumnPOCO, ColumnVM>>
            {
                { typeof(TextColumnPOCO), (c) => new TextColumnVM((TextColumnPOCO)c) },
                { typeof(NumberColumnPOCO), (c) => new NumberColumnVM((NumberColumnPOCO)c) },
            };

        public static ColumnVM GetColumnVM(ColumnPOCO columnPOCO)
        {
            return mapping[columnPOCO.GetType()].Invoke(columnPOCO);
        }
    }
}