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
        /// <summary>
        /// Returns a value of a specified column.
        /// </summary>
        /// <param name="column">Column for which a value is to be returned.</param>
        /// <returns>Value of a specified column.</returns>
        public string GetValue(ColumnPOCO column)
        {
            throw new NotImplementedException();
        }
    }
}
