using Dapper;
using Project.Shared.DTOs.HomeContentDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.HomeContentRepository
{
	public class HomeContentRepository : IHomeContentRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

		public HomeContentRepository(DapperContext dapperContext, ILogger<HomeContentRepository> logger)
		{
			_dapperContext = dapperContext;
			_logger = logger;
		}

		public async Task<ResultHomeContentDto> GetHomeContent()
		{
			string getQuery = "SELECT * FROM HomeContent";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultHomeContentDto>(getQuery);

				if (values == null)
				{
					_logger.LogError("HomeContent not found.");
					return new ResultHomeContentDto
					{
						HomeContentId = 0,
						Title = "HomeContent not found."
					};
				}

				return values;
			}
		}

		public async Task UpdateHomeContent(UpdateHomeContentDto updateHomeContentDto)
		{
			string updateQuery = "UPDATE HomeContent SET Title = @title, Subtitle = @subtitle, VideoTitle = @videoTitle, VideoUrl = @videoUrl, BackgroundImage = @backgroundImage WHERE HomeContentId = @homeContentId";

			var parameters = new DynamicParameters();
			parameters.Add("@title", updateHomeContentDto.Title);
			parameters.Add("@subTitle", updateHomeContentDto.Subtitle);
			parameters.Add("@videoTitle", updateHomeContentDto.VideoTitle);
			parameters.Add("@videoUrl", updateHomeContentDto.VideoUrl);
			parameters.Add("@backgroundImage", updateHomeContentDto.BackgroundImage);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.QueryAsync(updateQuery, parameters);
				}
				_logger.LogInformation("Home content updated successfully : " + updateHomeContentDto.HomeContentId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while updating the home content : " + updateHomeContentDto.HomeContentId);
				throw;
			}
		}
	}
}
