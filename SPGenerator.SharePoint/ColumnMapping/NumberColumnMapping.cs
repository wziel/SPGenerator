using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    class NumberColumnMapping : ColumnMapping<FieldNumber, NumberColumnPOCO>
    {
        protected override void ApplyProperies(NumberColumnPOCO column, FieldNumber field)
        {
            base.ApplyProperies(column, field);
            column.InternalMaxValue = Math.Min(field.MaximumValue, NumberColumnPOCO.MAX_VALUE);
            column.InternalMinValue = Math.Max(field.MinimumValue, NumberColumnPOCO.MIN_VALUE);
            column.MinValue = Math.Max(field.MinimumValue, NumberColumnPOCO.MIN_VALUE);
            column.MaxValue = Math.Min(field.MaximumValue, NumberColumnPOCO.MAX_VALUE);
        }

        protected override NumberColumnPOCO CreateColumnPOCO()
        {
            return new NumberColumnPOCO();
        }
    }
}
