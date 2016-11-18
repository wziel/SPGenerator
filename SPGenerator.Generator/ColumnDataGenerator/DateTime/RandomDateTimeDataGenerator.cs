using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.DateTime
{
    public class RandomDateTimeDataGenerator : ColumnDataGenerator<DateTimeColumnPOCO>
    {
        public RandomDateTimeDataGenerator(DateTimeColumnPOCO column) : base(column)
        {
            //left empty
        }

        public override IEnumerable<object> GenerateData(int recordsCount)
        {
            var data = new List<object>(recordsCount);
            var milisecondsRange = (column.MaxValue - column.MinValue).TotalMilliseconds;
            while (recordsCount-- > 0)
            {
                var miliseconds = RANDOM.NextDouble() * milisecondsRange;
                var resultDate = column.MinValue.AddMilliseconds(miliseconds);
                data.Add(resultDate);
            }
            return data;
        }
    }
}
