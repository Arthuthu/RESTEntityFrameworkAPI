using EntityFrameworkAPITraining.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkAPITraining.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly DataContext _context;

		public UserController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<UserModel>>> GetAllUsers()
		{

			return Ok(await _context.Users.ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<UserModel>> GetUserById()
		{
			var user = await _context.Users.FindAsync();

			if(user is null)
			{
				return BadRequest("User not found");
			}

			return Ok(user);
		}

		[HttpPost]
		public async Task<ActionResult<List<UserModel>>> CreateUser(UserModel user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return Ok(await _context.Users.ToListAsync());
		}

		[HttpPut]
		public async Task<ActionResult<List<UserModel>>> UpdateUser(UserModel requestedUser)
		{
			var user = await _context.Users.FindAsync(requestedUser.Id);

			if(user is null)
			{
				return BadRequest("User not found");
			}

			user.Id = requestedUser.Id;
			user.DisplayName = requestedUser.DisplayName;

			await _context.SaveChangesAsync();

			return Ok(await _context.Users.ToListAsync());
		}

		[HttpDelete]
		public async Task<ActionResult<UserModel>> DeleteUser(int id)
		{
			var user = await _context.Users.FindAsync(id);

			if (user is null)
			{
				return BadRequest("User not found");
			}

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return Ok(await _context.Users.ToListAsync());
		}
	}
}
