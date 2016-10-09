using SPGenerator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        /// Relative of SharePoint site collection's host web.
        /// </summary>
        public string HostWebUrl { get; set; }

        /// <summary>
        /// Title of the selected SPGList.
        /// </summary>
        [DisplayName("Lista docelowa")]
        public string SelectedSPGListTitle { get; set; }

        /// <summary>
        /// Number of SPGEntry objects to generate.
        /// </summary>
        [DisplayName("Liczba rekordów")]
        [Range(1, 2000)]
        public int RecordsToGenerateCount { get; set; }

        /// <summary>
        /// List selected for data generation.
        /// </summary>
        public SPGList SelectedSPGList { get; set; }

        /// <summary>
        /// Url to currently selected list's default view.
        /// </summary>
        public string SelectedSPGListAbsoluteUrl
        {
            get
            {
                if(SelectedSPGList != null)
                {
                    var serverUrl = HostWebUrl.Split(new string[] { "/sites" }, StringSplitOptions.None).First();
                    return serverUrl + SelectedSPGList.ServerRelativeUrl;
                }
                return null;
            }
        }

        /// <summary>
        /// Data for List selector in the view.
        /// </summary>
        public SelectList SPGListsSelectList
        {
            get
            {
                var selectListItems = SPGLists.Select(spgList => new SelectListItem()
                {
                    Text = spgList.Title,
                    Value = spgList.Title,
                    Selected = spgList == SelectedSPGList
                }).ToList();
                return new SelectList(selectListItems, "Text", "Value");
            }
        }

        public bool ShowSPGLists
        {
            get
            {
                return SPGLists != null && SPGLists.Any();
            }
        }
    }
}