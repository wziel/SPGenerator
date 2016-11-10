using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public class IntegerDataGenerator : ColumnDataGenerator<NumberColumnPOCO>
    {
        public IntegerDataGenerator(NumberColumnPOCO column) : base(column)
        {
            //intentionally left empty
        }

        public override IEnumerable<object> GenerateData(int recordsCount)
        {
            var data = new List<object>(recordsCount);
            while (recordsCount-- > 0)
            {
                var dataElement = GenerateDataElement();
                data.Add(dataElement);
            }
            return data;
        }


        private int GenerateDataElement()
        {
            var min = (int)Math.Ceiling(column.MinValue);
            var max = (int)Math.Floor(column.MaxValue);
            return RANDOM.Next(min, max);
        }
    }
}
