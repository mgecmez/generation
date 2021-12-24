using Generation.Service;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Generation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IRecurringJobManager _recurringJobManager;

        public JobController(IJobService jobService, IRecurringJobManager recurringJobManager)
        {
            _jobService = jobService;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet("/ReccuringJob")]
        public ActionResult CreateReccuringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _jobService.ReccuringJob(), "0 * * * *");
            return Ok();
        }
    }
}
