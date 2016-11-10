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
            var numberField = (FieldNumber)field;
            return new NumberColumnPOCO()
            {
                ColumnName = field.Title,
                Required = field.Required,
                InternalMaxValue = numberField.MaximumValue,
                InternalMinValue = numberField.MinimumValue,
                MinValue = Math.Max(numberField.MinimumValue, NumberColumnPOCO.MIN_VALUE),
                MaxValue = Math.Min(numberField.MaximumValue, NumberColumnPOCO.MAX_VALUE)
            };
        }
    }
}
