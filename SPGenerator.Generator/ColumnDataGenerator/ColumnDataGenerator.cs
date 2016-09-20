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
    public class ColumnDataGenerator
    {
        /// <summary>
        /// Default constructor of column data generator.
        /// </summary>
        /// <param name="column">Column for which data will be generated.</param>
        public ColumnDataGenerator(SPGColumn column)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates data.
        /// </summary>
        /// <param name="recordsCount">Number of records to generated.</param>
        /// <returns>Generated data.</returns>
        public IEnumerable<string> GenerateData(int recordsCount)
        {
            throw new NotImplementedException();
        }
    }
}
