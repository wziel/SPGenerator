using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class BooleanColumnVM : ColumnVM<BooleanColumnPOCO>, IBooleanColumnVM
    {
        public BooleanColumnVM()
        {
            //for razor
        }

        public BooleanColumnVM(BooleanColumnPOCO columnPOCO) : base(columnPOCO)
        {
            //left empty
        }
    }

    public interface IBooleanColumnVM : IColumnVM
    {
        //left empty
    }
}