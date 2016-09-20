using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Model.List
{
    /// <summary>
    /// Detailed information about SharePoint list.
    /// </summary>
    public class SPGListDetails : SPGList
    {
        /// <summary>
        /// List of all columns in this list.
        /// </summary>
        public List<SPGColumn> SPGColumns { get; set; }
    }
}
