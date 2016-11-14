using SPGenerator.Generator.Database.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.Database
{
    public class GeneratorDbContext : DbContext
    {
        public GeneratorDbContext() : base("GeneratorDbContext")
        {
            System.Data.Entity.Database.SetInitializer(new GeneratorDbInitializer());
        }

        public DbSet<Text> Texts { get; set; }
    }
}
