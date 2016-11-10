using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Text
{
    public class ConstantTextDataGenerator : ColumnDataGenerator<TextColumnPOCO>
    {
        public ConstantTextDataGenerator(TextColumnPOCO column) : base(column)
        {
            //intentionally left empty
        }

        public override IEnumerable<object> GenerateData(int recordsCount)
        {
            var data = new List<object>(recordsCount);
            while (recordsCount-- > 0)
            {
                var dataElement = "constant text";
                data.Add(dataElement);
            }
            return data;
        }
    }
}
