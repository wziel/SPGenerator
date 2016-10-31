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
                ColumnName = textField.Title,
                MinLength = Math.Min(5, textField.MaxLength),
                MaxLength = Math.Min(100, textField.MaxLength),
                InternalMaxLength = textField.MaxLength
            };
        }
    }
}
