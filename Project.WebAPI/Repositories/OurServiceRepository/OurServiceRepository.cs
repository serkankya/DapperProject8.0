using Dapper;
using Project.Shared.DTOs.OurServices;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.OurServiceRepository
{
    public class OurServiceRepository : IOurServiceRepository
    {
        readonly DapperContext _dapperContext;
        readonly ILogger _logger;

        public OurServiceRepository(DapperContext dapperContext, ILogger<OurServiceRepository> logger)
        {
            _dapperContext = dapperContext;
            _logger = logger;
        }

        public async Task<List<ResultOurServiceDto>> GetSelectedFourServices()
        {
            string getSelectedsQuery = "SELECT * FROM OurServices WHERE IsSelected = 1 AND Status = 1";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultOurServiceDto>(getSelectedsQuery);
                return values.ToList();
            }
        }

        public async Task<ResultOurServiceDto> GetServiceById(int id)
        {
            string getQuery = "SELECT * FROM OurServices WHERE Status = 1 AND ServiceId = @serviceId";

            var parameters = new DynamicParameters();
            parameters.Add("@serviceId", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultOurServiceDto>(getQuery, parameters);

                if (values == null)
                {
                    _logger.LogError("Service not found or inactive. " + id);

                    return new ResultOurServiceDto
                    {
                        ServiceId = 0,
                        Title = "Service not found or inactive!"
                    };
                }

                return values;
            }
        }

        public async Task InsertService(InsertOurServiceDto insertOurServiceDto)
        {
            var insertQuery = "INSERT INTO OurServices (Title, Icon, Description) VALUES (@title, @icon, @description)";

            var parameters = new DynamicParameters();
            parameters.Add("@title", insertOurServiceDto.Title);
            parameters.Add("@icon", insertOurServiceDto.Icon);
            parameters.Add("@description", insertOurServiceDto.Description);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    await connection.ExecuteAsync(insertQuery, parameters);
                }
                _logger.LogInformation("Service created successfully: " + insertOurServiceDto.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " An error occured while creating the service: " + insertOurServiceDto.Title);
                throw;
            }
        }

        public async Task<List<ResultOurServiceDto>> ListActiveServices()
        {
            string listActivesQuery = "SELECT * FROM OurServices WHERE Status = 1";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultOurServiceDto>(listActivesQuery);
                return values.ToList();
            }
        }

        public async Task<List<ResultOurServiceDto>> ListAllServices()
        {
            string listQuery = "SELECT * FROM OurServices";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultOurServiceDto>(listQuery);
                return values.ToList();
            }
        }

        public async Task RemoveService(int id)
        {
            string removeQuery = "UPDATE OurServices SET Status = 0 WHERE ServiceId = @serviceId";

            var parameters = new DynamicParameters();
            parameters.Add("@serviceId", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(removeQuery, parameters);
            }
        }

        public async Task UpdateService(UpdateOurServiceDto updateOurServiceDto)
        {
            string updateQuery = "UPDATE OurServices SET Title = @title, Icon = @icon, Description = @description, IsSelected = @isSelected, Status = @status WHERE ServiceId = @serviceId";

            var parameters = new DynamicParameters();
            parameters.Add("@title", updateOurServiceDto.Title);
            parameters.Add("@icon", updateOurServiceDto.Icon);
            parameters.Add("@description", updateOurServiceDto.Description);
            parameters.Add("@isSelected", updateOurServiceDto.IsSelected);
            parameters.Add("@status", updateOurServiceDto.Status);
            parameters.Add("@serviceId", updateOurServiceDto.ServiceId);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    await connection.ExecuteAsync(updateQuery, parameters);
                }
                _logger.LogInformation("Service updated successfully: " + updateOurServiceDto.ServiceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " An error occured while updating the service: " + updateOurServiceDto.ServiceId);
                throw;
            }

        }
    }
}
