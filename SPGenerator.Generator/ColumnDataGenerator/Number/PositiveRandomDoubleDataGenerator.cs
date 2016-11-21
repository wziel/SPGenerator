using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public class PositiveRandomDoubleDataGenerator : AbstractRandomDoubleDataGenerator, INumberDataGenerator
    {
        public PositiveRandomDoubleDataGenerator() : base(min: 1)
        {
            //left empty
        }
    }
}
