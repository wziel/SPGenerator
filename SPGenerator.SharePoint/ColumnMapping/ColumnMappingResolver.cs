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
        private Dictionary<Type, ICOlumnMapping> columnMappings = new Dictionary<Type, ICOlumnMapping>()
        {
            { typeof(FieldText), new TextColumnMapping() },
            { typeof(FieldNumber), new NumberColumnMapping() },
            { typeof(FieldMultiLineText), new MultilineTextColumnMapping() },
            { typeof(FieldChoice), new ChoiceColumnMapping() },
            { typeof(FieldDateTime), new DateTimeColumnMapping() },
        };

        public ColumnPOCO Map(Field field)
        {
            ICOlumnMapping mapping;
            columnMappings.TryGetValue(field.GetType(), out mapping);
            if(mapping == null)
            {
                throw new GUIVisibleException("Lista zawiera niewspieraną kolumnę typu " + field.GetType());
            }
            return mapping.Map(field);
        }
    }

    public interface IColumnMappingResolver
    {
        ColumnPOCO Map(Field field);
    }
}
