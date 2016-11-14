using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    class MultilineTextColumnMapping : ColumnMapping
    {
        public override ColumnPOCO Map(Field field)
        {
            var textField = field as FieldMultiLineText;
            return new MultilineTextColumnPOCO()
            {
                InternalName = field.InternalName,
                DisplayName = field.Title,
                Required = field.Required,
                MinLength = MultilineTextColumnPOCO.MIN_LENGTH,
                MaxLength = MultilineTextColumnPOCO.MAX_LENGTH,
            };
        }
    }
}
