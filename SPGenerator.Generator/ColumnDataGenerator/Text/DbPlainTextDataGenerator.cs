using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPGenerator.Generator.Database;

namespace SPGenerator.Generator.ColumnDataGenerator.Text
{
    public class DbPlainTextDataGenerator : DbColumnDataGenerator<TextColumnPOCO>
    {
        public DbPlainTextDataGenerator(TextColumnPOCO column) : base(column)
        {
            //left empty
        }

        public override bool CanGenerateData
        {
            get
            {
                return column.GenerateData;
            }
        }

        protected override IEnumerable<object> GenerateData(GeneratorDbContext db, int recordsCount)
        {
            var textSamples = db.Texts.OrderBy(x => Guid.NewGuid()).Take(recordsCount).Select(x => x.Content).ToList();
            var data = new List<object>(recordsCount);
            while (recordsCount-- > 0)
            {
                var text = textSamples[recordsCount % textSamples.Count];
                var length = RANDOM.Next(column.MinLength, column.MaxLength);
                var substring = text.Substring(0, length);
                data.Add(substring);
            }
            return data;
        }
    }
}
