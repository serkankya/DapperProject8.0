using Dapper;
using Project.Shared.DTOs.VehicleAmenitiesDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.VehicleAmenityRepository
{
	public class VehicleAmenityRepository : IVehicleAmenityRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

		public VehicleAmenityRepository(DapperContext dapperContext, ILogger<VehicleAmenityRepository> logger)
		{
			_dapperContext = dapperContext;
			_logger = logger;
		}

		public async Task<ResultAmenityDto> GetAmenityById(int id)
		{
			string getQuery = "SELECT * FROM Amenities WHERE AmenityId = @amenityId";

			var parameters = new DynamicParameters();
			parameters.Add("@amenityId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultAmenityDto>(getQuery, parameters);

				if (values == null)
				{
					_logger.LogError("Amenity not found or inactive. " + id);

					return new ResultAmenityDto
					{
						AmenityId = 0,
						Title = "Amenity not found or inactive!"
					};
				}

				return values;
			}
		}

		public async Task InsertAmenity(InsertAmenityDto insertAmenityDto)
		{
			string insertQuery = "INSERT INTO Amenities (Title) VALUES (@title)";

			var parameters = new DynamicParameters();
			parameters.Add("@title", insertAmenityDto.Title);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}
				_logger.LogInformation("Amenity created successfully: " + insertAmenityDto.Title);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while creating the amenity: " + insertAmenityDto.Title);
				throw;
			}
		}

		public async Task<List<ResultAmenityDto>> ListActiveAmenities()
		{
			string listActiveAmenities = "SELECT * FROM Amenities WHERE Status = 1";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultAmenityDto>(listActiveAmenities);
				return values.ToList();
			}
		}

		public async Task<List<ResultAmenityDto>> ListAllAmenities()
		{
			string listAllAmenities = "SELECT * FROM Amenities";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultAmenityDto>(listAllAmenities);
				return values.ToList();
			}
		}

		public async Task RemoveAmenity(int id)
		{
			string removeQuery = "UPDATE Amenities SET Status = 0 WHERE AmenityId = @amenityId";

			var parameters = new DynamicParameters();
			parameters.Add("@amenityId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				await connection.ExecuteAsync(removeQuery, parameters);
			}
		}

		public async Task UpdateAmenity(UpdateAmenityDto updateAmenityDto)
		{
			string updateQuery = "UPDATE Amenities SET Title = @title, Status = @status WHERE AmenityId = @amenityId";

			var parameters = new DynamicParameters();
			parameters.Add("@title", updateAmenityDto.Title);
			parameters.Add("@status", updateAmenityDto.Status);
			parameters.Add("@amenityId", updateAmenityDto.AmenityId);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(updateQuery, parameters);
				}
				_logger.LogInformation("Amenity updated successfully: " + updateAmenityDto.AmenityId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while updating the amenity: " + updateAmenityDto.AmenityId);
				throw;
			}
		}
	}
}
