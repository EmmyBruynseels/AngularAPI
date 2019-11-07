﻿using System;
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
    public class PollGebruikerController : ControllerBase
    {
        private readonly PollContext _context;

        public PollGebruikerController(PollContext context)
        {
            _context = context;
        }

		// GET: api/PollGebruiker
		[Authorize]
		[HttpGet]
        public async Task<ActionResult<IEnumerable<PollGebruiker>>> GetPollGebruiker()
        {
            return await _context.PollGebruiker.ToListAsync();
        }

		// GET: api/PollGebruiker/5
		[Authorize]
		[HttpGet("{id}")]
        public async Task<ActionResult<PollGebruiker>> GetPollGebruiker(int id)
        {
            var pollGebruiker = await _context.PollGebruiker.FindAsync(id);

            if (pollGebruiker == null)
            {
                return NotFound();
            }

            return pollGebruiker;
        }

		// PUT: api/PollGebruiker/5
		[Authorize]
		[HttpPut("{id}")]
        public async Task<IActionResult> PutPollGebruiker(int id, PollGebruiker pollGebruiker)
        {
            if (id != pollGebruiker.PollGebruikerID)
            {
                return BadRequest();
            }

            _context.Entry(pollGebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollGebruikerExists(id))
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

		// POST: api/PollGebruiker
		[Authorize]
		[HttpPost]
        public async Task<ActionResult<PollGebruiker2>> PostPollGebruiker(PollGebruiker2 pollGebruiker)
        {
			var pg1 = new PollGebruiker() { UserID = pollGebruiker.UserID, PollID = pollGebruiker.PollID };
            _context.PollGebruiker.Add(pg1);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollGebruiker", new { id = pg1.PollGebruikerID }, pg1);
        }

		// DELETE: api/PollGebruiker/5
		[Authorize]
		[HttpDelete("{id}")]
        public async Task<ActionResult<PollGebruiker>> DeletePollGebruiker(int id)
        {
            var pollGebruiker = await _context.PollGebruiker.FindAsync(id);
            if (pollGebruiker == null)
            {
                return NotFound();
            }

            _context.PollGebruiker.Remove(pollGebruiker);
            await _context.SaveChangesAsync();

            return pollGebruiker;
        }

        private bool PollGebruikerExists(int id)
        {
            return _context.PollGebruiker.Any(e => e.PollGebruikerID == id);
        }
    }
}