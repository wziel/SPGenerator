using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class NumberColumnVM : ColumnVM
    {
        public NumberColumnVM()
        {
            //intentionally left empty, constructor for Razor
        }

        public NumberColumnVM(NumberColumnPOCO columnPOCO) : base(columnPOCO)
        {
            MinValue = columnPOCO.MinValue;
            MaxValue = columnPOCO.MaxValue;
        }

        [DisplayName("Minimalna wartość")]
        public int MinValue { get; set; }

        [DisplayName("Maksymalna wartość")]
        public int MaxValue { get; set; }

        public override ColumnPOCO ColumnPOCO
        {
            get
            {
                return new NumberColumnPOCO()
                {
                    MaxValue = MaxValue,
                    MinValue = MinValue
                };
            }
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaxValue < MinValue)
            {
                yield return new ValidationResult("Minimalna wartość nie może być większa niż maksymalna",
                    new[] { nameof(MinValue), nameof(MinValue) });
            }
        }
    }
}