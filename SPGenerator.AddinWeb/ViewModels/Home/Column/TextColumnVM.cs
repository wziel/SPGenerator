using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class TextColumnVM : ColumnVM
    {
        public TextColumnVM()
        {
            //intentionally left empty, constructor for Razor
        }

        public TextColumnVM(TextColumnPOCO columnPOCO) : base(columnPOCO)
        {
            MaxLength = columnPOCO.MaxLength;
            MinLength = columnPOCO.MinLength;
            InternalMaxLength = columnPOCO.InternalMaxLength;
        }

        [DisplayName("Maksymalna długość")]
        [Range(1, 1024)]
        public int MaxLength { get; set; }

        [DisplayName("Minimalna długość")]
        [Range(1, 1024)]
        public int MinLength { get; set; }

        public int InternalMaxLength { get; set; }

        public override ColumnPOCO ColumnPOCO
        {
            get
            {
                return new TextColumnPOCO()
                {
                    MaxLength = MaxLength,
                    MinLength = MinLength,
                    InternalMaxLength = InternalMaxLength
                };
            }
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinLength > MaxLength)
            {
                yield return new ValidationResult("Minimalna długość nie może być większa niż maksymalna",
                    new[] { nameof(MaxLength), nameof(MinLength) });
            }
            if (MaxLength > InternalMaxLength)
            {
                yield return new ValidationResult("Maksymalna długość nie moze być większa niż " + InternalMaxLength,
                    new[] { nameof(MaxLength) });
            }
        }
    }
}