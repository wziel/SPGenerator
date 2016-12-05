using SPGenerator.Model;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class TextColumnVM : ColumnVM<TextColumnPOCO>, ITextColumnVM
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
        public int MaxLength { get; set; }

        [DisplayName("Minimalna długość")]
        public int MinLength { get; set; }

        public int InternalMaxLength { get; set; }

        public override void ApplyTo(ColumnPOCO columnPOCO)
        {
            base.ApplyTo(columnPOCO);
            var textColumnPOCO = columnPOCO as TextColumnPOCO;
            textColumnPOCO.MaxLength = MaxLength;
            textColumnPOCO.MinLength = MinLength;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinLength > MaxLength)
            {
                yield return new ValidationResult($"Minimalna długość kolumny {InternalName} nie może być większa niż maksymalna",
                    new[] { nameof(MaxLength), nameof(MinLength) });
            }
            if (columnPOCO != null && MaxLength > columnPOCO.InternalMaxLength)
            {
                yield return new ValidationResult($"Maksymalna długość kolumny {InternalName} nie może przekraczać {columnPOCO.InternalMaxLength}",
                    new[] { nameof(MaxLength)});
            }
            if (MinLength < TextColumnPOCO.MIN_LENGTH)
            {
                yield return new ValidationResult($"Minimalna długość kolumny {InternalName} nie może być mniejsza od {TextColumnPOCO.MIN_LENGTH}",
                    new[] { nameof(MinLength) });
            }
        }
    }

    public interface ITextColumnVM : IColumnVM
    {
        /// <summary>
        /// Maximum length of this column specified by user.
        /// </summary>
        int MaxLength { get; set; }
        
        /// <summary>
        /// Minimum length of this column specified by user.
        /// </summary>
        int MinLength { get; set; }

        /// <summary>
        /// Maximum length of this column specified by system.
        /// </summary>
        int InternalMaxLength { get; set; }
    }
}