using Generation.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Repository
{
    public class PowerPlantRepository : GenericRepository<PowerPlant>, IPowerPlantRepository
    {
        public PowerPlantRepository(GenerationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PowerPlant>> GetAllPowerPlantsAsync()
        {
            return await All().ToListAsync();
        }
    }
}
