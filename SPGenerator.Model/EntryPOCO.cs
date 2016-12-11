using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Model
{
    /// <summary>
    /// An entry in a list that contains values of columns.
    /// </summary>
    public class EntryPOCO
    {
        private Dictionary<ColumnPOCO, object> columnValues
            = new Dictionary<ColumnPOCO, object>();

        /// <summary>
        /// Returns a value of a specified column.
        /// </summary>
        /// <param name="column">Column for which a value is to be returned.</param>
        /// <returns>Value of a specified column.</returns>
        public object GetValue(ColumnPOCO column)
        {
            return columnValues[column];
        }

        /// <summary>
        /// Sets value of a column to this entry.
        /// </summary>
        /// <param name="column">Column for which value is to be added.</param>
        /// <param name="value">Value of the column.</param>
        public void SetValue(ColumnPOCO column, object value)
        {
            columnValues.Add(column, value);
        }
    }
}
