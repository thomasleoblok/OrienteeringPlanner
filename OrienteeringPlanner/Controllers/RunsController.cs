using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrienteeringPlanner.Data;
using OrienteeringPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrienteeringPlanner.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class RunsController : ControllerBase
    {
        private readonly OrienteeringPlannerContext _context;

        public RunsController(OrienteeringPlannerContext context)
        {
            _context = context;
        }

        // GET: api/Runs/GetUpcomingRuns
        [HttpGet("GetUpcomingRuns")]
        public async Task<ActionResult<IEnumerable<Run>>> GetUpcomingRuns()
        {
            var today = DateTime.Now;

            //var upcomingRuns = await _context.Run.Where(run => run.StartDateTime > today && run.EndDateTime < today.AddDays(7)).ToListAsync();
            var upcomingRuns = await _context.Run.Where(s => s.Id > 0).ToListAsync();

            return upcomingRuns;
        }

        // GET api/<RunsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Run>> GetRun(int runId)
        {
            return new Run();
        }

        // POST api/<RunsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RunsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RunsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
