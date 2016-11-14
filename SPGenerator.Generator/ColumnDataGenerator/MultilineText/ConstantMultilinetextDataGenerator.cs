using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.MultilineText
{
    public class ConstantMultilinetextDataGenerator : ColumnDataGenerator<MultilineTextColumnPOCO>
    {
        public ConstantMultilinetextDataGenerator(MultilineTextColumnPOCO column) : base(column)
        {
            //intentionally left empty
        }

        public override IEnumerable<object> GenerateData(int recordsCount)
        {
            var data = new List<object>(recordsCount);
            while (recordsCount-- > 0)
            {
                var dataElement = "constant multiline text";
                data.Add(dataElement);
            }
            return data;
        }
    }
}
