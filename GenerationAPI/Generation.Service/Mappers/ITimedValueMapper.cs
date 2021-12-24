using Generation.Domain;
using Generation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Service.Mappers
{
    public interface ITimedValueMapper
    {
        TimedValue ToTimedValue(TimedValueDto timedValueDto);
        TimedValueDto ToTimedValueDto(TimedValue timedValue);
        List<TimedValueDto> ToTimedValueDtoList(List<TimedValue> timedValues);
        ApiResponseDto<TimedValuesResponseDto> ToTimedValueResponseDtoList(List<TimedValueDto> timedValuesDto);
    }
}
