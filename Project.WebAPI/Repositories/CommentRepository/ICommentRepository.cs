using Project.Shared.DTOs.CommentDtos;

namespace Project.WebAPI.Repositories.CommentRepository
{
	public interface ICommentRepository
	{
		Task<List<ResultCommentDto>> ListAllComments();
		Task<List<ResultCommentDto>> ListCommentsByBlogId(int blogId);
		Task InsertComment(InsertCommentDto insertCommentDto);
		Task UpdateComment(UpdateCommentDto updateCommentDto);
		Task DeleteComment(int id);
		Task<ResultCommentDto> GetCommentById(int id);
	}
}
