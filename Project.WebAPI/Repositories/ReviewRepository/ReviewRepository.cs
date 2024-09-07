using Dapper;
using Project.Shared.DTOs.Reviews;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.ReviewRepository
{
	public class ReviewRepository : IReviewRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

		public ReviewRepository(DapperContext dapperContext, ILogger<ReviewRepository> logger)
		{
			_dapperContext = dapperContext;
			_logger = logger;
		}

		public async Task<ResultReviewDto> GetReviewById(int id)
		{
			string getQuery = "SELECT * FROM Reviews WHERE ReviewId = @reviewId";

			var parameters = new DynamicParameters();
			parameters.Add("@reviewId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultReviewDto>(getQuery, parameters);

				if (values == null)
				{
					_logger.LogError("Review not found or inactive. " + id);

					return new ResultReviewDto
					{
						ReviewId = 0,
						Comment = "Review not found or inactive!"
					};
				}

				return values;
			}
		}

		public async Task InsertReview(InsertReviewDto insertReviewDto)
		{
			string insertQuery = "INSERT INTO Reviews (VehicleId, FullName, Comment, Stars, ImageUrl) VALUES (@vehicleId, @fullName, @comment, @stars, @imageUrl)";

			var parameters = new DynamicParameters();
			parameters.Add("@vehicleId", insertReviewDto.VehicleId);
			parameters.Add("@fullName", insertReviewDto.FullName);
			parameters.Add("@comment", insertReviewDto.Comment);
			parameters.Add("@stars", insertReviewDto.Stars);
			parameters.Add("@imageUrl", insertReviewDto.ImageUrl);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}
				_logger.LogInformation("Review created successfully: " + insertReviewDto.Comment);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while creating the review: " + insertReviewDto.Comment);
				throw;
			}
		}

		public async Task<List<ResultReviewDto>> ListActiveReviews()
		{
			string listActivesQuery = "SELECT * FROM Reviews WHERE Status = 1";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultReviewDto>(listActivesQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultReviewDto>> ListAllReviews()
		{
			string listActivesQuery = "SELECT * FROM Reviews";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultReviewDto>(listActivesQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultReviewDto>> ListRelatedReviews(int id)
		{
			string listRelatedsQuery = "SELECT * FROM Reviews WHERE Status = 1 AND VehicleId = @vehicleId";

			var parameters = new DynamicParameters();
			parameters.Add("@vehicleId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultReviewDto>(listRelatedsQuery, parameters);
				return values.ToList();
			}
		}

		public async Task RemoveReview(int id)
		{
			string removeQuery = "UPDATE Reviews SET Status = 0 WHERE ReviewId = @reviewId";

			var parameters = new DynamicParameters();
			parameters.Add("@reviewId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				await connection.ExecuteAsync(removeQuery, parameters);
			}
		}

		public async Task UpdateReview(UpdateReviewDto updateReviewDto)
		{
			try
			{
				string updateQuery = "UPDATE Reviews SET VehicleId = @vehicleId, FullName = @fullName, Comment = @comment, Stars = @stars, ImageUrl = @imageUrl, Status = @status WHERE ReviewId = @reviewId";

				var parameters = new DynamicParameters();
				parameters.Add("@vehicleId", updateReviewDto.VehicleId);
				parameters.Add("@fullName", updateReviewDto.FullName);
				parameters.Add("@comment", updateReviewDto.Comment);
				parameters.Add("@stars", updateReviewDto.Stars);
				parameters.Add("@imageUrl", updateReviewDto.ImageUrl);
				parameters.Add("@status", updateReviewDto.Status);
				parameters.Add("@reviewId", updateReviewDto.ReviewId);

				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(updateQuery, parameters);
				}

				_logger.LogInformation("Review updated successfully: " + updateReviewDto.ReviewId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while updating the review: " + updateReviewDto.ReviewId);
				throw;
			}
		}
	}
}
