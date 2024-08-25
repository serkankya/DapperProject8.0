using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.TestimonialDtos;
using Project.WebAPI.Repositories.TestimonialRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestimonialController : ControllerBase
	{
		readonly ITestimonialRepository _testimonialRepository;

		public TestimonialController(ITestimonialRepository testimonialRepository)
		{
			_testimonialRepository = testimonialRepository;
		}

		[HttpGet("GetAllTestimonials")]
		public async Task<IActionResult> GetAllTestimonials()
		{
			var values = await _testimonialRepository.ListAllTestimonials();
			return Ok(values);
		}

		[HttpGet("GetActiveTestimonials")]
		public async Task<IActionResult> GetActiveTestimonials()
		{
			var values = await _testimonialRepository.ListActiveTestimonials();
			return Ok(values);
		}

		[HttpGet("GetTestimonial/{id}")]
		public async Task<IActionResult> GetTestimonialById(int id)
		{
			var values = await _testimonialRepository.GetTestimonialById(id);
			return Ok(values);
		}

		[HttpPost("InsertTestimonial")]
		public async Task<IActionResult> InsertTestimonial(InsertTestimonialDto insertTestimonialDto)
		{
			await _testimonialRepository.InsertTestimonial(insertTestimonialDto);
			return Ok("Testimonial inserted successfully.");
		}

		[HttpPut("UpdateTestimonial")]
		public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
		{
			await _testimonialRepository.UpdateTestimonial(updateTestimonialDto);
			return Ok("Testimonial updated successfully.");
		}

		[HttpPut("RemoveTestimonial/{id}")]
		public async Task<IActionResult> RemoveTestimonial(int id)
		{
			await _testimonialRepository.RemoveTestimonial(id);
			return Ok("Testimonial removed successfully.");
		}
	}
}
