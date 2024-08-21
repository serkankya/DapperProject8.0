using Dapper;
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
			string insertQuery = "INSERT INTO Vehicles (CategoryId, ModelId, LicensePlate, Year, Color, PricePerDay) VALUES (@categoryId, @modelId, @licensePlate, @year, @color, @pricePerDay)";

			var parameters = new DynamicParameters();
			parameters.Add("@categoryId", insertVehicleDto.CategoryId);
			parameters.Add("@modelId", insertVehicleDto.ModelId);
			parameters.Add("@licensePlate", insertVehicleDto.LicensePlate);
			parameters.Add("@year", insertVehicleDto.Year);
			parameters.Add("@color", insertVehicleDto.Color);
			parameters.Add("@pricePerDay", insertVehicleDto.PricePerDay);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while creating the vehicle: " + insertVehicleDto.LicensePlate);
				throw;
			}
		}

		public async Task<List<ResultVehicleDto>> ListActiveVehicles()
		{
			string listActivesQuery = "SELECT * FROM Vehicles WHERE Status = 1";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultVehicleDto>(listActivesQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultVehicleDto>> ListAllVehicles()
		{
			string listActivesQuery = "SELECT * FROM Vehicles";

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
