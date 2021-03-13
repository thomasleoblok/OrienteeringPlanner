using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CreateUser.Data;
using OrienteeringPlanner.Models;

namespace CreateUser.Controllers
{
    public class ClubsController : Controller
    {
        private readonly ClubContext _context;

        public ClubsController(ClubContext context)
        {
            _context = context;
        }

        // GET: Clubs
        public async Task<IActionResult> Index()
        {
            var clubs = await _context.Club.ToListAsync();

            foreach (var club in clubs)
            {
                club.Email = CreateUser.Services.EncryptionDecryptionService.Decrypt(club.Email);
                club.Password = CreateUser.Services.EncryptionDecryptionService.Decrypt(club.Password);
            }
            return View(clubs);
        }

        // GET: Clubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Club
                .FirstOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }
            club.Email = CreateUser.Services.EncryptionDecryptionService.Decrypt(club.Email);
            club.Password = CreateUser.Services.EncryptionDecryptionService.Decrypt(club.Password);
            return View(club);
        }

        // GET: Clubs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,Token")] Club club)
        {
            if (ModelState.IsValid)
            {
                club.Email = CreateUser.Services.EncryptionDecryptionService.Encrypt(club.Email);
                club.Password = CreateUser.Services.EncryptionDecryptionService.Encrypt(club.Password);
                club.Token = Guid.NewGuid();

                _context.Add(club);
                await _context.SaveChangesAsync();

                var extendedClub = new ClubExtended
                {
                    ClubId = club.Id,
                    HasChangedPassword = false,
                    FirstTimeLogin = true
                };

                _context.Add(extendedClub);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        // GET: Clubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Club.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            club.Email = CreateUser.Services.EncryptionDecryptionService.Decrypt(club.Email);
            club.Password = CreateUser.Services.EncryptionDecryptionService.Decrypt(club.Password);
            return View(club);
        }

        // POST: Clubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,Token")] Club club)
        {
            if (id != club.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    club.Email = CreateUser.Services.EncryptionDecryptionService.Encrypt(club.Email);
                    club.Password = CreateUser.Services.EncryptionDecryptionService.Encrypt(club.Password);
                    _context.Update(club);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubExists(club.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        // GET: Clubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Club
                .FirstOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }
            club.Email = CreateUser.Services.EncryptionDecryptionService.Decrypt(club.Email);
            club.Password = CreateUser.Services.EncryptionDecryptionService.Decrypt(club.Password);
            return View(club);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var club = await _context.Club.FindAsync(id);
            var clubextended = await _context.ClubExtended.Where(c => c.ClubId == id).FirstOrDefaultAsync();

            _context.Club.Remove(club);
            _context.ClubExtended.Remove(clubextended);


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubExists(int id)
        {
            return _context.Club.Any(e => e.Id == id);
        }
    }
}
