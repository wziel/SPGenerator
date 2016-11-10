using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    class TextColumnMapping : ColumnMapping
    {
        public override ColumnPOCO Map(Field field)
        {
            var textField = (FieldText)field;
            return new TextColumnPOCO()
            {
                InternalName = textField.InternalName,
                DisplayName = textField.Title,
                Required = textField.Required,
                MinLength = 0,
                MaxLength = Math.Min(TextColumnPOCO.MAX_LENGTH, textField.MaxLength),
                InternalMaxLength = textField.MaxLength
            };
        }
    }
}
