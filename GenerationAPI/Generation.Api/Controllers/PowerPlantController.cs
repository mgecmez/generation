using Generation.Dto;
using Generation.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Generation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerPlantController : ControllerBase
    {
        private readonly IPowerPlantService _powerPlantService;

        public PowerPlantController(IPowerPlantService powerPlantService)
        {
            _powerPlantService = powerPlantService;
        }

        // GET api/powerPlant
        [HttpGet]
        public async Task<ActionResult<List<PowerPlantDto>>> Get()
        {
            return Ok(await _powerPlantService.GetAll());
        }

        // GET api/powerPlant/id
        [HttpGet("{id}")]
        public async Task<ActionResult<PowerPlantDto>> Get(Guid id)
        {
            return Ok(await _powerPlantService.GetById(id));
        }

        // POST api/powerPlant
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PowerPlantDto powerPlant)
        {
            await _powerPlantService.Create(powerPlant);
            return Ok();
        }

        // PUT api/powerPlant
        [HttpPut]
        public async Task<ActionResult<PowerPlantDto>> Put([FromBody] PowerPlantDto powerPlant)
        {
            return Ok(await _powerPlantService.Update(powerPlant));
        }
    }
}
