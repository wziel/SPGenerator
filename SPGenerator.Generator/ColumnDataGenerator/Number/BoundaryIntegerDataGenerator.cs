using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public class BoundaryIntegerDataGenerator : ColumnDataGenerator<NumberColumnPOCO>, INumberDataGenerator
    {
        protected override bool CanGenerateData(NumberColumnPOCO column)
        {
            return GetBoundaryValues(column).Any() && base.CanGenerateData(column);
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

        private List<int> GetBoundaryValues(NumberColumnPOCO column)
        {
            var maxVal = (int)Math.Floor(column.MaxValue);
            var minVal = (int)Math.Ceiling(column.MinValue);
            return new List<int>() { -1, 0, 1, minVal, minVal + 1, maxVal, maxVal - 1 }
                .Where(v => column.MaxValue >= v && column.MinValue <= v).ToList();
        }
    }
}