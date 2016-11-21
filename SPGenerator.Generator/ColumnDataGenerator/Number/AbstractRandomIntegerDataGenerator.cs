using SPGenerator.Generator.ColumnDataGenerator.Currency;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public abstract class AbstractRandomIntegerDataGenerator : ColumnDataGenerator<NumberColumnPOCO>, INumberDataGenerator
    {
        private int? max;
        private int? min;

        public AbstractRandomIntegerDataGenerator(int? min = null, int? max = null)
        {
            this.min = min;
            this.max = max;
        }

        protected override bool CanGenerateData(NumberColumnPOCO column)
        {
            return (GetColumnMax(column) - GetColumnMin(column) > 1)
                && base.CanGenerateData(column);
        }

        protected override IEnumerable<object> GenerateData(NumberColumnPOCO column, int recordsCount)
        {
            int columnMax = GetColumnMax(column);
            int columnMin = GetColumnMin(column);
            while (recordsCount-- > 0)
            {
                yield return RANDOM.Next(columnMin, columnMax);
            }
        }

        private int GetColumnMin(NumberColumnPOCO column)
        {
            var columnMin = (int)Math.Ceiling(column.MinValue);
            return min.HasValue ?
                Math.Max(min.Value, columnMin) :
                columnMin;
        }

        private int GetColumnMax(NumberColumnPOCO column)
        {
            var columnMax = (int)Math.Floor(column.MaxValue);
            return max.HasValue ?
                Math.Min(max.Value, columnMax) :
                columnMax;
        }
    }
}
