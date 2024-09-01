using Dapper;
using Project.Shared.DTOs.MissionVisionDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.MissionVisionRepository
{
    public class MissionVisionRepository : IMissionVisionRepository
    {
        readonly DapperContext _dapperContext;
        readonly ILogger _logger;

        public MissionVisionRepository(DapperContext dapperContext, ILogger<MissionVisionRepository> logger)
        {
            _dapperContext = dapperContext;
            _logger = logger;
        }

        public async Task<ResultMissionVisionDto> GetMisionAndVision()
        {
            string getQuery = "SELECT * FROM MissionAndVisions";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultMissionVisionDto>(getQuery); 

                if(values == null)
                {
                    _logger.LogError("Mission and Vision not found.");
                    return new ResultMissionVisionDto
                    {
                        Id = 0,
                        MissionTitle = "Mission and Vision not found."
                    };
                }

                return values;
            }
        }

        public async Task UpdateMissionAndVision(UpdateMissionVisionDto updateMissionVisionDto)
        {
            string updateQuery = "UPDATE MissionAndVisions SET MissionTitle = @missionTitle, MissionText = @missionText, VisionTitle = @visionTitle, VisionText = @visionText WHERE Id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("@missionTitle", updateMissionVisionDto.MissionTitle);
            parameters.Add("@missionText", updateMissionVisionDto.MissionText);
            parameters.Add("@visionTitle", updateMissionVisionDto.VisionTitle);
            parameters.Add("@visionText", updateMissionVisionDto.VisionText);
            parameters.Add("@id", updateMissionVisionDto.Id);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    await connection.ExecuteAsync(updateQuery, parameters); 
                }
                _logger.LogInformation("Mission and Vision updated successfully: "+updateMissionVisionDto.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " An error occured while updating the mission and vision: " + updateMissionVisionDto.Id);
                throw;
            }
        }
    }
}
