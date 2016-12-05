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
        private HashSet<FieldType> unsupportedFieldTypes = new HashSet<FieldType>()
        {
            FieldType.AllDayEvent,
            FieldType.Attachments,
            FieldType.Calculated,
            FieldType.Computed,
            FieldType.ContentTypeId,
            FieldType.Counter,
            FieldType.CrossProjectLink,
            FieldType.Error,
            FieldType.File,
            FieldType.Geolocation,
            FieldType.GridChoice,
            FieldType.Guid,
            FieldType.Integer,
            FieldType.Invalid,
            FieldType.Lookup,
            FieldType.MaxItems,
            FieldType.ModStat,
            FieldType.MultiChoice,
            FieldType.OutcomeChoice,
            FieldType.PageSeparator,
            FieldType.Recurrence,
            FieldType.ThreadIndex,
            FieldType.Threading,
            FieldType.URL,
            FieldType.User,
            FieldType.WorkflowEventType,
            FieldType.WorkflowStatus,
        };

        private Dictionary<FieldType, IColumnMapping> columnMappings = new Dictionary<FieldType, IColumnMapping>()
        {
            { FieldType.Boolean, new BooleanColumnMapping() },
            { FieldType.Choice, new ChoiceColumnMapping() },
            { FieldType.Currency, new CurrencyColumnMapping() },
            { FieldType.DateTime, new DateTimeColumnMapping() },
            { FieldType.Note, new MultilineTextColumnMapping() },
            { FieldType.Number, new NumberColumnMapping() },
            { FieldType.Text, new TextColumnMapping() },
        };

        public ColumnPOCO Map(Field field)
        {
            IColumnMapping mapping;
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
