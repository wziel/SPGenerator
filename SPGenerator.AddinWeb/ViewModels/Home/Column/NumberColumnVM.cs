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
        [Range(NumberColumnPOCO.MIN_VALUE, NumberColumnPOCO.MAX_VALUE)]
        public double MinValue { get; set; }

        [DisplayName("Maksymalna wartość")]
        [Range(NumberColumnPOCO.MIN_VALUE, NumberColumnPOCO.MAX_VALUE)]
        public double MaxValue { get; set; }

        [DisplayName("Tylko całkowite")]
        public bool OnlyIntegers { get; set; }

        public double InternalMinValue { get; set; }

        public double InternalMaxValue { get; set; }

        public override void ApplyTo(ColumnPOCO columnPOCO)
        {
            base.ApplyTo(columnPOCO);
            var numberColumnPOCO = columnPOCO as NumberColumnPOCO;
            numberColumnPOCO.MaxValue = MaxValue;
            numberColumnPOCO.MinValue = MinValue;
            numberColumnPOCO.OnlyIntegers = OnlyIntegers;
        }

        public override void AssertCanApplyTo(ColumnPOCO columnPOCO)
        {
            base.AssertCanApplyTo(columnPOCO);
            var numberColumnPOCO = columnPOCO as NumberColumnPOCO;
            if (MinValue < NumberColumnPOCO.MIN_VALUE)
            {
                throw new GUIVisibleException("Wartość w kolumnie " + InternalName + " typu Liczba nie może być mniejsza od "
                    + numberColumnPOCO.InternalMinValue);
            }
            if (MaxValue > NumberColumnPOCO.MAX_VALUE)
            {
                throw new GUIVisibleException("Wartość w kolumnie " + InternalName + " typu Liczba nie może przekraczać "
                    + numberColumnPOCO.InternalMaxValue);
            }
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaxValue < MinValue)
            {
                yield return new ValidationResult("Minimalna wartość nie może być większa niż maksymalna",
                    new[] { nameof(MinValue), nameof(MaxValue) });
            }
            if(OnlyIntegers && (MaxValue - MinValue < 1))
            {
                yield return new ValidationResult("Przedział musi zawierać co najmniej jedną wartość całkowitą",
                    new[] { nameof(MinValue), nameof(MaxValue), nameof(OnlyIntegers) });
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