using SPGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.SharePoint.ColumnMapping
{
    public class ColumnMappingNotFoundException : GUIVisibleException
    {
        public ColumnMappingNotFoundException(Type columnType) : base("Lista zawiera niewspieraną kolumnę typu" + columnType.Name)
        {
        }
    }
}
