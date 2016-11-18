using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Model.Column
{
    public class ChoiceColumnPOCO : ColumnPOCO
    {
        public string[] Choices { get; set; }
    }
}
