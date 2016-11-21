using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPGenerator.Model.Column;
using SPGenerator.Generator.ColumnDataGenerator.Currency;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public abstract class AbstractRandomDoubleDataGenerator : ColumnDataGenerator<NumberColumnPOCO>, INumberDataGenerator
    {
        private double? max;
        private double? min;

        public AbstractRandomDoubleDataGenerator(double? min = null, double? max = null)
        {
            this.min = min;
            this.max = max;
        }
        
        protected override bool CanGenerateData(NumberColumnPOCO column)
        {
            return !column.OnlyIntegers 
                && GetColumnMin(column) <= GetColumnMax(column)
                && base.CanGenerateData(column);
        }

        protected override IEnumerable<object> GenerateData(NumberColumnPOCO column, int recordsCount)
        {
            var columnMin = GetColumnMin(column);
            var columnMax = GetColumnMax(column);
            while (recordsCount-- > 0)
            {
                yield return (RANDOM.NextDouble() * (columnMax - columnMin) + columnMin);
            }
        }

        private double GetColumnMin(NumberColumnPOCO column)
        {
            return min.HasValue ?
                Math.Max(min.Value, column.MinValue) :
                column.MinValue;
        }

        private double GetColumnMax(NumberColumnPOCO column)
        {
            return max.HasValue ?
                Math.Min(max.Value, column.MaxValue) :
                column.MaxValue;
        }
    }
}
