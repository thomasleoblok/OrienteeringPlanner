using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrienteeringPlanner.Data;
using OrienteeringPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrienteeringPlanner.Services;

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
        public async Task<ActionResult<ClubWithExtendedData>> Login(LoginRequest loginRequest)
        {
            try
            {
                var club = await _context.Club.Where(club =>
                    club.Email == EncryptionDecryptionService.Encrypt(loginRequest.Email) &&
                    club.Password == EncryptionDecryptionService.Encrypt(loginRequest.Password)
                    ).FirstOrDefaultAsync();

                if (club != null)
                {
                    var clubExtended = await _context.ClubExtended.Where(ce => ce.ClubId == club.Id).FirstOrDefaultAsync();
                    
                    try
                    {
                        if (clubExtended.FirstTimeLogin)
                        {
                            clubExtended.FirstTimeLogin = false;
                            _context.Entry(clubExtended).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                        }
                    }
                    catch { }

                    club.Password = "";
                    club.Email = loginRequest.Email;

                    var clubWithExtendedData = new ClubWithExtendedData
                    {
                        ClubData = club,
                        ExtendedData = clubExtended
                    };

                    return clubWithExtendedData;
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

        [HttpGet("ChangeClubPassword")]
        public async Task<ActionResult<bool>> ChangeClubPassword(ChangePasswordRequest request)
        {
            try
            {
                var dbClub = await _context.Club.FindAsync(request.ClubId);

                if (dbClub.Token == request.ClubToken)
                {
                    dbClub.Password = EncryptionDecryptionService.Encrypt(request.NewPassword);
                    _context.Entry(dbClub).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    var dbClubExtended = await _context.ClubExtended.Where(ce => ce.ClubId == request.ClubId).FirstOrDefaultAsync();

                    if(dbClubExtended != null)
                    {
                        dbClubExtended.HasChangedPassword = true;
                        _context.Entry(dbClubExtended).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }

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
