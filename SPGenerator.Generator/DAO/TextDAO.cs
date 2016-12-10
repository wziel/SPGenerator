using SPGenerator.Generator.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.DAO
{
    public class TextDAO : ITextDAO
    {
        public List<string> GetRandomTexts(int count)
        {
            using (var db = new GeneratorDbContext())
            {
                return db.Texts.OrderBy(x => Guid.NewGuid()).Take(count).Select(x => x.Content).ToList();
            }
        }
    }

    public interface ITextDAO
    {
        List<string> GetRandomTexts(int count);
    }
}
