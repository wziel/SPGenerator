using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Model.Column
{
    public class DateTimeColumnPOCO : ColumnPOCO
    {
        public DateTime MinValue { get; set; }
        public DateTime MaxValue { get; set; }
    }
}
