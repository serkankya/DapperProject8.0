using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.AboutUsDtos;
using Project.WebAPI.Repositories.AboutUsRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AboutUsController : ControllerBase
	{
		readonly IAboutUsRepository _aboutUsRepository;

		public AboutUsController(IAboutUsRepository aboutUsRepository)
		{
			_aboutUsRepository = aboutUsRepository;
		}

		[HttpGet("GetAboutUs")]
		public async Task<IActionResult> GetAboutUs()
		{
			var values = await _aboutUsRepository.GetAboutUs();
			return Ok(values);
		}

		[HttpPut("UpdateAboutUs")]
		public async Task<IActionResult> UpdateAboutUs(UpdateAboutUsDto updateAboutUsDto)
		{
			await _aboutUsRepository.UpdateAboutUs(updateAboutUsDto);
			return Ok("About Us updated successfully.");
		}
	}
}
