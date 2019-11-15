using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Models;
using ProjectAPI.Models.dto;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntwoordController : ControllerBase
    {
        private readonly PollContext _context;

        public AntwoordController(PollContext context)
        {
            _context = context;
        }

		// GET: api/Antwoord
		[Authorize]
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Antwoord>>> GetAntwoord()
        {
            return await _context.Antwoord.Include(a => a.Poll).ToListAsync();
        }

		// GET: api/Antwoord/5
		[Authorize]
		[HttpGet("{id}")]
        public async Task<ActionResult<Antwoord>> GetAntwoord(int id)
        {
            var antwoord = await _context.Antwoord.Include(a => a.Poll).SingleOrDefaultAsync(a => a.AntwoordID == id);

            if (antwoord == null)
            {
                return NotFound();
            }

            return antwoord;
        }

/*
		[Authorize]
		public async Task<ActionResult<IEnumerable<Antwoord>>> GetAntwoordenWherePollID(int pollID)
		{
			return await _context.Antwoord.Where(a => a.PollID == pollID).ToListAsync();
		}*/


		// PUT: api/Antwoord/5
		[Authorize]
		[HttpPut("{id}")]
        public async Task<IActionResult> PutAntwoord(int id, Antwoord antwoord)
        {
            if (id != antwoord.AntwoordID)
            {
                return BadRequest();
            }

            _context.Entry(antwoord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AntwoordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

		// POST: api/Antwoord
		[Authorize]
		[HttpPost]
        public async Task<ActionResult<Antwoord_dto>> PostAntwoord(Antwoord_dto antwoord)
        {
			var antwoord1 = new Antwoord() { Naam = antwoord.Naam, PollID =  antwoord.PollID};

            _context.Antwoord.Add(antwoord1);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAntwoord", new { id = antwoord1.AntwoordID }, antwoord1);
        }

		// DELETE: api/Antwoord/5
		[Authorize]
		[HttpDelete("{id}")]
        public async Task<ActionResult<Antwoord>> DeleteAntwoord(int id)
        {
            var antwoord = await _context.Antwoord.FindAsync(id);
            if (antwoord == null)
            {
                return NotFound();
            }

            _context.Antwoord.Remove(antwoord);
            await _context.SaveChangesAsync();

            return antwoord;
        }

        private bool AntwoordExists(int id)
        {
            return _context.Antwoord.Any(e => e.AntwoordID == id);
        }
    }
}
