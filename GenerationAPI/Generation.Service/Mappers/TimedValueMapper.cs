using Generation.Domain;
using Generation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Service.Mappers
{
    public class TimedValueMapper : ITimedValueMapper
    {
        public TimedValue ToTimedValue(TimedValueDto timedValueDto)
        {
            return new TimedValue(timedValueDto.PowerPlantId, timedValueDto.Timestamp, timedValueDto.Good, timedValueDto.Value.ToString());
        }

        public TimedValueDto ToTimedValueDto(TimedValue timedValue)
        {
            return new TimedValueDto
            {
                Id = timedValue.Id,
                Timestamp = timedValue.Timestamp,
                Good = timedValue.Good,
                Value = timedValue.Value
            };
        }

        public List<TimedValueDto> ToTimedValueDtoList(List<TimedValue> timedValues)
        {
            var response = new List<TimedValueDto>();

            foreach (var timedValue in timedValues)
            {
                var timedValueDto = ToTimedValueDto(timedValue);
                response.Add(timedValueDto);
            }

            return response;
        }

        public ApiResponseDto<TimedValuesResponseDto> ToTimedValueResponseDtoList(List<TimedValueDto> timedValuesDto)
        {
            var response = new TimedValuesResponseDto();

            foreach (var timedValueDto in timedValuesDto)
            {
                response.Items.Add(new TimedValueResponseDto
                {
                    Timestamp = timedValueDto.Timestamp,
                    Good = timedValueDto.Good,
                    Value = timedValueDto.Value
                });
            }

            return ApiResponseDto<TimedValuesResponseDto>.Set(response);
        }
    }
}
