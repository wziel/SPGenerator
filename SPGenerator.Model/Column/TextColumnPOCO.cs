using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Model.Column
{
    public class TextColumnPOCO : ColumnPOCO
    {
        public const int MAX_LENGTH = 1024;

        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public int InternalMaxLength { get; set; }
    }
}
