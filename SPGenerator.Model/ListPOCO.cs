using SPGenerator.Model.Column;
using System.Collections.Generic;

namespace SPGenerator.Model
{
    /// <summary>
    /// A SharePoint list.
    /// </summary>
    public class ListPOCO
    {
        /// <summary>
        /// Name of the list.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// List of all columns in this list.
        /// </summary>
        public List<ColumnPOCO> ColumnPOCOList { get; set; }

        /// <summary>
        /// Url for default view of this list.
        /// </summary>
        public string ServerRelativeUrl { get; set; }
    }
}