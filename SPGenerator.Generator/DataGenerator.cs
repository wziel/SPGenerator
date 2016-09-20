using SPGenerator.Model;
using SPGenerator.Model.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator
{
    /// <summary>
    /// Class of objects responsible for generating entries for lists.
    /// </summary>
    public class DataGenerator
    {
        /// <summary>
        /// Generates entries for list.
        /// </summary>
        /// <param name="list">List for which entries will be generated..</param>
        /// <param name="recordsCount">Number of entries to generate.</param>
        /// <returns>Generated entries.</returns>
        public IEnumerable<SPGEntry> GenerateData(SPGList list, int recordsCount)
        {
            throw new NotImplementedException();
        }
    }
}
