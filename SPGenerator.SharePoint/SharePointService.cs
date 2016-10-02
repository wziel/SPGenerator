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
        /// Url of SharePoint's site collection web url.
        /// </summary>
        public string HostWebUrl
        {
            get
            {
                using (var context = contextHelper.ClientContext)
                {
                    context.Load(context.Web);
                    context.ExecuteQuery();
                    return context.Web.Url;
                }
            }
        }

        /// <summary>
        /// All SharePoint Lists in site collection.
        /// </summary>
        public List<SPGList> AllSPGLists
        {
            get
            {
                using (var context = contextHelper.ClientContext)
                {
                    var query = context.Web.Lists
                        .Where(list => !list.Hidden)
                        .Include(list => list.Title, list => list.DefaultViewUrl);
                    var lists = context.LoadQuery(query);
                    context.ExecuteQuery();
                    return modelTranslator.TranslateToAppDomain(lists);
                }
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