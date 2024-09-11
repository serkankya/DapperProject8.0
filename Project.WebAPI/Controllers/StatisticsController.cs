using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.WebAPI.Repositories.StatisticsRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatisticsController : ControllerBase
	{
		readonly IStatisticsRepository _statisticsRepository;

		public StatisticsController(IStatisticsRepository statisticsRepository)
		{
			_statisticsRepository = statisticsRepository;
		}

		[HttpGet("GetVehicleCount")]
		public async Task<IActionResult> GetVehicleCount()
		{
			var value = await _statisticsRepository.GetVehicleCount();
			return Ok(value);
		}

		[HttpGet("GetBlogCount")]
		public async Task<IActionResult> GetBlogCount()
		{
			var value = await _statisticsRepository.GetBlogCount();
			return Ok(value);
		}

		[HttpGet("GetReviewCount")]
		public async Task<IActionResult> GetReviewCount()
		{
			var value = await _statisticsRepository.GetReviewCount();
			return Ok(value);
		}
	}
}
