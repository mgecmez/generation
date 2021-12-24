using Generation.Domain;
using Generation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Service.Mappers
{
    public class PowerPlantMapper : IPowerPlantMapper
    {
        public PowerPlant ToPowerPlant(PowerPlantDto powerPlantDto)
        {
            return new PowerPlant(powerPlantDto.WebId);
        }

        public PowerPlantDto ToPowerPlantDto(PowerPlant powerPlant)
        {
            return new PowerPlantDto
            {
                WebId = powerPlant.WebId
            };
        }

        public List<PowerPlantDto> ToPowerPlantDtoList(List<PowerPlant> powerPlants)
        {
            var response = new List<PowerPlantDto>();

            foreach (var powerPlant in powerPlants)
            {
                var powerPlantDto = ToPowerPlantDto(powerPlant);
                response.Add(powerPlantDto);
            }

            return response;
        }
    }
}
