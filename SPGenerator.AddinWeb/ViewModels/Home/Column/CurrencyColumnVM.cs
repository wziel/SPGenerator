using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class CurrencyColumnVM : NumberColumnVM, ICurrencyColumnVM
    {
        public CurrencyColumnVM()
        {
            //for razor
        }
        public CurrencyColumnVM(CurrencyColumnPOCO column) : base(column)
        {
            //left empty
        }
    }

    public interface ICurrencyColumnVM : INumberColumnVM
    {
        //left empty
    }
}