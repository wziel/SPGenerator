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
        public List<ListPOCO> ListPOCOs { get; set; }

        /// <summary>
        /// Relative of SharePoint site collection's host web.
        /// </summary>
        public string HostWebUrl { get; set; }

        /// <summary>
        /// Title of the selected ListPOCO.
        /// </summary>
        [DisplayName("Lista docelowa")]
        public string SelectedListPOCOTitle { get; set; }

        /// <summary>
        /// Number of EntryPOCO objects to generate.
        /// </summary>
        [DisplayName("Liczba rekordów")]
        [Range(1, 2000)]
        public int RecordsToGenerateCount { get; set; }

        /// <summary>
        /// List selected for data generation.
        /// </summary>
        public ListPOCO SelectedListPOCO { get; set; }

        /// <summary>
        /// Url to currently selected list's default view.
        /// </summary>
        public string SelectedListPOCOAbsoluteUrl
        {
            get
            {
                if(SelectedListPOCO != null)
                {
                    var serverUrl = HostWebUrl.Split(new string[] { "/sites" }, StringSplitOptions.None).First();
                    return serverUrl + SelectedListPOCO.ServerRelativeUrl;
                }
                return null;
            }
        }

        /// <summary>
        /// Data for List selector in the view.
        /// </summary>
        public SelectList ListPOCOSelectList
        {
            get
            {
                var selectListItems = ListPOCOs.Select(listPOCO => new SelectListItem()
                {
                    Text = listPOCO.Title,
                    Value = listPOCO.Title,
                    Selected = listPOCO == SelectedListPOCO
                }).ToList();
                return new SelectList(selectListItems, "Text", "Value");
            }
        }

        public bool ShowListPOCOs
        {
            get
            {
                return ListPOCOs != null && ListPOCOs.Any();
            }
        }
    }
}