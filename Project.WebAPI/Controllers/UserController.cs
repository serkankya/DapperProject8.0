using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.UserDtos;
using Project.WebAPI.Repositories.UserRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		readonly IUserRepository _userRepository;

		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpGet("GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			var values = await _userRepository.ListAllUsers();
			return Ok(values);
		}

		[HttpGet("GetActiveUsers")]
		public async Task<IActionResult> GetActiveUsers()
		{
			var values = await _userRepository.ListActiveUsers();
			return Ok(values);
		}

		[HttpPost("InsertUser")]
		public async Task<IActionResult> InsertUser(InsertUserDto insertUserDto)
		{
			await _userRepository.InsertUser(insertUserDto);
			return Ok("User inserted successfully.");
		}

		[HttpPut("UpdateUser")]
		public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
		{
			await _userRepository.UpdateUser(updateUserDto);
			return Ok("User updated successfully.");
		}

		[HttpPut("RemoveUser/{id}")]
		public async Task<IActionResult> RemoveUser(int id)
		{
			await _userRepository.RemoveUser(id);
			return Ok("User removed successfully.");
		}

		[HttpGet("GetUser/{id}")]
		public async Task<IActionResult> GetUserById(int id)
		{
			var values = await _userRepository.GetUserById(id);
			return Ok(values);
		}


	}
}
