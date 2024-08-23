using Dapper;
using Microsoft.Data.SqlClient;
using Project.Shared.DTOs.CategoryDtos;
using Project.Shared.DTOs.VehicleDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.VehicleRepository
{
    public class VehicleRepository : IVehicleRepository
    {
        readonly DapperContext _dapperContext;
        readonly ILogger _logger;

        public VehicleRepository(DapperContext dapperContext, ILogger<VehicleRepository> logger)
        {
            _dapperContext = dapperContext;
            _logger = logger;
        }

        public async Task<ResultVehicleDto> GetVehicleById(int id)
        {
            string getQuery = "SELECT * FROM Vehicles WHERE VehicleId = @vehicleId AND STATUS = 1";

            var parameters = new DynamicParameters();
            parameters.Add("@vehicleId", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultVehicleDto>(getQuery, parameters);

                if (values == null)
                {
                    _logger.LogError("Vehicle not found or inactive. " + id);
                    return new ResultVehicleDto
                    {
                        VehicleId = 0,
                        LicensePlate = "Vehicle not found or inactive!"
                    };
                }

                return values;
            }
        }

        public async Task InsertVehicle(InsertVehicleDto insertVehicleDto)
        {
            string insertVehiclesQuery = "INSERT INTO Vehicles (CategoryId, ModelId, LicensePlate, Year, Color, PricePerDay) VALUES (@categoryId, @modelId, @licensePlate, @year, @color, @pricePerDay); SELECT SCOPE_IDENTITY();";

            string insertVehicleDetailsQuery = "INSERT INTO VehicleDetails (VehicleId, EngineType, Transmission, FuelType, Mileage, NumberOfSeats, Description) VALUES (@vehicleId, @engineType, @transmission, @fuelType, @mileage, @numberOfSeats, @description)";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var vehicleParameters = new DynamicParameters();
                        vehicleParameters.Add("@categoryId", insertVehicleDto.CategoryId);
                        vehicleParameters.Add("@modelId", insertVehicleDto.ModelId);
                        vehicleParameters.Add("@licensePlate", insertVehicleDto.LicensePlate);
                        vehicleParameters.Add("@year", insertVehicleDto.Year);
                        vehicleParameters.Add("@color", insertVehicleDto.Color);
                        vehicleParameters.Add("@pricePerDay", insertVehicleDto.PricePerDay);

                        int insertedVehicleId = await connection.ExecuteScalarAsync<int>(insertVehiclesQuery, vehicleParameters, transaction);

                        var vehicleDetailParameters = new DynamicParameters();
                        vehicleDetailParameters.Add("@vehicleId", insertedVehicleId);
                        vehicleDetailParameters.Add("@engineType", insertVehicleDto.EngineType);
                        vehicleDetailParameters.Add("@transmission", insertVehicleDto.Transmission);
                        vehicleDetailParameters.Add("@fuelType", insertVehicleDto.FuelType);
                        vehicleDetailParameters.Add("@mileage", insertVehicleDto.MileAge);
                        vehicleDetailParameters.Add("@numberOfSeats", insertVehicleDto.NumberOfSeats);
                        vehicleDetailParameters.Add("@description", insertVehicleDto.Description);

                        await connection.ExecuteAsync(insertVehicleDetailsQuery, vehicleDetailParameters, transaction);

                        transaction.Commit();
                        
                        _logger.LogInformation("Vehicle created successfully: "+insertVehicleDto.LicensePlate);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, " An error occured while creating the vehicle: " + insertVehicleDto.LicensePlate);
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<List<ResultVehicleDto>> ListActiveVehicles()
        {
            string listActivesQuery = "SELECT * FROM Vehicles v LEFT JOIN VehicleDetails vD ON v.VehicleId = vD.VehicleId WHERE Status = 1";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultVehicleDto>(listActivesQuery);
                return values.ToList();
            }
        }

        public async Task<List<ResultVehicleDto>> ListAllVehicles()
        {
            string listActivesQuery = "SELECT * FROM Vehicles v LEFT JOIN VehicleDetails vD ON v.VehicleId = vD.VehicleId";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultVehicleDto>(listActivesQuery);
                return values.ToList();
            }
        }

        public async Task RemoveVehicle(int id)
        {
            string removeQuery = "UPDATE Vehicles SET Status = 0 WHERE VehicleId = @vehicleId";

            var parameters = new DynamicParameters();
            parameters.Add("@vehicleId", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(removeQuery, parameters);
            }
        }

        public async Task UpdateVehicle(UpdateVehicleDto updateVehicleDto)
        {
            string updateQuery = "UPDATE Vehicles SET CategoryId = @categoryId, ModelId = @modelId, LicensePlate = @licensePlate, Year = @year, Color = @color, PricePerDay = @pricePerDay, Status = @status WHERE VehicleId = @vehicleId";

            var parameters = new DynamicParameters();

            parameters.Add("@categoryId", updateVehicleDto.CategoryId);
            parameters.Add("@modelId", updateVehicleDto.ModelId);
            parameters.Add("@licensePlate", updateVehicleDto.LicensePlate);
            parameters.Add("@year", updateVehicleDto.Year);
            parameters.Add("@color", updateVehicleDto.Color);
            parameters.Add("@pricePerDay", updateVehicleDto.PricePerDay);
            parameters.Add("@status", updateVehicleDto.Status);
            parameters.Add("@vehicleId", updateVehicleDto.VehicleId);

            //to be continued

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    await connection.ExecuteAsync(updateQuery, parameters);
                }
                _logger.LogInformation("Vehicle updated successfully: " + updateVehicleDto.VehicleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " An error occured while updating the vehicle: " + updateVehicleDto.VehicleId);
                throw;
            }
        }
    }
}
