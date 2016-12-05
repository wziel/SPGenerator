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
    public class IndexVM : IValidatableObject
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
                return AllColumnVMs.Any();
            }
        }

        internal void ApplyTo(ListPOCO listPOCO)
        {
            foreach(var columnVM in AllColumnVMs)
            {
                var columnPOCO = listPOCO.ColumnPOCOList.FirstOrDefault(c => c.InternalName == columnVM.InternalName);
                if (columnPOCO == null)
                {
                    throw new GUIVisibleException("Nie znaleziono kolumny o nazwie " + columnVM.InternalName);
                }
                columnVM.ApplyTo(columnPOCO);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach(var columnVM in AllColumnVMs)
            {
                foreach(var validationResult in columnVM.Validate(validationContext))
                {
                    yield return validationResult;
                }
            }
        }

        public List<IColumnVM> AllColumnVMs
        {
            get
            {
                var allColumnVMs = new List<IColumnVM>();
                allColumnVMs.AddRange(TextColumnVMs);
                allColumnVMs.AddRange(NumberColumnVMs);
                allColumnVMs.AddRange(MultilineTextColumnVMs);
                allColumnVMs.AddRange(ChoiceColumnVMs);
                allColumnVMs.AddRange(DateTimeColumnVMs);
                allColumnVMs.AddRange(BooleanColumnVMs);
                allColumnVMs.AddRange(CurrencyColumnVMs);
                return allColumnVMs;
            }
        }

        public List<TextColumnVM> TextColumnVMs { get; set; } = new List<TextColumnVM>();
        public List<MultilineTextColumnVM> MultilineTextColumnVMs { get; set; } = new List<MultilineTextColumnVM>();
        public List<NumberColumnVM> NumberColumnVMs { get; set; } = new List<NumberColumnVM>();
        public List<ChoiceColumnVM> ChoiceColumnVMs { get; set; } = new List<ChoiceColumnVM>();
        public List<DateTimeColumnVM> DateTimeColumnVMs { get; set; } = new List<DateTimeColumnVM>();
        public List<BooleanColumnVM> BooleanColumnVMs { get; set; } = new List<BooleanColumnVM>();
        public List<CurrencyColumnVM> CurrencyColumnVMs { get; set; } = new List<CurrencyColumnVM>();

    }
}