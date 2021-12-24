using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Domain
{
    public interface ITimedValueRepository : IGenericRepository<TimedValue>
    {
        Task<List<TimedValue>> GetAllTimedValuesAsync();
        Task<List<TimedValue>> GetAllTimedValuesByWebIdAndTime(string webId, string startTime, string endTime);
    }
}
