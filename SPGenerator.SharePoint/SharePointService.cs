using Microsoft.SharePoint.Client;
using SPGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SPGenerator.SharePoint
{
    /// <summary>
    /// Class used for all communication with SharePoint.
    /// </summary>
    public class SharePointService
    {
        private readonly SharePointContextHelper contextHelper;
        private ModelTranslator modelTranslator;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="httpContext">Http context which will be used to communicate
        /// with SharePoint.</param>
        public SharePointService(SharePointContextHelper contextHelper, 
            ModelTranslator modelTranslator)
        {
            this.contextHelper = contextHelper;
            this.modelTranslator = modelTranslator;
        }

        /// <summary>
        /// Used for fetching all SharePoint lists from site collection.
        /// </summary>
        /// <returns>List of all SharePoint lists from site collection.</returns>
        public List<SPGList> GetAllSPGLists()
        {
            using (var context = contextHelper.ClientContext)
            {
                context.Load(context.Web.Lists);
                context.ExecuteQuery();
                return modelTranslator.TranslateToAppDomain(context.Web.Lists);
            }
        }

        /// <summary>
        /// Used for fetching detailed information about a SharePoint list.
        /// </summary>
        /// <param name="listName">Name of the list to fetch.</param>
        /// <returns>Detailed information about a SharePoint list.</returns>
        public SPGList GetSPGList(string listName)
        {
            throw new NotImplementedException();
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