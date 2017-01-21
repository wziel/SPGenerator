using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPGenerator.Generator.Database;
using SPGenerator.Generator.DAO;

namespace SPGenerator.Generator.ColumnDataGenerator.MultilineText
{
    public class MultilineTextDataGenerator : ColumnDataGenerator<MultilineTextColumnPOCO>, IMultilineTextDataGenerator
    {
        private ITextDAO textDAO;

        public MultilineTextDataGenerator(ITextDAO textDAO)
        {
            this.textDAO = textDAO;
        }

        protected override IEnumerable<object> GenerateData(MultilineTextColumnPOCO column, int recordsCount)
        {
            var textSamples = textDAO.GetRandomTexts(recordsCount);
            while (recordsCount-- > 0)
            {
                var text = textSamples[recordsCount % textSamples.Count];
                var maxLength = Math.Min(text.Length, column.MaxLength);
                var length = RANDOM.Next(column.MinLength, maxLength);
                yield return text.Substring(0, length);
            }
        }
    }
}
