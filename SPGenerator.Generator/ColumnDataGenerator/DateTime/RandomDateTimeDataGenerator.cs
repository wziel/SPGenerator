using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.DateTime
{
    public class RandomDateTimeDataGenerator : ColumnDataGenerator<DateTimeColumnPOCO>, IDateTimeDataGenerator
    {
        protected override IEnumerable<object> GenerateData(DateTimeColumnPOCO column, int recordsCount)
        {
            var milisecondsRange = (column.MaxValue - column.MinValue).TotalMilliseconds;
            while (recordsCount-- > 0)
            {
                var miliseconds = RANDOM.NextDouble() * milisecondsRange;
                yield return column.MinValue.AddMilliseconds(miliseconds);
            }
        }
    }
}
