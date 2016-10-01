using SPGenerator.Model.Column;
using System.Collections.Generic;

namespace SPGenerator.Model.List
{
    /// <summary>
    /// A SharePoint list.
    /// </summary>
    public class SPGList
    {
        /// <summary>
        /// Name of the list.
        /// </summary>
        public string ListName { get; set; }

        /// <summary>
        /// List of all columns in this list.
        /// </summary>
        public List<SPGColumn> SPGColumns { get; set; }
    }
}