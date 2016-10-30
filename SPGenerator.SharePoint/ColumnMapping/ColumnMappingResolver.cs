using Microsoft.SharePoint.Client;
using SPGenerator.Model;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.SharePoint.ColumnMapping
{
    public class ColumnMappingResolver : IColumnMappingResolver
    {
        private Dictionary<Type, ColumnMapping> columnMappings = new Dictionary<Type, ColumnMapping>()
        {
            { typeof(FieldText), new TextColumnMapping() },
            { typeof(FieldNumber), new NumberColumnMapping() },
        };

        public ColumnPOCO Map(Field field)
        {
            ColumnMapping mapping;
            columnMappings.TryGetValue(field.GetType(), out mapping);
            if(mapping == null)
            {
                throw new GUIVisibleException("Lista zawiera niewspieraną kolumnę typu "
                    + field.GetType().ToString());
            }
            return mapping.Map(field);
        }
    }

    public interface IColumnMappingResolver
    {
        ColumnPOCO Map(Field field);
    }
}
