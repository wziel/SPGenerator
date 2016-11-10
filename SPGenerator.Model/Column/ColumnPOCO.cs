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
        /// SharePoint internal name of this column.
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Display name of this column.
        /// </summary>
        public string DisplayName { get; set; }

        public bool Required { get; set; }
    }
}
