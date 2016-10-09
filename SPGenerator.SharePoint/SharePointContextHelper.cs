using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SPGenerator.SharePoint
{
    /// <summary>
    /// Component responsible for delivering information about current sharepoint context.
    /// </summary>
    public class SharePointContextHelper : ISharePointContextHelper
    {
        /// <summary>
        /// Sharepoint host's client context. Should be disposed after usage.
        /// </summary>
        public ClientContext ClientContext
        {
            get
            {
                var httpContext = new HttpContextWrapper(HttpContext.Current);
                var spContext = SharePointContextProvider.Current.GetSharePointContext(httpContext);
                return spContext.CreateUserClientContextForSPHost();
            }
        }
    }

    public interface ISharePointContextHelper
    {
        /// <summary>
        /// Sharepoint host's client context. Should be disposed after usage.
        /// </summary>
        ClientContext ClientContext { get; }
    }
}
