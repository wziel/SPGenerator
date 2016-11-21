using SPGenerator.Generator.ColumnDataGenerator.Currency;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public class BoundaryDoubleDataGenerator : ColumnDataGenerator<NumberColumnPOCO>, INumberDataGenerator
    {
        protected override bool CanGenerateData(NumberColumnPOCO column)
        {
            return !column.OnlyIntegers && base.CanGenerateData(column);
        }

        protected override IEnumerable<object> GenerateData(NumberColumnPOCO column, int recordsCount)
        {
            var boundaryValues = GetBoundaryValues(column);
            while (recordsCount-- > 0)
            {
                var i = recordsCount % boundaryValues.Count;
                yield return boundaryValues[i];
            }
        }

        private List<double> GetBoundaryValues(NumberColumnPOCO column)
        {
            return new List<double>() { column.MinValue, column.MaxValue };
        }
    }
}
