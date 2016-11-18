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
        private Dictionary<FieldType, ICOlumnMapping> columnMappings = new Dictionary<FieldType, ICOlumnMapping>()
        {
            { FieldType.Text, new TextColumnMapping() },
            { FieldType.Number, new NumberColumnMapping() },
            { FieldType.Note, new MultilineTextColumnMapping() },
            { FieldType.Choice, new ChoiceColumnMapping() },
            { FieldType.DateTime, new DateTimeColumnMapping() },
            { FieldType.Boolean, new BooleanColumnMapping() }
        };

        public ColumnPOCO Map(Field field)
        {
            ICOlumnMapping mapping;
            columnMappings.TryGetValue(field.FieldTypeKind, out mapping);
            if(mapping == null)
            {
                throw new GUIVisibleException("Lista zawiera niewspieraną kolumnę typu " + field.FieldTypeKind);
            }
            return mapping.Map(field);
        }
    }

    public interface IColumnMappingResolver
    {
        ColumnPOCO Map(Field field);
    }
}
