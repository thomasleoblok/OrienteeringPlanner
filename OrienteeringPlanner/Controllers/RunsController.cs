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
        [HttpGet("GetUpcomingRuns/{searchDaysAhead}")]
        public async Task<ActionResult<IEnumerable<Run>>> GetUpcomingRuns(int searchDaysAhead)
        {
            var today = DateTime.Now;
            var searchSpanDate = today.AddDays(searchDaysAhead);

            var upcomingRuns = await _context.Run.Where(run => run.EndDateTime > today && run.StartDateTime < searchSpanDate).ToListAsync();

            return upcomingRuns;
        }

        // GET: api/Runs/GetUpcomingRuns
        [HttpGet("GetUpcomingRunsForClub")]
        public async Task<ActionResult<IEnumerable<Run>>> GetUpcomingRunsForClub(Club club)
        {
            try
            {
                var dbClub = await _context.Club.FindAsync(club.Id);

                if (dbClub.Token == club.Token)
                {
                    var today = DateTime.Now;

                    var upcomingRuns = await _context.Run.Where(run => run.EndDateTime > today && run.ClubId == dbClub.Id).ToListAsync();

                    return upcomingRuns;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        // GET api/<RunsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Run>> GetRun(int runId)
        {
            return await _context.Run.FindAsync(runId);
        }

        // POST api/<RunsController>
        [HttpPost("CreateRun")]
        public async Task<ActionResult<Run>> CreateRun(Run run)
        {
            var newRun = await _context.Run.AddAsync(run);
            await _context.SaveChangesAsync();

            return newRun.Entity;

        }

        // PUT api/<RunsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPost("DeleteRun")]
        public async Task<ActionResult> Delete(Run run)
        {
            try
            {
                var dbRun = await _context.Run.FindAsync(run.Id);

                if (dbRun == null)
                {
                    return NotFound();
                }

                if (run.ClubId == dbRun.ClubId)
                {
                    _context.Run.Remove(dbRun);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else 
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
