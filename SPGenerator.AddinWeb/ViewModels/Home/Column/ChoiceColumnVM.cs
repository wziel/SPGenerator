using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPGenerator.Model.Column;
using SPGenerator.Model;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class ChoiceColumnVM : ColumnVM<ChoiceColumnPOCO>, IChoiceColumnVM
    {
        public ChoiceColumnVM()
        {
            //for razor
        }

        public ChoiceColumnVM(ChoiceColumnPOCO columnPOCO) : base(columnPOCO)
        {
            //left empty
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }

    public interface IChoiceColumnVM : IColumnVM
    {
        //left empty
    }
}