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

        public bool CanGenerateData(ColumnPOCO column)
        {
            if(!(column is TColumnPOCO))
            {
                return false;
            }
            var specificColumn = column as TColumnPOCO;
            return CanGenerateData(specificColumn);
        }

        protected virtual bool CanGenerateData(TColumnPOCO column)
        {
            return column.GenerateData;
        }

        public IEnumerable<object> GenerateData(ColumnPOCO column, int recordsCount)
        {
            var specificColumn = column as TColumnPOCO;
            return GenerateData(specificColumn, recordsCount);
        }

        protected abstract IEnumerable<object> GenerateData(TColumnPOCO column, int recordsCount);
    }

    public interface IColumnDataGenerator
    {
        /// <summary>
        /// Generates data.
        /// </summary>
        /// <param name="column">Column for which data will be generated.</param>
        /// <param name="recordsCount">Number of records to generated.</param>
        /// <returns>Generated data.</returns>
        IEnumerable<object> GenerateData(ColumnPOCO column, int recordsCount);

        /// <summary>
        /// Checks if data can be generated.
        /// </summary>
        bool CanGenerateData(ColumnPOCO column);
    }
}