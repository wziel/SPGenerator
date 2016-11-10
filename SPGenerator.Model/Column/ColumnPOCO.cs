using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Model.Column
{
    /// <summary>
    /// A column definition in a list.
    /// </summary>
    public abstract class ColumnPOCO
    {
        /// <summary>
        /// Name of this column.
        /// </summary>
        public string ColumnName { get; set; }

        public bool Required { get; set; }
    }
}
