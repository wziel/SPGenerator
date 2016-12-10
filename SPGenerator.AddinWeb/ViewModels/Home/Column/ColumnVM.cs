using SPGenerator.Model;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPGenerator.AddinWeb.ViewModels.Home.Column
{
    public abstract class ColumnVM<TMappedColumnPOCO> : IColumnVM where TMappedColumnPOCO : ColumnPOCO
    {
        protected TMappedColumnPOCO columnPOCO;

        protected ColumnVM()
        {
            //intentionally left empty, constructor for Razor
        }

        protected ColumnVM(TMappedColumnPOCO columnPOCO)
        {
            InternalName = columnPOCO.InternalName;
            DisplayName = columnPOCO.DisplayName;
            GenerateData = columnPOCO.GenerateData;
            Required = columnPOCO.Required;
        }

        [DisplayName("Generuj dane")]
        public bool GenerateData { get; set; }

        public string InternalName { get; set; }
        
        public string DisplayName { get; set; }
        
        public bool Required { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(columnPOCO != null && !GenerateData && columnPOCO.Required)
            {
                yield return new ValidationResult($"Kolumna {InternalName} jest wymagana i nie można nie generować na nią danych",
                    new[] { nameof(GenerateData)});
            }
        }

        public virtual void ApplyTo(ColumnPOCO columnPOCO)
        {
            var specificColumn = columnPOCO as TMappedColumnPOCO;
            if(specificColumn == null)
            {
                throw new GUIVisibleException($"Kolumna {InternalName} nie jest odpowiedniego typu");
            }
            this.columnPOCO = specificColumn;
            columnPOCO.GenerateData = columnPOCO.Required ? true : GenerateData;
        }
    }

    public interface IColumnVM : IValidatableObject
    {
        /// <summary>
        /// Name of this column.
        /// </summary>
        string InternalName { get; set; }

        /// <summary>
        /// Display name of this column.
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// Should data generator generate data for this column.
        /// </summary>
        bool GenerateData { get; set; }

        /// <summary>
        /// Is this a required column of the list.
        /// </summary>
        bool Required { get; set; }

        /// <summary>
        /// Aplies field values to POCO from this VM.
        /// </summary>
        /// <param name="columnPOCO">POCO to which values should be applied.</param>
        void ApplyTo(ColumnPOCO columnPOCO);
    }
}