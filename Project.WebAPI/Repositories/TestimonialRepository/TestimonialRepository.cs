using Dapper;
using Project.Shared.DTOs.TestimonialDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.TestimonialRepository
{
	public class TestimonialRepository : ITestimonialRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

		public TestimonialRepository(DapperContext dapperContext, ILogger<TestimonialRepository> logger)
		{
			_logger = logger;
			_dapperContext = dapperContext;
		}

		public async Task<ResultTestimonialDto> GetTestimonialById(int id)
		{
			string getQuery = "SELECT * FROM Testimonials WHERE Status = 1 AND TestimonialId = @testimonialId";

			var parameters = new DynamicParameters();
			parameters.Add("@testimonialId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultTestimonialDto>(getQuery, parameters);

				if (values == null)
				{
					_logger.LogError("Testimonial not found or inactive. " + id);
					return new ResultTestimonialDto
					{
						TestimonialId = 0,
						Description = "Testimonial not found or inactive."
					};
				}

				return values;
			}
		}

		public async Task InsertTestimonial(InsertTestimonialDto insertTestimonialDto)
		{
			string insertQuery = "INSERT INTO Testimonials (ImageUrl, FullName, Description, JobTitle, Status) VALUES (@imageUrl, @fullName, @description, @jobTitle, @status)";

			var parameters = new DynamicParameters();
			parameters.Add("@imageUrl", insertTestimonialDto.ImageUrl);
			parameters.Add("@fullName", insertTestimonialDto.FullName);
			parameters.Add("@description", insertTestimonialDto.Description);
			parameters.Add("@jobTitle", insertTestimonialDto.JobTitle);
			parameters.Add("@status", insertTestimonialDto.Status);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}
				_logger.LogInformation("Testimonial created successfully: " + insertTestimonialDto.FullName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while creating the testimonial: " + insertTestimonialDto.FullName);
				throw;
			}
		}

		public async Task<List<ResultTestimonialDto>> ListActiveTestimonials()
		{
			string listActivesQuery = "SELECT * FROM Testimonials WHERE Status = 1";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultTestimonialDto>(listActivesQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultTestimonialDto>> ListAllTestimonials()
		{
			string listQuery = "SELECT * FROM Testimonials";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultTestimonialDto>(listQuery);
				return values.ToList();
			}
		}

		public async Task RemoveTestimonial(int id)
		{
			string removeQuery = "UPDATE Testimonials SET Status = 0 WHERE TestimonialId = @testimonialId";

			var parameters = new DynamicParameters();
			parameters.Add("@testimonialId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				await connection.ExecuteAsync(removeQuery, parameters);
			}
		}

		public async Task UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
		{
			string updateQuery = "UPDATE Testimonials SET ImageUrl = @imageUrl, FullName = @fullName, Description = @description, JobTitle = @jobTitle, Status = @status WHERE TestimonialId = @testimonialId";

			var parameters = new DynamicParameters();
			parameters.Add("@imageUrl", updateTestimonialDto.ImageUrl);
			parameters.Add("@fullName", updateTestimonialDto.FullName);
			parameters.Add("@description", updateTestimonialDto.Description);
			parameters.Add("@jobTitle", updateTestimonialDto.JobTitle);
			parameters.Add("@status", updateTestimonialDto.Status);
			parameters.Add("@testimonialId", updateTestimonialDto.TestimonialId);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(updateQuery, parameters);
				}
				_logger.LogInformation("Testimonial updated successfully: " + updateTestimonialDto.TestimonialId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while updating the testimonial: " + updateTestimonialDto.TestimonialId);
				throw;
			}
		}
	}
}
