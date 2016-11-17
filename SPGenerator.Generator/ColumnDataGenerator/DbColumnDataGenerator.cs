using SPGenerator.Generator.Database;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator
{
    public abstract class DbColumnDataGenerator<TColumnPOCO> : ColumnDataGenerator<TColumnPOCO> where TColumnPOCO : ColumnPOCO
    {
        protected DbColumnDataGenerator(TColumnPOCO column) : base(column)
        {
            //left empty
        }

        public sealed override IEnumerable<object> GenerateData(int recordsCount)
        {
            using (var db = new GeneratorDbContext())
            {
                return GenerateData(db, recordsCount);
            }
        }

        protected abstract IEnumerable<object> GenerateData(GeneratorDbContext db, int recordsCount);
    }
}