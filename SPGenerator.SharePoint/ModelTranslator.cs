using Microsoft.SharePoint.Client;
using SPGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.SharePoint
{
    /// <summary>
    /// A helper class that translates objects from SharePoint domain
    /// to application domain.
    /// </summary>
    public class ModelTranslator
    {
        /// <summary>
        /// Gets application domain list of SPGLists.
        /// </summary>
        /// <param name="listCollection">Collection to be translated.</param>
        /// <returns>List of translated SPGLists</returns>
        public List<SPGList> TranslateToAppDomain(ListCollection listCollection)
        {
            IEnumerable<List> enumerableLists = listCollection;
            return enumerableLists.Select(list => new SPGList()
            {
                ListName = list.Title
            }).ToList();
        }
    }
}
