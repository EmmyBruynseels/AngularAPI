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
		public async Task<IEnumerable<Poll_dto>> GetPolls(int userID)
		{
			//gebruikerID  == huidige gebruiker
			IEnumerable<Poll> Polls = await _context.Polls.Include(p => p.Antwoorden).ThenInclude(a => a.Stemmen).Include(p => p.PollGebruikers).ToListAsync();
			List<Poll_dto> pollModel = new List<Poll_dto>();

			foreach (var p in Polls)
			{
				foreach (var pg in p.PollGebruikers)
				{
					if (pg.PollID == p.PollID && pg.UserID == userID)
					{

						Poll_dto poll = new Poll_dto();
						poll.Naam = p.Naam;
						poll.PollID = p.PollID;
						poll.Antwoorden = new List<Antwoord_dto>();
						foreach (var a in p.Antwoorden)
						{
							var antw = new Antwoord_dto();
							antw.AntwoordID = a.AntwoordID;
							antw.Naam = a.Naam;
							antw.Stemmen = new List<Stem_dto>();
							foreach (var s in a.Stemmen)
							{
								antw.Stemmen.Add(new Stem_dto() { StemID = s.StemID, UserID = s.UserID, AntwoordID = s.AntwoordID });

							}

							poll.Antwoorden.Add(antw);
						}
						poll.Users = new List<PollGebruiker_dto>();
						foreach (var u in p.PollGebruikers)
						{
							var pollUser = new PollGebruiker_dto();
							pollUser.UserID = u.UserID;
							pollUser.PollID = u.PollID;
							pollUser.isAdmin = u.isAdmin;
							poll.Users.Add(pollUser);
						}

						pollModel.Add(poll);
					}
				}
			}


			return pollModel;
		}
		[Authorize]
		[HttpGet("pollsAdmin")]
		public async Task<IEnumerable<Poll_dto>> GetPollsBeheerder(int userID)
		{
			//gebruikerID  == huidige gebruiker
			IEnumerable<Poll> Polls = await _context.Polls.Include(p => p.Antwoorden).ThenInclude(a => a.Stemmen).Include(p => p.PollGebruikers).ToListAsync();
			List<Poll_dto> pollModel = new List<Poll_dto>();

			foreach (var p in Polls)
			{
				foreach (var pg in p.PollGebruikers)
				{
					if (pg.PollID == p.PollID && pg.UserID == userID)
					{
						if (pg.isAdmin == true)
						{

							Poll_dto poll = new Poll_dto();
							poll.Naam = p.Naam;
							poll.PollID = p.PollID;
							poll.Antwoorden = new List<Antwoord_dto>();
							foreach (var a in p.Antwoorden)
							{
								var antw = new Antwoord_dto();
								antw.AntwoordID = a.AntwoordID;
								antw.Naam = a.Naam;
								antw.Stemmen = new List<Stem_dto>();


								foreach (var s in a.Stemmen)
								{
									antw.Stemmen.Add(new Stem_dto() { StemID= s.StemID, UserID = s.UserID, AntwoordID = s.AntwoordID });

								}

								poll.Antwoorden.Add(antw);
							}
							poll.Users = new List<PollGebruiker_dto>();
							foreach (var u in p.PollGebruikers)
							{
								var pollUser = new PollGebruiker_dto();
								pollUser.UserID = u.UserID;
								pollUser.PollID = u.PollID;
								pollUser.isAdmin = u.isAdmin;
								poll.Users.Add(pollUser);
							}

							pollModel.Add(poll);
						}
					}
				}
			}


			return pollModel;
		}
		[Authorize]
		[HttpGet("pollsUser")]
		public async Task<IEnumerable<Poll_dto>> GetPollsUser(int userID)
		{
			//gebruikerID  == huidige gebruiker
			IEnumerable<Poll> Polls = await _context.Polls.Include(p => p.Antwoorden).ThenInclude(a => a.Stemmen).Include(p => p.PollGebruikers).ToListAsync();
			List<Poll_dto> pollModel = new List<Poll_dto>();

			foreach (var p in Polls)
			{
				foreach (var pg in p.PollGebruikers)
				{
					if (pg.PollID == p.PollID && pg.UserID == userID)
					{
						if (pg.isAdmin == false)
						{
							Poll_dto poll = new Poll_dto();
							poll.Naam = p.Naam;
							poll.PollID = p.PollID;
							poll.Antwoorden = new List<Antwoord_dto>();
							foreach (var a in p.Antwoorden)
							{
								var antw = new Antwoord_dto();
								antw.AntwoordID = a.AntwoordID;
								antw.Naam = a.Naam;
								antw.Stemmen = new List<Stem_dto>();


								foreach (var s in a.Stemmen)
								{
									antw.Stemmen.Add(new Stem_dto() { StemID = s.StemID, UserID = s.UserID, AntwoordID = s.AntwoordID });

								}

								poll.Antwoorden.Add(antw);
							}
							poll.Users = new List<PollGebruiker_dto>();
							foreach (var u in p.PollGebruikers)
							{
								var pollUser = new PollGebruiker_dto();
								pollUser.UserID = u.UserID;
								pollUser.PollID = u.PollID;
								pollUser.isAdmin = u.isAdmin;
								poll.Users.Add(pollUser);
							}

							pollModel.Add(poll);
						}
					}
				}
			}


			return pollModel;
		}

		[Authorize]
		[HttpGet]
		public async Task<IEnumerable<Poll_dto>> GetPolls()
		{
			IEnumerable<Poll> Polls = await _context.Polls.Include(p => p.Antwoorden).ThenInclude(a => a.Stemmen).Include(p => p.PollGebruikers).ToListAsync();
			List<Poll_dto> pollModel = new List<Poll_dto>();

			foreach (var p in Polls)
			{
				Poll_dto poll = new Poll_dto();
				poll.Naam = p.Naam;
				poll.PollID = p.PollID;
				poll.Antwoorden = new List<Antwoord_dto>();
				foreach (var a in p.Antwoorden)
				{
					var antw = new Antwoord_dto();
					antw.AntwoordID = a.AntwoordID;
					antw.Naam = a.Naam;
					antw.Stemmen = new List<Stem_dto>();

					foreach (var s in a.Stemmen)
					{
						antw.Stemmen.Add(new Stem_dto() { StemID = s.StemID, UserID = s.UserID, AntwoordID = s.AntwoordID });
					}

					poll.Antwoorden.Add(antw);
				}
				poll.Users = new List<PollGebruiker_dto>();
				foreach (var u in p.PollGebruikers)
				{
					var pollUser = new PollGebruiker_dto();
					pollUser.UserID = u.UserID;
					pollUser.PollID = u.PollID;
					pollUser.isAdmin = u.isAdmin;
					poll.Users.Add(pollUser);
				}
				pollModel.Add(poll);
			}
			return pollModel;
		}

		// GET: api/Poll/5
		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<Poll_dto>> GetPoll(int id)
		{
			var p = await _context.Polls.Include(po => po.Antwoorden).ThenInclude(a => a.Stemmen).Include(po => po.PollGebruikers).FirstOrDefaultAsync(po => po.PollID == id);

			if (p == null)
			{
				return NotFound();
			}

			Poll_dto poll = new Poll_dto();
			poll.Naam = p.Naam;
			poll.PollID = p.PollID;
			poll.Antwoorden = new List<Antwoord_dto>();
			foreach (var a in p.Antwoorden)
			{
				var antw = new Antwoord_dto();
				antw.AntwoordID = a.AntwoordID;
				antw.Naam = a.Naam;
				antw.Stemmen = new List<Stem_dto>();

				foreach (var s in a.Stemmen)
				{
					antw.Stemmen.Add(new Stem_dto() { StemID = s.StemID, UserID = s.UserID, AntwoordID = s.AntwoordID });
				}

				poll.Antwoorden.Add(antw);
			}
			poll.Users = new List<PollGebruiker_dto>();
			foreach (var u in p.PollGebruikers)
			{
				var pollUser = new PollGebruiker_dto();
				pollUser.UserID = u.UserID;
				pollUser.PollID = u.PollID;
				pollUser.isAdmin = u.isAdmin;
				poll.Users.Add(pollUser);
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
			var poll = await _context.Polls.Include(p => p.Antwoorden).ThenInclude(a=>a.Stemmen).Include(p => p.PollGebruikers).FirstOrDefaultAsync(p => p.PollID == id);
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
