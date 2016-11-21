using SPGenerator.Generator.Database;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator
{
    public abstract class DbColumnDataGenerator<TColumnPOCO, TDbReturn> : ColumnDataGenerator<TColumnPOCO> 
        where TColumnPOCO : ColumnPOCO
    {
        protected sealed override IEnumerable<object> GenerateData(TColumnPOCO column, int recordsCount)
        {
            List<TDbReturn> dbData;
            using (var db = new GeneratorDbContext())
            {
                dbData = FetchData(db, column, recordsCount);
            }
            return GenerateData(dbData, column, recordsCount);
        }

        protected abstract List<TDbReturn> FetchData(GeneratorDbContext db, TColumnPOCO column, int recordsCount);

        protected abstract IEnumerable<object> GenerateData(List<TDbReturn> dbData, TColumnPOCO column, int recordsCount);
    }
}