using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public class BoundaryDoubleDataGenerator : ColumnDataGenerator<NumberColumnPOCO>
    {
        public BoundaryDoubleDataGenerator(NumberColumnPOCO column) : base(column)
        {
        }

        public override bool CanGenerateData
        {
            get
            {
                return !column.OnlyIntegers && column.GenerateData;
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

        private List<double> GetBoundaryValues()
        {
            return new List<double>() { column.MinValue, column.MaxValue };
        }
    }
}
