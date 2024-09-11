using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.CommentDtos;
using Project.WebAPI.Repositories.CommentRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		readonly ICommentRepository _commentRepository;

		public CommentController(ICommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
		}

		[HttpGet("GetAllComments")]
		public async Task<IActionResult> GetAllComments()
		{
			var values = await _commentRepository.ListAllComments();
			return Ok(values);
		}

		[HttpGet("GetCommentsByBlog/{id}")]
		public async Task<IActionResult> GetCommentsByBlogId(int id)
		{
			var values = await _commentRepository.ListCommentsByBlogId(id);
			return Ok(values);
		}

		[HttpPost("InsertComment")]
		public async Task<IActionResult> InsertComment(InsertCommentDto insertCommentDto)
		{
			await _commentRepository.InsertComment(insertCommentDto);
			return Ok("Comment inserted successfully.");
		}

		[HttpPut("UpdateComment")]
		public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
		{
			await _commentRepository.UpdateComment(updateCommentDto);
			return Ok("Comment updated successfully.");
		}

		[HttpDelete("DeleteComment/{id}")]
		public async Task<IActionResult> DeleteComment(int id)
		{
			await _commentRepository.DeleteComment(id);
			return Ok("Comment deleted successfully.");
		}

		[HttpGet("GetComment/{id}")]
		public async Task<IActionResult> GetCommentById(int id)
		{
			var values = await _commentRepository.GetCommentById(id);
			return Ok(values);
		}

		
	}
}
