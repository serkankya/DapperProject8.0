using Dapper;
using Project.Shared.DTOs.AboutUsDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.AboutUsRepository
{
	public class AboutUsRepository : IAboutUsRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

		public AboutUsRepository(DapperContext dapperContext, ILogger<AboutUsRepository> logger)
		{
			_dapperContext = dapperContext;
			_logger = logger;
		}

		public async Task<ResultAboutUsDto> GetAboutUs()
		{
			string getQuery = "SELECT * FROM AboutUs";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultAboutUsDto>(getQuery);

				if (values == null)
				{
					_logger.LogError("About Us information not found.");
					return new ResultAboutUsDto
					{
						Description = "About Us information not found!"
					};
				}

				return values;
			}
		}

		public async Task UpdateAboutUs(UpdateAboutUsDto updateAboutUsDto)
		{
			string updateQuery = "UPDATE AboutUs SET Title = @title, Description = @description, MainImageUrl = @mainImageUrl WHERE AboutUsId = @aboutUsId";

			var parameters = new DynamicParameters();
			parameters.Add("@title", updateAboutUsDto.Title);
			parameters.Add("@description", updateAboutUsDto.Description);
			parameters.Add("@mainImageUrl", updateAboutUsDto.MainImageUrl);
			parameters.Add("@aboutUsId", updateAboutUsDto.AboutUsId);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(updateQuery, parameters);
				}
				_logger.LogInformation("About Us updated successfully: " + updateAboutUsDto.AboutUsId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while updating About Us: " + updateAboutUsDto.AboutUsId);
				throw;
			}
		}
	}
}
