using System;
using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    internal class CurrencyColumnMapping : NumberColumnMapping
    {
        protected override NumberColumnPOCO CreateColumnPOCO()
        {
            return new CurrencyColumnPOCO();
        }
    }
}