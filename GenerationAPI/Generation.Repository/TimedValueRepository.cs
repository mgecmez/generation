using Generation.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Repository
{
    public class TimedValueRepository : GenericRepository<TimedValue>, ITimedValueRepository
    {
        public TimedValueRepository(GenerationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<TimedValue>> GetAllTimedValuesAsync()
        {
            return await All().ToListAsync();
        }

        public async Task<List<TimedValue>> GetAllTimedValuesByWebIdAndTime(string webId, string startTime, string endTime)
        {
            var startDate = Convert.ToDateTime(startTime);
            var endDate = Convert.ToDateTime(endTime);
            return await Find(x => x.PowerPlant.WebId == webId && x.Timestamp >= startDate && x.Timestamp <= endDate).ToListAsync();
        }
    }
}
