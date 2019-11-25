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
using ProjectAPI.Services;

namespace ProjectAPI.Controllers
{
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		private readonly PollContext _context;
		private IUserService _userService;
		public UserController(PollContext context, IUserService userService)
        {
            _context = context;
			_userService = userService;
		}

		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody]User userParam)
		{
			var user = _userService.Authenticate(userParam.Username, userParam.Password);
			if (user == null)
				return BadRequest(new { message = "Username or password is incorrect" });
			return Ok(user);
		}


		// GET: api/User
		[HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

		[HttpGet("ByEmail")]
		public async Task<ActionResult<User>> GetUserByEmail(string email)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

			return user;
		}

		// PUT: api/User/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id,[FromBody] User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
		//	var user1 = new User() { Email = user.Email, Username = user.Username, Password =user.Password};
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        // DELETE: api/User/5s
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
