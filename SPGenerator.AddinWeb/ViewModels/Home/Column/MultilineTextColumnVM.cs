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
        public int MaxLength { get; set; }

        [DisplayName("Minimalna długość")]
        public int MinLength { get; set; }

        public override void SyncModels(ColumnPOCO columnPOCO)
        {
            base.SyncModels(columnPOCO);
            var multilineTextColumnPOCO = columnPOCO as MultilineTextColumnPOCO;
            multilineTextColumnPOCO.MaxLength = MaxLength;
            multilineTextColumnPOCO.MinLength = MinLength;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinLength > MaxLength)
            {
                yield return new ValidationResult($"Minimalna długość kolumny {DisplayName} nie może być większa niż maksymalna",
                    new[] { nameof(MaxLength), nameof(MinLength) });
            }
            if (MaxLength > MultilineTextColumnPOCO.MAX_LENGTH)
            {
                yield return new ValidationResult($"Długość wartości kolumny {DisplayName} nie może przekraczać {MultilineTextColumnPOCO.MAX_LENGTH} znaków", 
                    new[] { nameof(MaxLength)});
            }
            if (MinLength < MultilineTextColumnPOCO.MIN_LENGTH)
            {
                yield return new ValidationResult($"Długość wartości kolumny {DisplayName} nie może być mniejsza od {MultilineTextColumnPOCO.MIN_LENGTH} znaków", 
                    new[] { nameof(MinLength) });
            }
            foreach (var baseResult in base.Validate(validationContext))
            {
                yield return baseResult;
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