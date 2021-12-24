using Generation.Domain;
using Generation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Service.Mappers
{
    public interface IPowerPlantMapper
    {
        PowerPlant ToPowerPlant(PowerPlantDto powerPlantDto);
        PowerPlantDto ToPowerPlantDto(PowerPlant powerPlant);
        List<PowerPlantDto> ToPowerPlantDtoList(List<PowerPlant> powerPlants);
    }
}
