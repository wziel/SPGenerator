using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.DateTime
{
    public class BoundaryDateTimeDataGenerator : ColumnDataGenerator<DateTimeColumnPOCO>, IDateTimeDataGenerator
    {
        protected override IEnumerable<object> GenerateData(DateTimeColumnPOCO column, int recordsCount)
        {
            var boundaryValues = GetBoundaryValues(column);
            while (recordsCount-- > 0)
            {
                var i = recordsCount % boundaryValues.Count;
                yield return boundaryValues[i];
            }
        }

        private List<System.DateTime> GetBoundaryValues(DateTimeColumnPOCO column)
        {
            return new List<System.DateTime>() { column.MinValue, column.MaxValue, System.DateTime.Now };
        }
    }
}
