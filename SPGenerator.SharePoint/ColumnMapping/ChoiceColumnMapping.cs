using System;
using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    class ChoiceColumnMapping : ColumnMapping<FieldChoice, ChoiceColumnPOCO>
    {
        protected sealed override void ApplyProperies(ChoiceColumnPOCO column, FieldChoice field)
        {
            base.ApplyProperies(column, field);
            column.Choices = field.Choices;
        }

        protected override ChoiceColumnPOCO CreateColumnPOCO()
        {
            return new ChoiceColumnPOCO();
        }
    }
}
