using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public class RandomIntegerDataGenerator : ColumnDataGenerator<NumberColumnPOCO>
    {
        public RandomIntegerDataGenerator(NumberColumnPOCO column) : base(column)
        {
            //left empty
        }

        public override bool CanGenerateData
        {
            get
            {
                return (column.MaxValue - column.MinValue >= 1) && column.GenerateData;
            }
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
