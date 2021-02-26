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
    public class ClubsController : ControllerBase
    {
        private readonly OrienteeringPlannerContext _context;

        public ClubsController(OrienteeringPlannerContext context)
        {
            _context = context;
        }

        [HttpGet("Login")]
        public async Task<ActionResult<Club>> Login(LoginRequest loginRequest)
        {
            try
            {
                 return await _context.Club.FirstOrDefaultAsync(club => club.Email == loginRequest.Email && club.Password == loginRequest.Password);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
