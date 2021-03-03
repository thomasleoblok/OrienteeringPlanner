﻿using Microsoft.AspNetCore.Mvc;
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

            var upcomingRuns = await _context.Run.Where(run => run.StartDateTime > today && run.StartDateTime < searchSpanDate).ToListAsync();

            return upcomingRuns;
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

        // DELETE api/<RunsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
