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

        /// <summary>
        /// Is this column required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Should data be generated for this column.
        /// </summary>
        public bool GenerateData { get; set; }
    }
}
