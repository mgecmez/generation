using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Domain
{
    public interface IPowerPlantRepository : IGenericRepository<PowerPlant>
    {
        Task<List<PowerPlant>> GetAllPowerPlantsAsync();
    }
}
