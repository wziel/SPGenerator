using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPGenerator.Model.Column;
using System.ComponentModel;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class MultilineTextColumnVM : ColumnVM
    {
        public MultilineTextColumnVM()
        {
            //intentionally left empty, constructor for razor
        }

        public MultilineTextColumnVM(MultilineTextColumnPOCO columnPOCO) : base(columnPOCO)
        {
            MaxLength = columnPOCO.MaxLength;
            MinLength = columnPOCO.MinLength;
        }

        public override ColumnPOCO ColumnPOCO
        {
            get
            {
                return new MultilineTextColumnPOCO()
                {
                    InternalName = InternalName,
                    DisplayName = DisplayName,
                    Required = Required,
                    MaxLength = MaxLength,
                    MinLength = MinLength,
                };
            }
        }

        [DisplayName("Maksymalna długość")]
        [Range(MultilineTextColumnPOCO.MIN_LENGTH, MultilineTextColumnPOCO.MAX_LENGTH)]
        public int MaxLength { get; set; }

        [DisplayName("Minimalna długość")]
        [Range(MultilineTextColumnPOCO.MIN_LENGTH, MultilineTextColumnPOCO.MAX_LENGTH)]
        public int MinLength { get; set; }


        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinLength > MaxLength)
            {
                yield return new ValidationResult("Minimalna długość nie może być większa niż maksymalna",
                    new[] { nameof(MaxLength), nameof(MinLength) });
            }
        }
    }
}