using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
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
            ColumnName = columnPOCO.ColumnName;
        }

        /// <summary>
        /// Name of this column.
        /// </summary>
        public string ColumnName { get; set; }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        public abstract ColumnPOCO ColumnPOCO { get; }
    }
}