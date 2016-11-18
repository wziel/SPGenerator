using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    class ChoiceColumnMapping : ColumnMapping
    {
        public override ColumnPOCO Map(Field field)
        {
            var choiceField = (FieldChoice) field;
            return new ChoiceColumnPOCO()
            {
                InternalName = field.InternalName,
                DisplayName = field.Title,
                Required = field.Required,
                Choices = choiceField.Choices
            };
        }
    }
}
