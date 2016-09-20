using SPGenerator.Model;
using SPGenerator.Model.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.SharePoint
{
    /// <summary>
    /// Class used for all communication with SharePoint.
    /// </summary>
    internal class SharePointService
    {
        /// <summary>
        /// Used for fetching all SharePoint lists from site collection.
        /// </summary>
        /// <returns>List of all SharePoint lists from site collection.</returns>
        public List<SPGList> GetSPGList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used for fetching detailed information about a SharePoint list.
        /// </summary>
        /// <param name="listName">Name of the list to fetch.</param>
        /// <returns>Detailed information about a SharePoint list.</returns>
        public SPGListDetails GetSPGListDetails(string listName)
        {
            throw new NotFiniteNumberException();
        }
        
        /// <summary>
        /// Used for saving collection of entries to SharePoint list.
        /// </summary>
        /// <param name="entries">Collection of entries to save.</param>
        /// <param name="list">Target list for entries to be saved to.</param>
        public void Save(IEnumerable<SPGEntry> entries, SPGList list)
        {
            throw new NotImplementedException();
        }
    }
}
