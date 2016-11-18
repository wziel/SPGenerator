using Microsoft.SharePoint.Client;
using SPGenerator.Model.Column;

namespace SPGenerator.SharePoint.ColumnMapping
{
    internal abstract class ColumnMapping<TInputField, TResultColumnPOCO> : ICOlumnMapping 
        where TResultColumnPOCO : ColumnPOCO
        where TInputField : Field
    {
        public ColumnPOCO Map(Field field)
        {
            var column = CreateColumnPOCO();
            var specificField = (TInputField)field;
            ApplyProperies(column, specificField);
            return column;
        }

        protected virtual void ApplyProperies(TResultColumnPOCO column, TInputField field)
        {
            column.InternalName = field.InternalName;
            column.DisplayName = field.Title;
            column.Required = field.Required;
            column.GenerateData = true;
        }

        protected abstract TResultColumnPOCO CreateColumnPOCO();
    }

    internal interface ICOlumnMapping
    {
        ColumnPOCO Map(Field field);
    }
}
