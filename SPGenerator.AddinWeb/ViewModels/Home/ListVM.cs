using SPGenerator.AddinWeb.ViewModels.Home.Column;
using SPGenerator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SPGenerator.AddinWeb.ViewModels.Home
{
    /// <summary>
    /// A SharePoint list.
    /// </summary>
    public class ListVM
    {
        public ListVM()
        {
            //intentionally left empty, constructor for Razor
        }

        public ListVM(ListPOCO listPOCO)
        {
            Title = listPOCO.Title;
            ServerRelativeUrl = listPOCO.ServerRelativeUrl;
        }

        /// <summary>
        /// Name of the list.
        /// </summary>
        [DisplayName("Lista docelowa")]
        public string Title { get; set; }

        /// <summary>
        /// Url for default view of this list.
        /// </summary>
        public string ServerRelativeUrl { get; set; }

        public ListPOCO ListPOCO
        {
            get
            {
                return new ListPOCO()
                {
                    Title = Title,
                    ServerRelativeUrl = ServerRelativeUrl,
                };
            }
        }

    }
}