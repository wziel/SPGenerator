using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Model.Column
{
    public class MultilineTextColumnPOCO : ColumnPOCO
    {
        public const int MAX_LENGTH = 1000;
        public const int MIN_LENGTH = 1;

        public int MaxLength { get; set; }
        public int MinLength { get; set; }
    }
}
