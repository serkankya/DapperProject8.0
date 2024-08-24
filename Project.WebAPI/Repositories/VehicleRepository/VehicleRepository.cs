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

        public async Task<List<ResultVehicleDto>> GetSelectedCars()
        {
            string selectedCarsQuery = "SELECT v.VehicleId, m.ModelName, b.BrandName, c.CategoryName, v.LicensePlate, v.Year, v.Color, v.PricePerDay, vD.Description FROM Vehicles v INNER JOIN VehicleDetails vD ON v.VehicleId = vD.VehicleId INNER JOIN Models m ON v.ModelId = m.ModelId INNER JOIN Brands b ON b.BrandId = m.BrandId INNER JOIN Categories c ON c.CategoryId = v.CategoryId WHERE v.STATUS = 1 AND v.IsSelectedCar = 1";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultVehicleDto>(selectedCarsQuery);
                return values.ToList();
            }
        }

        public async Task<ResultVehicleDto> GetVehicleById(int id)
        {
            string getQuery = "SELECT v.VehicleId, v.CategoryId, v.ModelId, m.ModelName, b.BrandName, c.CategoryName, v.LicensePlate, v.Year, v.Color, v.PricePerDay, v.InsertedDate, v.Status, vD.EngineType, vD.Transmission, vD.FuelType, vD.Mileage, vD.NumberOfSeats, vD.Description FROM Vehicles v INNER JOIN VehicleDetails vD ON v.VehicleId = vD.VehicleId INNER JOIN Models m ON v.ModelId = m.ModelId INNER JOIN Brands b ON b.BrandId = m.BrandId INNER JOIN Categories c ON c.CategoryId = v.CategoryId WHERE v.VehicleId = @vehicleId AND v.STATUS = 1";

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

                        _logger.LogInformation("Vehicle created successfully: " + insertVehicleDto.LicensePlate);
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
            string listActivesQuery = "SELECT v.VehicleId, v.CategoryId, v.ModelId, m.ModelName, b.BrandName, c.CategoryName, v.LicensePlate, v.Year, v.Color, v.PricePerDay, v.InsertedDate, v.Status, vD.EngineType, vD.Transmission, vD.FuelType, vD.Mileage, vD.NumberOfSeats, vD.Description FROM Vehicles v INNER JOIN VehicleDetails vD ON v.VehicleId = vD.VehicleId INNER JOIN Models m ON v.ModelId = m.ModelId INNER JOIN Brands b ON b.BrandId = m.BrandId INNER JOIN Categories c ON c.CategoryId = v.CategoryId WHERE v.Status = 1";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultVehicleDto>(listActivesQuery);
                return values.ToList();
            }
        }

        public async Task<List<ResultVehicleDto>> ListAllVehicles()
        {
            string listActivesQuery = "SELECT v.VehicleId, v.CategoryId, v.ModelId, m.ModelName, b.BrandName, c.CategoryName, v.LicensePlate, v.Year, v.Color, v.PricePerDay, v.InsertedDate, v.Status, vD.EngineType, vD.Transmission, vD.FuelType, vD.Mileage, vD.NumberOfSeats, vD.Description FROM Vehicles v INNER JOIN VehicleDetails vD ON v.VehicleId = vD.VehicleId INNER JOIN Models m ON v.ModelId = m.ModelId INNER JOIN Brands b ON b.BrandId = m.BrandId INNER JOIN Categories c ON c.CategoryId = v.CategoryId";

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
            string updateVehiclesQuery = "UPDATE Vehicles SET CategoryId = @categoryId, ModelId = @modelId, LicensePlate = @licensePlate, Year = @year, Color = @color, PricePerDay = @pricePerDay, Status = @status WHERE VehicleId = @vehicleId";

            string updateVehicleDetailsQuery = "UPDATE VehicleDetails SET EngineType = @engineType, Transmission = @transmission, FuelType = @fuelType, Description = @description, NumberOfSeats = @numberOfSeats, Mileage = @mileage WHERE VehicleId = @vehicleId";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var vehicleParameters = new DynamicParameters();

                        vehicleParameters.Add("@categoryId", updateVehicleDto.CategoryId);
                        vehicleParameters.Add("@modelId", updateVehicleDto.ModelId);
                        vehicleParameters.Add("@licensePlate", updateVehicleDto.LicensePlate);
                        vehicleParameters.Add("@year", updateVehicleDto.Year);
                        vehicleParameters.Add("@color", updateVehicleDto.Color);
                        vehicleParameters.Add("@pricePerDay", updateVehicleDto.PricePerDay);
                        vehicleParameters.Add("@status", updateVehicleDto.Status);
                        vehicleParameters.Add("@vehicleId", updateVehicleDto.VehicleId);

                        await connection.ExecuteAsync(updateVehiclesQuery, vehicleParameters, transaction);

                        var vehicleDetailsParameters = new DynamicParameters();
                        vehicleDetailsParameters.Add("@engineType", updateVehicleDto.EngineType);
                        vehicleDetailsParameters.Add("@transmission", updateVehicleDto.Transmission);
                        vehicleDetailsParameters.Add("@fuelType", updateVehicleDto.FuelType);
                        vehicleDetailsParameters.Add("@description", updateVehicleDto.Description);
                        vehicleDetailsParameters.Add("@numberOfSeats", updateVehicleDto.NumberOfSeats);
                        vehicleDetailsParameters.Add("@mileage", updateVehicleDto.Mileage);
                        vehicleDetailsParameters.Add("@vehicleId", updateVehicleDto.VehicleId);

                        await connection.ExecuteAsync(updateVehicleDetailsQuery, vehicleDetailsParameters, transaction);

                        transaction.Commit();

                        _logger.LogInformation("Vehicle updated successfully: " + updateVehicleDto.VehicleId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, " An error occured while updating the vehicle: " + updateVehicleDto.VehicleId);
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
