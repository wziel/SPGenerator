using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Model.Column
{
    public class NumberColumnPOCO : ColumnPOCO
    {
        public const double MAX_VALUE = 1000000;
        public const double MIN_VALUE = -1000000;

        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double InternalMinValue { get; set; }
        public double InternalMaxValue { get; set; }
    }
}
