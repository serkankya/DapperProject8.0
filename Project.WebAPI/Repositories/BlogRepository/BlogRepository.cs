using Dapper;
using Project.Shared.DTOs.BlogDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.BlogRepository
{
	public class BlogRepository : IBlogRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

		public BlogRepository(DapperContext dapperContext, ILogger<BlogRepository> logger)
		{
			_dapperContext = dapperContext;
			_logger = logger;
		}

		public async Task<ResultBlogDto> GetBlogById(int id)
		{
			string getQuery = "SELECT * FROM Blogs WHERE BlogId = @blogId";

			var parameters = new DynamicParameters();
			parameters.Add("@blogId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultBlogDto>(getQuery, parameters);

				if (values == null)
				{
					_logger.LogError("Blog not found or inactive. " + id);

					return new ResultBlogDto
					{
						BlogId = 0,
						PreTitle = "Blog not found or inactive!"
					};
				}

				return values;
			}
		}

		public async Task InsertBlog(InsertBlogDto insertBlogDto)
		{
			string insertQuery = "INSERT INTO Blogs (UserId, PreTitle, PreDescription, FirstTitle, FirstDescription, SecondTitle, SecondDescription, ImageUrl) VALUES (@userId, @preTitle, @preDescription, @firstTitle, @firstDescription, @secondTitle, @secondDescription, @imageUrl)";

			var parameters = new DynamicParameters();
			parameters.Add("@userId", insertBlogDto.UserId);
			parameters.Add("@preTitle", insertBlogDto.PreTitle);
			parameters.Add("@preDescription", insertBlogDto.PreDescription);
			parameters.Add("@firstTitle", insertBlogDto.FirstTitle);
			parameters.Add("@firstDescription", insertBlogDto.FirstDescription);
			parameters.Add("@secondTitle", insertBlogDto.SecondTitle);
			parameters.Add("@secondDescription", insertBlogDto.SecondDescription);
			parameters.Add("@imageUrl", insertBlogDto.ImageUrl);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}
				_logger.LogInformation("Blog created successfully: " + insertBlogDto.PreTitle);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while creating the blog: " + insertBlogDto.PreTitle);
				throw;
			}
		}

		public async Task<List<ResultBlogDto>> ListActiveBlogs()
		{
			string listActivesQuery = "SELECT * FROM Blogs WHERE Status = 1 ORDER BY PostDate DESC";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultBlogDto>(listActivesQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultBlogDto>> ListAllBlogs()
		{
			string listQuery = "SELECT * FROM Blogs";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultBlogDto>(listQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultBlogDto>> ListRecentBlogs()
		{
			string listRecentBlogsQuery = "SELECT TOP(3) * FROM Blogs WHERE Status = 1 ORDER BY PostDate DESC";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultBlogDto>(listRecentBlogsQuery);
				return values.ToList();
			}
		}

		public async Task RemoveBlog(int id)
		{
			string removeQuery = "UPDATE Blogs SET Status = 0 WHERE BlogId = @blogId";

			var parameters = new DynamicParameters();
			parameters.Add("@blogId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				await connection.ExecuteAsync(removeQuery, parameters);
			}
		}

		public async Task UpdateBlog(UpdateBlogDto updateBlogDto)
		{
			string updateQuery = "UPDATE Blogs SET PreTitle = @preTitle, PreDescription = @preDescription, FirstTitle = @firstTitle, FirstDescription = @firstDescription, SecondTitle = @secondTitle, SecondDescription = @secondDescription, ImageUrl = @imageUrl, Status = @status WHERE BlogId = @blogId";

			var parameters = new DynamicParameters();
			parameters.Add("@preTitle", updateBlogDto.PreTitle);
			parameters.Add("@preDescription", updateBlogDto.PreDescription);
			parameters.Add("@firstTitle", updateBlogDto.FirstTitle);
			parameters.Add("@firstDescription", updateBlogDto.FirstDescription);
			parameters.Add("@secondTitle", updateBlogDto.SecondTitle);
			parameters.Add("@secondDescription", updateBlogDto.SecondDescription);
			parameters.Add("@status", updateBlogDto.Status);
			parameters.Add("@imageUrl", updateBlogDto.ImageUrl);
			parameters.Add("@blogId", updateBlogDto.BlogId);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(updateQuery, parameters);
				}
				_logger.LogInformation("Blog updated successfully: " + updateBlogDto.BlogId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while updating the blog: " + updateBlogDto.BlogId);
				throw;
			}
		}
	}
}
