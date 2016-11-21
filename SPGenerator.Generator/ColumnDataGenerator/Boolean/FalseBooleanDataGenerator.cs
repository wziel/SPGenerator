using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Boolean
{
    public class FalseBooleanDataGenerator : ColumnDataGenerator<BooleanColumnPOCO>, IBooleanDataGenerator
    {
        protected override IEnumerable<object> GenerateData(BooleanColumnPOCO column, int recordsCount)
        {
            while (recordsCount-- > 0)
            {
                yield return false;
            }
        }
    }
}
