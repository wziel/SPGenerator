using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public class DateTimeColumnVM : ColumnVM<DateTimeColumnPOCO>, IDateTimeColumnVM
    {
        public DateTimeColumnVM()
        {
            //for razor
        }

        public DateTimeColumnVM(DateTimeColumnPOCO columnPOCO) : base(columnPOCO)
        {
            MinValue = columnPOCO.MinValue;
            MaxValue = columnPOCO.MaxValue;
        }

        [DisplayName("Minimalna wartość")]
        public DateTime MinValue { get; set; }

        [DisplayName("Maksymalna wartość")]
        public DateTime MaxValue { get; set; }

        public override void ApplyTo(ColumnPOCO columnPOCO)
        {
            base.ApplyTo(columnPOCO);
            var dateTimeColumnPOCO = columnPOCO as DateTimeColumnPOCO;
            dateTimeColumnPOCO.MinValue = MinValue;
            dateTimeColumnPOCO.MaxValue = MaxValue;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinValue > MaxValue)
            {
                yield return new ValidationResult($"Minimalna wartość kolumny {InternalName} nie może być większa niż maksymalna",
                    new[] { nameof(MaxValue), nameof(MinValue) });
            }
            foreach(var baseResult in base.Validate(validationContext))
            {
                yield return baseResult;
            }
        }
    }

    public interface IDateTimeColumnVM : IColumnVM
    {
        DateTime MinValue { get; set; }
        DateTime MaxValue { get; set; }
    }
}