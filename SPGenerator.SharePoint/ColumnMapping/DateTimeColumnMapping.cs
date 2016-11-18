using System;
using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    class DateTimeColumnMapping : ColumnMapping<FieldDateTime, DateTimeColumnPOCO>
    {
        protected override void ApplyProperies(DateTimeColumnPOCO column, FieldDateTime field)
        {
            base.ApplyProperies(column, field);
            column.MinValue = new DateTime(1900, 1, 1);
            column.MaxValue = new DateTime(2100, 1, 1);
        }

        protected override DateTimeColumnPOCO CreateColumnPOCO()
        {
            return new DateTimeColumnPOCO();
        }
    }
}
