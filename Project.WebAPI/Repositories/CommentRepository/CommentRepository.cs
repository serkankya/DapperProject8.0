using Dapper;
using Project.Shared.DTOs.CommentDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.CommentRepository
{
	public class CommentRepository : ICommentRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

		public CommentRepository(DapperContext dapperContext, ILogger<CommentRepository> logger)
		{
			_dapperContext = dapperContext;
			_logger = logger;
		}

		public async Task DeleteComment(int id)
		{
			string deleteQuery = "DELETE FROM Comments WHERE CommentId = @commentId";

			var parameters = new DynamicParameters();
			parameters.Add("@commentId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				await connection.ExecuteAsync(deleteQuery, parameters);
			}
		}

		public async Task<ResultCommentDto> GetCommentById(int id)
		{
			string getQuery = "SELECT * FROM Comments WHERE CommentId = @commentId";

			var parameters = new DynamicParameters();
			parameters.Add("@commentId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultCommentDto>(getQuery, parameters);

				if (values == null)
				{
					_logger.LogError("Comment not found. " + +id);

					return new ResultCommentDto
					{
						CommentId = 0,
						Comment = "Comment not found!"
					};
				}

				return values;
			}
		}

		public async Task InsertComment(InsertCommentDto insertCommentDto)
		{
			string insertQuery = "INSERT INTO Comments (BlogId, Comment, UserName, Email, ImageUrl) VALUES (@blogId, @comment, @userName, @email, @imageUrl)";

			var parameters = new DynamicParameters();
			parameters.Add("@blogId", insertCommentDto.BlogId);
			parameters.Add("@comment", insertCommentDto.Comment);
			parameters.Add("@userName", insertCommentDto.UserName);
			parameters.Add("@email", insertCommentDto.Email);
			parameters.Add("@imageUrl", insertCommentDto.ImageUrl);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}
				_logger.LogInformation("Comment created successfully: " + insertCommentDto.Comment);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while creating the comment: " + insertCommentDto.Comment);
				throw;
			}
		}

		public async Task<List<ResultCommentDto>> ListAllComments()
		{
			string listAllQuery = "SELECT * FROM Comments";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultCommentDto>(listAllQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultCommentDto>> ListCommentsByBlogId(int blogId)
		{
			string listByBlogQuery = "SELECT * FROM Comments WHERE BlogId = @blogId";

			var parameters = new DynamicParameters();
			parameters.Add("@blogId", blogId);

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultCommentDto>(listByBlogQuery, parameters);
				return values.ToList();
			}
		}

		public async Task UpdateComment(UpdateCommentDto updateCommentDto)
		{
			string updateQuery = "UPDATE Comments SET Comment = @comment, UserName = @userName, Email = @email, ImageUrl = @imageUrl WHERE CommentId = @commentId";

			var parameters = new DynamicParameters();
			parameters.Add("@comment", updateCommentDto.Comment);
			parameters.Add("@userName", updateCommentDto.UserName);
			parameters.Add("@email", updateCommentDto.Email);
			parameters.Add("@imageUrl", updateCommentDto.ImageUrl);
			parameters.Add("@commentId", updateCommentDto.CommentId);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(updateQuery, parameters);
				}
				_logger.LogInformation("Comment updated successfully: " + updateCommentDto.CommentId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while updating the comment: " + updateCommentDto.CommentId);
				throw;
			}
		}
	}
}
