using Generation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Service
{
    public interface IPowerPlantService
    {
        Task Create(PowerPlantDto powerPlantDto);
        Task<PowerPlantDto> Update(PowerPlantDto powerPlant);
        Task<List<PowerPlantDto>> GetAll();
        Task<PowerPlantDto> GetById(Guid id);
    }
}
