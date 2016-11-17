using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public class BoundaryIntegerDataGenerator : ColumnDataGenerator<NumberColumnPOCO>
    {
        public BoundaryIntegerDataGenerator(NumberColumnPOCO column) : base(column)
        {
            //left empty
        }

        public override bool CanGenerateData
        {
            get
            {
                return GetBoundaryValues().Any();
            }
        }

        public override IEnumerable<object> GenerateData(int recordsCount)
        {
            var boundaryValues = GetBoundaryValues();
            var data = new List<object>(recordsCount);
            while (recordsCount-- > 0)
            {
                var i = recordsCount % boundaryValues.Count;
                data.Add(boundaryValues[i]);
            }
            return data;
        }

        private List<int> GetBoundaryValues()
        {
            var maxVal = (int)Math.Floor(column.MaxValue);
            var minVal = (int)Math.Ceiling(column.MinValue);
            return new List<int>() { -1, 0, 1, minVal, minVal + 1, maxVal, maxVal - 1 }
                .Where(v => column.MaxValue >= v && column.MinValue <= v).ToList();
        }
    }
}