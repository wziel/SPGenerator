using SPGenerator.AddinWeb.ViewModels.Home.Column;
using SPGenerator.Model;
using SPGenerator.Model.Column;
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
        public List<ListVM> ListVMs { get; set; }

        /// <summary>
        /// Relative of SharePoint site collection's host web.
        /// </summary>
        public string HostWebUrl { get; set; }

        /// <summary>
        /// Number of EntryPOCO objects to generate.
        /// </summary>
        [DisplayName("Liczba rekordów")]
        [Range(1, 2000)]
        public int RecordsToGenerateCount { get; set; }

        /// <summary>
        /// List selected for data generation.
        /// </summary>
        public ListVM SelectedListVM { get; set; }

        /// <summary>
        /// Url to currently selected list's default view.
        /// </summary>
        public string SelectedListAbsoluteUrl
        {
            get
            {
                if(SelectedListVM != null)
                {
                    var serverUrl = HostWebUrl.Split(new string[] { "/sites" }, StringSplitOptions.None).First();
                    return serverUrl + SelectedListVM.ServerRelativeUrl;
                }
                return null;
            }
        }

        public bool ShowListVMs
        {
            get
            {
                return ListVMs != null && ListVMs.Any();
            }
        }

        public bool ShowColumnVMs
        {
            get
            {
                return TextColumnVMs.Any() || NumberColumnVMs.Any();
            }
        }

        public List<ColumnVM> AllColumnVMs
        {
            get
            {
                var allColumnVMs = new List<ColumnVM>();
                allColumnVMs.AddRange(TextColumnVMs);
                allColumnVMs.AddRange(NumberColumnVMs);
                return allColumnVMs;
            }
        }

        public List<TextColumnVM> TextColumnVMs { get; set; } = new List<TextColumnVM>();
        public List<NumberColumnVM> NumberColumnVMs { get; set; } = new List<NumberColumnVM>();
    }
}