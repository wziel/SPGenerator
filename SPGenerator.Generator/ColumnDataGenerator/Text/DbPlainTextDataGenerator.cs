using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPGenerator.Generator.Database;

namespace SPGenerator.Generator.ColumnDataGenerator.Text
{
    public class DbPlainTextDataGenerator : DbColumnDataGenerator<TextColumnPOCO, string>, ITextDataGenerator
    {
        protected override List<string> FetchData(GeneratorDbContext db, TextColumnPOCO column, int recordsCount)
        {
            return db.Texts.OrderBy(x => Guid.NewGuid()).Take(recordsCount).Select(x => x.Content).ToList();
        }

        protected override IEnumerable<object> GenerateData(List<string> textSamples, TextColumnPOCO column, int recordsCount)
        {
            while (recordsCount-- > 0)
            {
                var text = textSamples[recordsCount % textSamples.Count()];
                var length = RANDOM.Next(column.MinLength, column.MaxLength);
                yield return text.Substring(0, length);
            }
        }
    }
}
