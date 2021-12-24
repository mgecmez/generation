using Generation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Service
{
    public interface ITimedValueService
    {
        Task Create(TimedValueDto timedValueDto);
        Task<TimedValueDto> Update(TimedValueDto timedValue);
        Task<List<TimedValueDto>> GetAll();
        Task<TimedValueDto> GetById(Guid id);
        Task<ApiResponseDto<TimedValuesResponseDto>> GetAllTimedValuesByWebIdAndTime(string webId, string startTime, string endTime);
    }
}
