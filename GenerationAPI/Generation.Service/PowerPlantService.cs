using Generation.Domain;
using Generation.Dto;
using Generation.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Service
{
    public class PowerPlantService : IPowerPlantService
    {
        private readonly IPowerPlantRepository _powerPlantRepository;
        private readonly IPowerPlantMapper _powerPlantMapper;

        public PowerPlantService(IPowerPlantRepository powerPlantRepository, IPowerPlantMapper powerPlantMapper)
        {
            _powerPlantRepository = powerPlantRepository;
            _powerPlantMapper = powerPlantMapper;
        }

        public async Task Create(PowerPlantDto powerPlantDto)
        {
            var powerPlant = _powerPlantMapper.ToPowerPlant(powerPlantDto);
            await _powerPlantRepository.Save(powerPlant);
        }

        public async Task<List<PowerPlantDto>> GetAll()
        {
            var all = await _powerPlantRepository.GetAllPowerPlantsAsync();
            return _powerPlantMapper.ToPowerPlantDtoList(all);
        }

        public async Task<PowerPlantDto> GetById(Guid id)
        {
            var powerPlant = await _powerPlantRepository.Get(id);
            if (powerPlant == null)
            {
                throw new Exception("Powerplant with this id: " + id + " not found.");
            }

            var powerPlantDto = _powerPlantMapper.ToPowerPlantDto(powerPlant);
            return powerPlantDto;
        }

        public async Task<PowerPlantDto> Update(PowerPlantDto powerPlant)
        {
            var existing = await _powerPlantRepository.Get(powerPlant.Id);

            existing.SetFields(powerPlant.WebId);
            await _powerPlantRepository.Update(existing);

            var powerPlantDto = _powerPlantMapper.ToPowerPlantDto(existing);
            return powerPlantDto;
        }
    }
}
