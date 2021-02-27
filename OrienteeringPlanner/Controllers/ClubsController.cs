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
                var club = await _context.Club.Where(club => club.Email == loginRequest.Email && club.Password == loginRequest.Password).FirstOrDefaultAsync();

                club.Password = "";

                return club;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet("ValidClubCredidentials")]
        public async Task<ActionResult<bool>> ValidClubCredidentials(Club club)
        {
            try
            {
                var dbClub = await _context.Club.FindAsync(club.Id);

                if (dbClub.Token == club.Token)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}
