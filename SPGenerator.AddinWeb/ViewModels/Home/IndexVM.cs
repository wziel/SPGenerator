using SPGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPGenerator.AddinWeb.ViewModels.Home
{
    /// <summary>
    /// View Model for Index View in Home Controller.
    /// </summary>
    public class IndexVM
    {
        /// <summary>
        /// Lists available for generation in Site Collection.
        /// </summary>
        public List<SPGList> SPGLists { get; set; }

        /// <summary>
        /// List selected for data generation.
        /// </summary>
        public SPGList SelectedSPGList { get; set; }

        /// <summary>
        /// Relative of SharePoint site collection's host web.
        /// </summary>
        public string HostWebUrl { get; set; }

        /// <summary>
        /// Url to currently selected list's default view.
        /// </summary>
        public string SelectedSPGListAbsoluteUrl
        {
            get
            {
                var serverUrl = HostWebUrl.Split(new string[] { "/sites" }, StringSplitOptions.None).First();
                return serverUrl + SelectedSPGList.ServerRelativeUrl;
            }
        }
    }
}