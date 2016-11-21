using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Choice
{
    public class RandomChoiceDataGenerator : ColumnDataGenerator<ChoiceColumnPOCO>, IChoiceDataGenerator
    {
        protected override IEnumerable<object> GenerateData(ChoiceColumnPOCO column, int recordsCount)
        {
            var choiceCount = column.Choices.Count();
            while (recordsCount-- > 0)
            {
                var idx = RANDOM.Next(choiceCount);
                yield return column.Choices[idx];
            }
        }
    }
}
