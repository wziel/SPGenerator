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
        [Range(TextColumnPOCO.MIN_LENGTH, TextColumnPOCO.MAX_LENGTH)]
        public int MaxLength { get; set; }

        [DisplayName("Minimalna długość")]
        [Range(TextColumnPOCO.MIN_LENGTH, TextColumnPOCO.MAX_LENGTH)]
        public int MinLength { get; set; }

        public int InternalMaxLength { get; set; }

        public override void ApplyTo(ColumnPOCO columnPOCO)
        {
            base.ApplyTo(columnPOCO);
            var textColumnPOCO = columnPOCO as TextColumnPOCO;
            textColumnPOCO.MaxLength = MaxLength;
            textColumnPOCO.MinLength = MinLength;
        }

        public override void AssertCanApplyTo(ColumnPOCO columnPOCO)
        {
            base.AssertCanApplyTo(columnPOCO);
            var textColumnPOCO = columnPOCO as TextColumnPOCO;
            if (MaxLength > TextColumnPOCO.MAX_LENGTH)
            {
                throw new GUIVisibleException("Długość wartości kolumny " + InternalName + " typu Pojedynczy wiersz tesktu " +
                    "nie może przekraczać " + textColumnPOCO.InternalMaxLength);
            }
            if (MinLength > TextColumnPOCO.MIN_LENGTH)
            {
                throw new GUIVisibleException("Długość wartości kolumny " + InternalName + " typu Pojedynczy wiersz tesktu " +
                    "nie może być mniejsza od " + TextColumnPOCO.MIN_LENGTH);
            }
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinLength > MaxLength)
            {
                yield return new ValidationResult("Minimalna długość nie może być większa niż maksymalna",
                    new[] { nameof(MaxLength), nameof(MinLength) });
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