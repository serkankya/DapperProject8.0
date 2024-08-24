using Dapper;
using Microsoft.IdentityModel.Logging;
using Project.Shared.DTOs.BrandDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.BrandRepository
{
	public class BrandRepository : IBrandRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

		public BrandRepository(DapperContext dapperContext, ILogger<BrandRepository> logger)
		{
			_dapperContext = dapperContext;
			_logger = logger;
		}

		public async Task InsertBrand(InsertBrandDto insertBrandDto)
		{
			string insertQuery = "INSERT INTO Brands (BrandName) VALUES (@brandName)";

			var parameters = new DynamicParameters();
			parameters.Add("@brandName", insertBrandDto.BrandName);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}
				_logger.LogInformation("Brand created successfully. " + insertBrandDto.BrandName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while creating the brand: " + insertBrandDto.BrandName);
				throw;
			}
		}

		public async Task RemoveBrand(int id)
		{
			string removeQuery = "UPDATE Brands SET Status = 0 WHERE BrandId = @brandId";

			var parameters = new DynamicParameters();
			parameters.Add("@brandId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				await connection.ExecuteAsync(removeQuery, parameters);
			}
		}

		public async Task<List<ResultBrandDto>> ListActiveBrands()
		{
			string listActivesQuery = "SELECT * FROM Brands WHERE Status = 1";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultBrandDto>(listActivesQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultBrandDto>> ListAllBrands()
		{
			string listQuery = "SELECT * FROM Brands";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultBrandDto>(listQuery);
				return values.ToList();
			}
		}

		public async Task<ResultBrandDto> GetBrandById(int id)
		{
			string getQuery = "SELECT * FROM Brands WHERE BrandId = @brandId AND Status = 1";

			var parameters = new DynamicParameters();
			parameters.Add("@brandId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultBrandDto>(getQuery, parameters);

				if (values == null)
				{
					_logger.LogError("Brand not found or inactive. " + id);

					return new ResultBrandDto
					{
						BrandId = 0,
						BrandName = "Brand not found or inactive!"
					};
				}

				return values;
			}
		}

		public async Task UpdateBrand(UpdateBrandDto updateBrandDto)
		{
			string updateQuery = "SELECT * FROM Brands SET BrandName = @brandName, Status = @status WHERE BrandId = @brandId";

			var parameters = new DynamicParameters();
			parameters.Add("@brandName", updateBrandDto.BrandName);
			parameters.Add("@status", updateBrandDto.Status);
			parameters.Add("@brandId", updateBrandDto.BrandId);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(updateQuery, parameters);
				}
				_logger.LogInformation("Brand updated successfully: " + updateBrandDto.BrandId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while updating the brand : " + updateBrandDto.BrandId);
				throw;
			}
		}
	}
}
