using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPGenerator.Model.Column;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public class RandomDoubleDataGenerator : ColumnDataGenerator<NumberColumnPOCO>
    {
        public RandomDoubleDataGenerator(NumberColumnPOCO column) : base(column)
        {
            //left empty
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
            var data = new List<object>(recordsCount);
            while (recordsCount-- > 0)
            {
                var dataElement = GenerateDataElement();
                data.Add(dataElement);
            }
            return data;
        }

        private double GenerateDataElement()
        {
            return RANDOM.NextDouble() * (column.MaxValue - column.MinValue) + column.MinValue;
        }
    }
}
