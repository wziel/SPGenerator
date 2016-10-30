using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.SharePoint.ColumnMapping
{
    internal abstract class ColumnMapping
    {
        public abstract ColumnPOCO Map(Field field);
    }
}
