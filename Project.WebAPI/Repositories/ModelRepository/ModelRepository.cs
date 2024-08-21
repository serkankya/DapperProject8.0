using Dapper;
using Project.Shared.DTOs.ModelDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.ModelRepository
{
	public class ModelRepository : IModelRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

		public ModelRepository(DapperContext dapperContext, ILogger<ModelRepository> logger)
		{
			_dapperContext = dapperContext;
			_logger = logger;
		}

		public async Task<ResultModelDto> GetModelById(int id)
		{
			string getQuery = "SELECT * FROM Models WHERE ModelId = @modelId AND Status = 1";

			var parameters = new DynamicParameters();
			parameters.Add("@modelId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultModelDto>(getQuery, parameters);

				if (values == null)
				{
					_logger.LogError("Model not found or inactive. " + id);

					return new ResultModelDto
					{
						ModelId = 0,
						ModelName = "Model not found or inactive!"
					};
				}

				return values;
			}
		}

		public async Task InsertModel(InsertModelDto insertModelDto)
		{
			string insertQuery = "INSERT INTO Models (BrandId, ModelName) VALUES (@brandId, @modelName)";

			var parameters = new DynamicParameters();
			parameters.Add("@brandId", insertModelDto.BrandId);
			parameters.Add("@modelName", insertModelDto.ModelName);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}
				_logger.LogInformation("Model created successfully: " + insertModelDto.ModelName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while creating the model: " + insertModelDto.ModelName);
				throw;
			}
		}

		public async Task<List<ResultModelDto>> ListActiveModels()
		{
			string listActivesQuery = "SELECT * FROM Models WHERE Status = 1";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultModelDto>(listActivesQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultModelDto>> ListAllModels()
		{
			string listQuery = "SELECT * FROM Models";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultModelDto>(listQuery);
				return values.ToList();
			}
		}

		public async Task RemoveModel(int id)
		{
			string removeQuery = "UPDATE Models SET Status = 0 WHERE ModelId = @modelId";

			var parameters = new DynamicParameters();
			parameters.Add("@modelId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				await connection.ExecuteAsync(removeQuery, parameters);
			}
		}

		public async Task UpdateModel(UpdateModelDto updateModelDto)
		{
			string updateQuery = "UPDATE Models SET BrandId = @brandId, ModelName = @modelName, Status = @status WHERE ModelId = @modelId";

			var parameters = new DynamicParameters();
			parameters.Add("@brandId", updateModelDto.BrandId);
			parameters.Add("@modelName", updateModelDto.ModelName);
			parameters.Add("@status", updateModelDto.Status);
			parameters.Add("@modelId", updateModelDto.ModelId);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.QueryAsync(updateQuery, parameters);
				}
				_logger.LogInformation("Model updated successfully: " + updateModelDto.ModelName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex + " An error occured while updating the model: " + updateModelDto.ModelId);
				throw;
			}
		}
	}
}
