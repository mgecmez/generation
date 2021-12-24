using Generation.Domain;
using Generation.Dto;
using Generation.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Generation.Service
{
    public class JobService : IJobService
    {
        private readonly IPowerPlantRepository _powerPlantRepository;
        private readonly ITimedValueRepository _timedValueRepository;
        private readonly ITimedValueMapper _timedValueMapper;

        public JobService(
            IPowerPlantRepository powerPlantRepository,
            ITimedValueRepository timedValueRepository,
            ITimedValueMapper timedValueMapper)
        {
            _powerPlantRepository = powerPlantRepository;
            _timedValueRepository = timedValueRepository;
            _timedValueMapper = timedValueMapper;
        }

        public async Task ReccuringJob()
        {
            var powerplants = _powerPlantRepository.All().ToList();

            foreach (var powerplant in powerplants)
            {
                var rand = new Random();
                var good = rand.NextDouble() >= 0.5;

                if (good)
                {
                    var timedValue = new TimedValueDto
                    {
                        PowerPlantId = powerplant.Id,
                        Timestamp = DateTime.Now,
                        Good = good,
                        Value = rand.NextDouble()
                    };

                    await _timedValueRepository.Save(_timedValueMapper.ToTimedValue(timedValue));
                }
                else
                {
                    List<string> ErrorCodes = new List<string>
                    {
                        "Err1", "Err2", "Err3", "Err4"
                    };

                    List<string> ErrorMessages = new List<string>
                    {
                        "Error 1 message",
                        "Error 2 message",
                        "Error 3 message",
                        "Error 4 message"
                    };

                    var randErrIndex = rand.Next(0, ErrorCodes.Count);

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var jsonError = JsonSerializer.Serialize(new { ErrCode = ErrorCodes[randErrIndex], ErrMessage = ErrorMessages[randErrIndex] }, options);

                    var timedValue = new TimedValueDto
                    {
                        PowerPlantId = powerplant.Id,
                        Timestamp = DateTime.Now,
                        Good = good,
                        Value = jsonError
                    };

                    await _timedValueRepository.Save(_timedValueMapper.ToTimedValue(timedValue));
                }
            }
        }
    }
}
