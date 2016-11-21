using SPGenerator.Generator.ColumnDataGenerator.Boolean;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Shared
{
    public class NullDataGenerator : ISharedDataGenerator
    {
        public IEnumerable<object> GenerateData(ColumnPOCO column, int recordsCount)
        {
            while (recordsCount-- > 0)
            {
                yield return null;
            }
        }

        public bool CanGenerateData(ColumnPOCO column)
        {
            return !column.Required && column.GenerateData;
        }
    }
}
