using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator
{
    public class NullDataGenerator : ColumnDataGenerator<ColumnPOCO>
    {

        public NullDataGenerator(ColumnPOCO column) : base(column)
        {
            //left empty
        }

        public override IEnumerable<object> GenerateData(int recordsCount)
        {
            var data = new List<object>(recordsCount);
            while (recordsCount-- > 0)
            {
                data.Add(null);
            }
            return data;
        }

        public override bool CanGenerateData
        {
            get
            {
                return !column.Required;
            }
        }
    }
}
