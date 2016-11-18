using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    class MultilineTextColumnMapping : ColumnMapping<FieldMultiLineText, MultilineTextColumnPOCO>
    {
        protected override void ApplyProperies(MultilineTextColumnPOCO column, FieldMultiLineText field)
        {
            base.ApplyProperies(column, field);
            column.MinLength = MultilineTextColumnPOCO.MIN_LENGTH;
            column.MaxLength = MultilineTextColumnPOCO.MAX_LENGTH;
        }

        protected override MultilineTextColumnPOCO CreateColumnPOCO()
        {
            return new MultilineTextColumnPOCO();
        }
    }
}
