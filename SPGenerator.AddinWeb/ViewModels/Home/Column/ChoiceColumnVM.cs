using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPGenerator.Model.Column;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class ChoiceColumnVM : ColumnVM
    {
        public ChoiceColumnVM()
        {
            //for razor
        }

        public ChoiceColumnVM(ChoiceColumnPOCO columnPOCO) : base(columnPOCO)
        {
            Choices = columnPOCO.Choices;
        }

        public string[] Choices { get; set; }

        public override ColumnPOCO ColumnPOCO
        {
            get
            {
                return new ChoiceColumnPOCO()
                {
                    InternalName = InternalName,
                    DisplayName = DisplayName,
                    Required = Required,
                    Choices = Choices
                };
            }
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}