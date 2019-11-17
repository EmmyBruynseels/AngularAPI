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
	public class StemController : ControllerBase
	{
		private readonly PollContext _context;

		public StemController(PollContext context)
		{
			_context = context;
		}

		// GET: api/Stem
		[Authorize]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Stem>>> GetStem()
		{
			return await _context.Stem.ToListAsync();
		}

		// GET: api/Stem/5
		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<Stem>> GetStem(int id)
		{
			var stem = await _context.Stem.FindAsync(id);

			if (stem == null)
			{
				return NotFound();
			}

			return stem;
		}

		// PUT: api/Stem/5
		[Authorize]
		[HttpPut("{id}")]
		public async Task<IActionResult> PutStem(int id, Stem stem)
		{
			if (id != stem.StemID)
			{
				return BadRequest();
			}

			_context.Entry(stem).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!StemExists(id))
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

		// POST: api/Stem		[Authorize]
		[HttpPost]
		public async Task<ActionResult<Stem_dto>> PostStem(Stem_dto stem)
		{
			var stem1 = new Stem() { AntwoordID = stem.AntwoordID, UserID = stem.UserID };

			_context.Stem.Add(stem1);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetStem", new { id = stem1.StemID }, stem1);
		}

		// DELETE: api/Stem/5
		[Authorize]
		[HttpDelete("{id}")]
		public async Task<ActionResult<Stem>> DeleteStem(int id)
		{
			var stem = await _context.Stem.FindAsync(id);
			if (stem == null)
			{
				return NotFound();
			}

			_context.Stem.Remove(stem);
			await _context.SaveChangesAsync();

			return stem;
		}

		private bool StemExists(int id)
		{
			return _context.Stem.Any(e => e.StemID == id);
		}
	}
}
