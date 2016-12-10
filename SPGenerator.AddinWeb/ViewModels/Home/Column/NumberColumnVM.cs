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
    public class NumberColumnVM : ColumnVM<NumberColumnPOCO>, INumberColumnVM
    {
        public NumberColumnVM()
        {
            //intentionally left empty, constructor for Razor
        }

        public NumberColumnVM(NumberColumnPOCO columnPOCO) : base(columnPOCO)
        {
            MinValue = columnPOCO.MinValue;
            MaxValue = columnPOCO.MaxValue;
            OnlyIntegers = columnPOCO.OnlyIntegers;
            InternalMinValue = columnPOCO.InternalMinValue;
            InternalMaxValue = columnPOCO.InternalMaxValue;
        }

        [DisplayName("Minimalna wartość")]
        public double MinValue { get; set; }

        [DisplayName("Maksymalna wartość")]
        public double MaxValue { get; set; }

        [DisplayName("Tylko całkowite")]
        public bool OnlyIntegers { get; set; }

        public double InternalMinValue { get; set; }

        public double InternalMaxValue { get; set; }

        public override void SyncModels(ColumnPOCO columnPOCO)
        {
            base.SyncModels(columnPOCO);
            var numberColumnPOCO = columnPOCO as NumberColumnPOCO;
            numberColumnPOCO.MaxValue = MaxValue;
            numberColumnPOCO.MinValue = MinValue;
            numberColumnPOCO.OnlyIntegers = OnlyIntegers;
            InternalMinValue = numberColumnPOCO.InternalMinValue;
            InternalMaxValue = numberColumnPOCO.InternalMaxValue;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaxValue < MinValue)
            {
                yield return new ValidationResult($"Minimalna wartość w kolumnie {DisplayName} nie może być większa niż maksymalna",
                    new[] { nameof(MinValue), nameof(MaxValue) });
            }
            if(OnlyIntegers && (MaxValue - MinValue < 1))
            {
                yield return new ValidationResult($"Przedział wartości w kolumnie {DisplayName} musi zawierać co najmniej jedną wartość całkowitą",
                    new[] { nameof(MinValue), nameof(MaxValue), nameof(OnlyIntegers) });
            }
            if (MinValue < InternalMinValue)
            {
                yield return new ValidationResult($"Wartość minimalna w kolumnie {DisplayName} nie może być mniejsza od {InternalMinValue}",
                    new[] { nameof(MinValue)});
            }
            if (MaxValue > InternalMaxValue)
            {
                yield return new ValidationResult($"Wartość maksymalna w kolumnie {DisplayName} nie może być większa od {InternalMaxValue}",
                    new[] { nameof(MaxValue) });
            }
            foreach(var baseResult in base.Validate(validationContext))
            {
                yield return baseResult;
            }
        }
    }

    public interface INumberColumnVM : IColumnVM
    {
        /// <summary>
        /// Minimum value of this column specified by user.
        /// </summary>
        double MinValue { get; set; }
        
        /// <summary>
        /// Maximum value of this column specified by user.
        /// </summary>
        double MaxValue { get; set; }
    
        /// <summary>
        /// Shoudl only integers be applied to this column?
        /// </summary>
        bool OnlyIntegers { get; set; }

        /// <summary>
        /// Minimum value of this column specified by system.
        /// </summary>
        double InternalMinValue { get; set; }

        /// <summary>
        /// Maximum value of this column specified by system.
        /// </summary>
        double InternalMaxValue { get; set; }
    }
}