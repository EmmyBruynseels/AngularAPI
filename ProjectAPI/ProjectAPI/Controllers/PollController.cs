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
	public class PollController : ControllerBase
	{
		private readonly PollContext _context;

		public PollController(PollContext context)
		{
			_context = context;
		}

		// GET: api/Poll
		/*	[Authorize]
			[HttpGet]
			public async Task<ActionResult<IEnumerable<Poll>>> GetPolls()
			{
				return await _context.Polls.Include(p => p.Antwoorden).ToListAsync();
			}*/

		[Authorize]
		[HttpGet("polls")]
		public async Task<IEnumerable<Poll2>> GetPolls(int userID)
		{
			//gebruikerID  == huidige gebruiker
			IEnumerable<Poll> Polls = await _context.Polls.Include(p => p.Antwoorden).ThenInclude(a => a.Stemmen).Include(p => p.PollGebruikers).ToListAsync();
			List<Poll2> pollModel = new List<Poll2>();

			foreach (var p in Polls)
			{
				foreach (var pg in p.PollGebruikers)
				{
					if (pg.PollID == p.PollID && pg.UserID == userID)
					{

						Poll2 poll = new Poll2();
						poll.Naam = p.Naam;
						poll.PollID = p.PollID;
						poll.Antwoorden = new List<Antwoord2>();
						foreach (var a in p.Antwoorden)
						{
							var antw = new Antwoord2();
							antw.AntwoordID = a.AntwoordID;
							antw.Naam = a.Naam;
							antw.Stemmen = new List<Stem2>();


							foreach (var s in a.Stemmen)
							{
								antw.Stemmen.Add(new Stem2() { UserID = s.UserID, AntwoordID = s.AntwoordID });

							}

							poll.Antwoorden.Add(antw);
						}
						pollModel.Add(poll);
					}
				}
			}


			return pollModel;
		}

		[Authorize]
		[HttpGet]
		public async Task<IEnumerable<Poll2>> GetPoll()
		{
			IEnumerable<Poll> Polls = await _context.Polls.Include(p => p.Antwoorden).ThenInclude(a => a.Stemmen).Include(p => p.PollGebruikers).ToListAsync();
			List<Poll2> pollModel = new List<Poll2>();

			foreach (var p in Polls)
			{
				Poll2 poll = new Poll2();
				poll.Naam = p.Naam;
				poll.PollID = p.PollID;
				poll.Antwoorden = new List<Antwoord2>();
				foreach (var a in p.Antwoorden)
				{
					var antw = new Antwoord2();
					antw.AntwoordID = a.AntwoordID;
					antw.Naam = a.Naam;
					antw.Stemmen = new List<Stem2>();

					foreach (var s in a.Stemmen)
					{
						antw.Stemmen.Add(new Stem2() { UserID = s.UserID, AntwoordID = s.AntwoordID });
					}

					poll.Antwoorden.Add(antw);
				}
				pollModel.Add(poll);
			}
			return pollModel;
		}

		// GET: api/Poll/5
		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<Poll>> GetPoll(int id)
		{
			var poll = await _context.Polls.Include(p => p.Antwoorden).FirstOrDefaultAsync(p => p.PollID == id);

			if (poll == null)
			{
				return NotFound();
			}

			return poll;
		}

		// PUT: api/Poll/5
		[Authorize]
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPoll(int id, Poll poll)
		{
			if (id != poll.PollID)
			{
				return BadRequest();
			}

			_context.Entry(poll).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PollExists(id))
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

		// POST: api/Poll
		[Authorize]
		[HttpPost]
		public async Task<ActionResult<Poll>> PostPoll(Poll poll)
		{
			_context.Polls.Add(poll);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetPoll", new { id = poll.PollID }, poll);
		}

		// DELETE: api/Poll/5
		[Authorize]
		[HttpDelete("{id}")]
		public async Task<ActionResult<Poll>> DeletePoll(int id)
		{
			var poll = await _context.Polls.FindAsync(id);
			if (poll == null)
			{
				return NotFound();
			}

			_context.Polls.Remove(poll);
			await _context.SaveChangesAsync();

			return poll;
		}

		private bool PollExists(int id)
		{
			return _context.Polls.Any(e => e.PollID == id);
		}
	}
}
