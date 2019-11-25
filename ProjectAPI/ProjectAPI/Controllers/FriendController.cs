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
    public class FriendController : ControllerBase
    {
        private readonly PollContext _context;

        public FriendController(PollContext context)
        {
            _context = context;
        }

		// GET: api/Friend
		[Authorize]
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> GetFriends()
        {
            return await _context.Friends.ToListAsync();
        }

		[Authorize]
		[HttpGet("allForUser")]
		public async Task<ActionResult<IEnumerable<Friend>>> GetAllFriendsforUser(int userID)
		{
			IEnumerable<Friend> Friends = await _context.Friends.Include(f => f.Sender).Include(f => f.Ontvanger).ToListAsync();
			List<Friend> friendModel = new List<Friend>();
			foreach (var f in Friends)
			{
				if (f.SenderID == userID || f.OntvangerID == userID)
				{
					friendModel.Add(f);
				}
			}
			return friendModel;
		}
		[Authorize]
		[HttpGet("friendRequests")]
		public async Task<ActionResult<IEnumerable<Friend>>> GetFriendRequests(int userID)
		{
			IEnumerable<Friend> Friends = await _context.Friends.Include(f => f.Sender).Include(f => f.Ontvanger).ToListAsync();
			List<Friend> friendModel = new List<Friend>();
			foreach (var f in Friends)
			{
				if (f.OntvangerID == userID)
				{
					if (f.Accepted == false)
					{
						friendModel.Add(f);
					}
				}
			}
			return friendModel;
		}
		[Authorize]
		[HttpGet("friends")]
		public async Task<ActionResult<IEnumerable<Friend>>> GetFriendPerUser(int userID)
		{
			IEnumerable<Friend> Friends = await _context.Friends.Include(f => f.Sender). Include(f => f.Ontvanger).ToListAsync();
			List<Friend> friendModel = new List<Friend>();
			foreach (var f in Friends)
			{
				if (f.SenderID == userID || f.OntvangerID == userID)
				{
					if (f.Accepted == true)
					{
						friendModel.Add(f);
					}
				}
			}
			return friendModel;
		}

		// GET: api/Friend/5
		[Authorize]
		[HttpGet("{id}")]
        public async Task<ActionResult<Friend>> GetFriend(int id)
        {
            var friend = await _context.Friends.FindAsync(id);

            if (friend == null)
            {
                return NotFound();
            }

            return friend;
        }

		// PUT: api/Friend/5
		[Authorize]
		[HttpPut("{id}")]
        public async Task<IActionResult> PutFriend(int id, Friend friend)
        {
            if (id != friend.FriendID)
            {
                return BadRequest();
            }

            _context.Entry(friend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(id))
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

		// POST: api/Friend
		[Authorize]
		[HttpPost]
        public async Task<ActionResult<Friend_dto>> PostFriend(Friend_dto friend)
        {
			var friend1 = new Friend() { OntvangerID = friend.OntvangerID, SenderID = friend.SenderID, Accepted = friend.Accepted };
            _context.Friends.Add(friend1);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriend", new { id = friend1.FriendID }, friend1);
        }

		// DELETE: api/Friend/5
		[Authorize]
		[HttpDelete("{id}")]
        public async Task<ActionResult<Friend>> DeleteFriend(int id)
        {
            var friend = await _context.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();

            return friend;
        }
		[Authorize]
		[HttpDelete("ByUserIDAndFriendID")]
		public async Task<ActionResult<Friend>> DeleteFriendByUserIDAndFriendID(int userID,int friendID)
		{
			//var friend = await _context.Friends.FindAsync(id);
			var friend = await _context.Friends.FirstOrDefaultAsync(f => (f.OntvangerID == userID && f.SenderID == friendID)
								|| (f.OntvangerID == friendID && f.SenderID == userID));
			if (friend == null)
			{
				return NotFound();
			}

			_context.Friends.Remove(friend);
			await _context.SaveChangesAsync();

			return friend;
		}

		private bool FriendExists(int id)
        {
            return _context.Friends.Any(e => e.FriendID == id);
        }
    }
}
