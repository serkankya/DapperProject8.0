using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.HomeContentDtos;
using Project.WebAPI.Repositories.HomeContentRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HomeContentController : ControllerBase
	{
		readonly IHomeContentRepository _homeContentRepository;

		public HomeContentController(IHomeContentRepository homeContentRepository)
		{
			_homeContentRepository = homeContentRepository;
		}

		[HttpGet("GetHomeContent")]
		public async Task<IActionResult> GetHomeContent()
		{
			var values = await _homeContentRepository.GetHomeContent();
			return Ok(values);
		}

		[HttpPut("UpdateHomeContent")]
		public async Task<IActionResult> UpdateHomeContent(UpdateHomeContentDto updateHomeContentDto)
		{
			await _homeContentRepository.UpdateHomeContent(updateHomeContentDto);
			return Ok("Home content updated successfully.");
		}
	}
}
