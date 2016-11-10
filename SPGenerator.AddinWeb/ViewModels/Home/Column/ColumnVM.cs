using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public abstract class ColumnVM : IValidatableObject
    {
        protected ColumnVM()
        {
            //intentionally left empty, constructor for Razor
        }

        protected ColumnVM(ColumnPOCO columnPOCO)
        {
            InternalName = columnPOCO.InternalName;
            DisplayName = columnPOCO.DisplayName;
            GenerateData = true;
            Required = columnPOCO.Required;
        }

        /// <summary>
        /// Name of this column.
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Display name of this column.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Should data generator generate data for this column.
        /// </summary>
        [DisplayName("Generuj dane")]
        public bool GenerateData { get; set; }

        /// <summary>
        /// Is this a required column of the list.
        /// </summary>
        public bool Required { get; set; }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        public abstract ColumnPOCO ColumnPOCO { get; }
    }
}