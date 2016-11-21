using SPGenerator.Generator.ColumnDataGenerator.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Number
{
    public class NegativeRandomIntegerDataGenerator : AbstractRandomIntegerDataGenerator, INumberDataGenerator
    {
        public NegativeRandomIntegerDataGenerator() : base(max: 0)
        {
            //left empty
        }
    }
}
