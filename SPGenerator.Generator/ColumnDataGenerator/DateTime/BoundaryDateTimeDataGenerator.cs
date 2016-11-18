using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.DateTime
{
    public class BoundaryDateTimeDataGenerator : ColumnDataGenerator<DateTimeColumnPOCO>
    {
        public BoundaryDateTimeDataGenerator(DateTimeColumnPOCO column) : base(column)
        {
            //left empty
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

        private List<System.DateTime> GetBoundaryValues()
        {
            return new List<System.DateTime>() { column.MinValue, column.MaxValue };
        }
    }
}
