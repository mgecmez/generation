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
    public class TimedValueService : ITimedValueService
    {
        private readonly ITimedValueRepository _timedValueRepository;
        private readonly ITimedValueMapper _timedValueMapper;

        public TimedValueService(ITimedValueRepository timedValueRepository, ITimedValueMapper timedValueMapper)
        {
            _timedValueRepository = timedValueRepository;
            _timedValueMapper = timedValueMapper;
        }

        public async Task Create(TimedValueDto timedValueDto)
        {
            var timedValue = _timedValueMapper.ToTimedValue(timedValueDto);
            await _timedValueRepository.Save(timedValue);
        }

        public async Task<List<TimedValueDto>> GetAll()
        {
            var all = await _timedValueRepository.GetAllTimedValuesAsync();
            return _timedValueMapper.ToTimedValueDtoList(all);
        }

        public async Task<TimedValueDto> GetById(Guid id)
        {
            var timedValue = await _timedValueRepository.Get(id);
            if (timedValue == null)
            {
                throw new Exception("Timed value with this id: " + id + " not found.");
            }

            var timedValueDto = _timedValueMapper.ToTimedValueDto(timedValue);
            return timedValueDto;
        }

        public async Task<ApiResponseDto<TimedValuesResponseDto>> GetAllTimedValuesByWebIdAndTime(string webId, string startTime, string endTime)
        {
            var all = await _timedValueRepository.GetAllTimedValuesByWebIdAndTime(webId, startTime, endTime);
            var timedValueDto = _timedValueMapper.ToTimedValueDtoList(all);

            return _timedValueMapper.ToTimedValueResponseDtoList(timedValueDto);
        }

        public async Task<TimedValueDto> Update(TimedValueDto timedValue)
        {
            var existing = await _timedValueRepository.Get(timedValue.Id);

            existing.SetFields(timedValue.PowerPlantId, timedValue.Timestamp, timedValue.Good, timedValue.Value.ToString());
            await _timedValueRepository.Update(existing);

            var timedValueDto = _timedValueMapper.ToTimedValueDto(existing);
            return timedValueDto;
        }
    }
}
