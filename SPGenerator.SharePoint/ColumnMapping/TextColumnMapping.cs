using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    class TextColumnMapping : ColumnMapping<FieldText, TextColumnPOCO>
    {
        protected override void ApplyProperies(TextColumnPOCO column, FieldText field)
        {
            base.ApplyProperies(column, field);
            column.MinLength = TextColumnPOCO.MIN_LENGTH;
            column.MaxLength = Math.Min(TextColumnPOCO.MAX_LENGTH, field.MaxLength);
            column.InternalMaxLength = Math.Min(TextColumnPOCO.MAX_LENGTH, field.MaxLength);
        }

        protected override TextColumnPOCO CreateColumnPOCO()
        {
            return new TextColumnPOCO();
        }
    }
}
