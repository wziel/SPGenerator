using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    class NumberColumnMapping : ColumnMapping
    {
        public override ColumnPOCO Map(Field field)
        {
            return new NumberColumnPOCO()
            {
                ColumnName = field.Title,
            };
        }
    }
}
