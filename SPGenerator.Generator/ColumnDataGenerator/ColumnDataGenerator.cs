using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator
{
    /// <summary>
    /// Class of objects that generate values for column.
    /// </summary>
    public abstract class ColumnDataGenerator<TColumnPOCO> : IColumnDataGenerator where TColumnPOCO : ColumnPOCO
    {
        protected static readonly Random RANDOM = new Random();
        protected TColumnPOCO column;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="column">Column for which data will be generated.</param>
        protected ColumnDataGenerator(TColumnPOCO column)
        {
            this.column = column;
        }

        /// <summary>
        /// Generates data.
        /// </summary>
        /// <param name="recordsCount">Number of records to generated.</param>
        /// <returns>Generated data.</returns>
        public abstract IEnumerable<object> GenerateData(int recordsCount);
        
        /// <summary>
        /// Checks if data can be generated.
        /// </summary>
        public abstract bool CanGenerateData { get; }
    }

    public interface IColumnDataGenerator
    {
        /// <summary>
        /// Generates data.
        /// </summary>
        /// <param name="column">Column for which data will be generated.</param>
        /// <param name="recordsCount">Number of records to generated.</param>
        /// <returns>Generated data.</returns>
        IEnumerable<object> GenerateData(int recordsCount);

        /// <summary>
        /// Checks if data can be generated.
        /// </summary>
        bool CanGenerateData { get; }
    }
}
