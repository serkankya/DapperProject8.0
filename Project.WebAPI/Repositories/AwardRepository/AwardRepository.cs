using Dapper;
using Project.Shared.DTOs.AwardDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.AwardRepository
{
    public class AwardRepository : IAwardRepository
    {
        readonly DapperContext _dapperContext;
        readonly ILogger _logger;

        public AwardRepository(DapperContext dapperContext, ILogger<AwardRepository> logger)
        {
            _dapperContext = dapperContext;
            _logger = logger;
        }

        public async Task<ResultAwardDto> GetAwardById(int id)
        {
            string getQuery = "SELECT * FROM Awards WHERE AwardId = @awardId AND Status = 1";

            var parameters = new DynamicParameters();
            parameters.Add("@awardId", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultAwardDto>(getQuery, parameters);

                if (values == null)
                {
                    _logger.LogError("Award not found or inactive. " + id);

                    return new ResultAwardDto
                    {
                        AwardId = 0,
                        AwardName = "Award not found or inactive!"
                    };
                }

                return values;
            }
        }

        public async Task InsertAward(InsertAwardDto insertAwardDto)
        {
            string insertQuery = "INSERT INTO Awards (AwardName, AwardDescription, AwardDate, AwardOrganization, AwardPhotoUrl) VALUES (@awardName, @awardDescription, @awardDate, @awardOrganization, @awardPhotoUrl)";

            var parameters = new DynamicParameters();
            parameters.Add("@awardName", insertAwardDto.AwardName);
            parameters.Add("@awardDescription", insertAwardDto.AwardDescription);
            parameters.Add("@awardDate", insertAwardDto.AwardDate);
            parameters.Add("@awardOrganization", insertAwardDto.AwardOrganization);
            parameters.Add("@awardPhotoUrl", insertAwardDto.AwardPhotoUrl);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    await connection.ExecuteAsync(insertQuery, parameters);
                }
                _logger.LogInformation("Award created successfully: " + insertAwardDto.AwardName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " An error occured while creating the award: " + insertAwardDto.AwardName);
                throw;
            }
        }

        public async Task<List<ResultAwardDto>> ListActiveAwards()
        {
            string listActivesQuery = "SELECT * FROM Awards WHERE Status = 1";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAwardDto>(listActivesQuery);
                return values.ToList();
            }
        }

        public async Task<List<ResultAwardDto>> ListAllAwards()
        {
            string listQuery = "SELECT * FROM Awards";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAwardDto>(listQuery);
                return values.ToList();
            }
        }

        public async Task RemoveAward(int id)
        {
            string removeQuery = "UPDATE Awards SET Status = 0 WHERE AwardId = @awardId";

            var parameters = new DynamicParameters();
            parameters.Add("@awardId", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(removeQuery, parameters);
            }
        }

        public async Task UpdateAward(UpdateAwardDto updateAwardDto)
        {
            string updateQuery = "UPDATE Awards SET AwardName = @awardName, AwardDescription = @awardDescription, AwardDate = @awardDate, AwardOrganization = @awardOrganization, AwardPhotoUrl = @awardPhotoUrl, Status = @status WHERE AwardId = @awardId";

            var parameters = new DynamicParameters();
            parameters.Add("@awardName", updateAwardDto.AwardName);
            parameters.Add("@awardDescription", updateAwardDto.AwardDescription);
            parameters.Add("@awardDate", updateAwardDto.AwardDate);
            parameters.Add("@awardOrganization", updateAwardDto.AwardOrganization);
            parameters.Add("@awardPhotoUrl", updateAwardDto.AwardPhotoUrl);
            parameters.Add("@status", updateAwardDto.Status);
            parameters.Add("@awardId", updateAwardDto.AwardId);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    await connection.ExecuteAsync(updateQuery, parameters);
                }
                _logger.LogInformation("Award updated successfully: " + updateAwardDto.AwardId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " An error occured while updating the award: " + updateAwardDto.AwardId);
                throw;
            }
        }
    }
}
