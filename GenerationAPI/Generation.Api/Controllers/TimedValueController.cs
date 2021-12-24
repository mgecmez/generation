using Generation.Dto;
using Generation.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimedValueController : ControllerBase
    {
        private readonly ITimedValueService _timedValueService;

        public TimedValueController(ITimedValueService timedValueService)
        {
            _timedValueService = timedValueService;
        }

        // GET api/timedValue
        [HttpGet]
        public async Task<ActionResult<List<TimedValueDto>>> Get()
        {
            return Ok(await _timedValueService.GetAll());
        }

        // GET api/timedValue/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TimedValueDto>> Get(Guid id)
        {
            return Ok(await _timedValueService.GetById(id));
        }

        [HttpGet("{webId}/{startTime}/{endTime}")]
        public async Task<ApiResponseDto<TimedValuesResponseDto>> Get(string webId, string startTime, string endTime)
        {
            return await _timedValueService.GetAllTimedValuesByWebIdAndTime(webId, startTime, endTime);
        }

        // POST api/timedValue
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TimedValueDto timedValue)
        {
            await _timedValueService.Create(timedValue);
            return Ok();
        }

        // PUT api/timedValue
        [HttpPut]
        public async Task<ActionResult<TimedValueDto>> Put([FromBody] TimedValueDto timedValue)
        {
            return Ok(await _timedValueService.Update(timedValue));
        }
    }
}
