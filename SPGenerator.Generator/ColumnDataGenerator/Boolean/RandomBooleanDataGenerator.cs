using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Boolean
{
    public class RandomBooleanDataGenerator : ColumnDataGenerator<BooleanColumnPOCO>
    {
        public RandomBooleanDataGenerator(BooleanColumnPOCO column) : base(column)
        {
            //left empty
        }

        public override IEnumerable<object> GenerateData(int recordsCount)
        {
            var data = new List<object>(recordsCount);
            while (recordsCount-- > 0)
            {
                var value = RANDOM.Next(0, 2);
                data.Add(value);
            }
            return data;
        }
    }
}
