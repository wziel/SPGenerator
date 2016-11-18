using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SPGenerator.Model.Column;
using System.ComponentModel;
using SPGenerator.Model;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class MultilineTextColumnVM : ColumnVM<MultilineTextColumnPOCO>, IMultilineTextColumnVM
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

        [DisplayName("Maksymalna długość")]
        [Range(MultilineTextColumnPOCO.MIN_LENGTH, MultilineTextColumnPOCO.MAX_LENGTH)]
        public int MaxLength { get; set; }

        [DisplayName("Minimalna długość")]
        [Range(MultilineTextColumnPOCO.MIN_LENGTH, MultilineTextColumnPOCO.MAX_LENGTH)]
        public int MinLength { get; set; }

        public override void ApplyTo(ColumnPOCO columnPOCO)
        {
            base.ApplyTo(columnPOCO);
            var multilineTextColumnPOCO = columnPOCO as MultilineTextColumnPOCO;
            multilineTextColumnPOCO.MaxLength = MaxLength;
            multilineTextColumnPOCO.MinLength = MinLength;
        }

        public override void AssertCanApplyTo(ColumnPOCO columnPOCO)
        {
            base.AssertCanApplyTo(columnPOCO);
            if (MaxLength > MultilineTextColumnPOCO.MAX_LENGTH)
            {
                throw new GUIVisibleException("Długość wartości kolumny " + InternalName + " typu Wiele wierszy tesktu nie może przekraczać " +
                    MultilineTextColumnPOCO.MAX_LENGTH + " znaków");
            }
            if(MinLength < MultilineTextColumnPOCO.MIN_LENGTH)
            {
                throw new GUIVisibleException("Długość wartości kolumny " + InternalName + " typu Wiele wierszy tesktu nie może być mniejsza od " +
                    MultilineTextColumnPOCO.MIN_LENGTH + " znaków");
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

    public interface IMultilineTextColumnVM : IColumnVM
    {
        /// <summary>
        /// Maximum length of column text.
        /// </summary>
        int MaxLength { get; set; }
        
        /// <summary>
        /// Minimum length of column text.
        /// </summary>
        int MinLength { get; set; }
    }
}