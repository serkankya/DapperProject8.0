using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.Reviews;
using Project.WebAPI.Repositories.ReviewRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
		readonly IReviewRepository _reviewRepository;

		public ReviewController(IReviewRepository reviewRepository)
		{
			_reviewRepository = reviewRepository;
		}

		[HttpGet("GetRelatedReviews/{id}")]
		public async Task<IActionResult> GetRelatedReviews(int id)
		{
			var values = await _reviewRepository.ListRelatedReviews(id);
			return Ok(values);
		}

		[HttpGet("GetActiveReviews")]
		public async Task<IActionResult> GetActiveReviews()
		{
			var values = await _reviewRepository.ListActiveReviews();
			return Ok(values);
		}

		[HttpGet("GetAllReviews")]
		public async Task<IActionResult> GetAllReviews()
		{
			var values = await _reviewRepository.ListAllReviews();
			return Ok(values);
		}

		[HttpPost("InsertReview")]
		public async Task<IActionResult> InsertReview(InsertReviewDto insertReviewDto)
		{
			await _reviewRepository.InsertReview(insertReviewDto);
			return Ok("Review inserted successfully.");
		}

		[HttpPut("UpdateReview")]
		public async Task<IActionResult> UpdateReview(UpdateReviewDto updateReviewDto)
		{
			await _reviewRepository.UpdateReview(updateReviewDto);
			return Ok("Review updated successfully.");
		}

		[HttpPut("RemoveReview/{id}")]
		public async Task<IActionResult> RemoveReview(int id)
		{
			await _reviewRepository.RemoveReview(id);
			return Ok("Review removed successfully.");
		}

		[HttpGet("GetReview/{id}")]
		public async Task<IActionResult> GetReviewById(int id)
		{
			var values = await _reviewRepository.GetReviewById(id);
			return Ok(values);
		}
	}
}
