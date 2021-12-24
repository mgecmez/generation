using Generation.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Repository
{
    public class GenerationDbContext : DbContext
    {
        public GenerationDbContext(DbContextOptions<GenerationDbContext> options)
            : base(options)
        {

        }

        public DbSet<PowerPlant> PowerPlants { get; set; }
        public DbSet<TimedValue> TimedValues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
