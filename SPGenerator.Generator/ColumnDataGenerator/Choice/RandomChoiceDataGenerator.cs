using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Choice
{
    public class RandomChoiceDataGenerator : ColumnDataGenerator<ChoiceColumnPOCO>
    {
        public RandomChoiceDataGenerator(ChoiceColumnPOCO column) : base(column)
        {
            //left empty
        }

        public override IEnumerable<object> GenerateData(int recordsCount)
        {
            var data = new List<object>(recordsCount);
            var choiceCount = column.Choices.Count();
            while (recordsCount-- > 0)
            {
                var idx = RANDOM.Next(choiceCount);
                data.Add(column.Choices[idx]);
            }
            return data;
        }
    }
}
