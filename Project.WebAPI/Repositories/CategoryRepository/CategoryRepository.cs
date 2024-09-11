using Dapper;
using Project.Shared.DTOs.CategoryDtos;
using Project.WebAPI.Models.DapperContext;
using System.Reflection.Metadata.Ecma335;

namespace Project.WebAPI.Repositories.CategoryRepository
{
	public class CategoryRepository : ICategoryRepository
	{
		readonly DapperContext _context;
		readonly ILogger _logger;

		public CategoryRepository(DapperContext context, ILogger<CategoryRepository> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task InsertCategory(InsertCategoryDto insertCategoryDto)
		{
			string insertQuery = "INSERT INTO Categories (CategoryName) VALUES (@categoryName)";

			var parameters = new DynamicParameters();
			parameters.Add("@categoryName", insertCategoryDto.CategoryName);

			try
			{
				using (var connection = _context.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}
				_logger.LogInformation("Category created successfully: " + insertCategoryDto.CategoryName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while creating the category: " + insertCategoryDto.CategoryName);
				throw;
			}
		}

		public async Task RemoveCategory(int id)
		{
			string deleteQuery = "UPDATE Categories SET Status = 0 WHERE CategoryId = @categoryId";

			var parameters = new DynamicParameters();
			parameters.Add("@categoryId", id);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(deleteQuery, parameters);
			}
		}

		public async Task<ResultCategoryDto> GetCategoryById(int id)
		{
			string getQuery = "SELECT * FROM Categories WHERE CategoryId = @categoryId AND Status = 1";

			var parameters = new DynamicParameters();
			parameters.Add("@categoryId", id);

			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultCategoryDto>(getQuery, parameters);

				if(values == null)
				{
					_logger.LogError("Category not found or inactive. "+ id);

					return new ResultCategoryDto
					{
						CategoryId = 0,
						CategoryName = "Category not found or inactive!"
					};
				}

				return values;
			}
		}

		public async Task<List<ResultCategoryDto>> ListActiveCategories()
		{
			string listActivesQuery = "SELECT * FROM Categories WHERE Status = 1";

			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultCategoryDto>(listActivesQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultCategoryDto>> ListAllCategories()
		{
			string listQuery = "SELECT * FROM Categories";

			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultCategoryDto>(listQuery);
				return values.ToList();
			}
		}

		public async Task UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			string updateQuery = "UPDATE Categories SET CategoryName = @categoryName, Status = @status WHERE CategoryId = @categoryId";

			var parameters = new DynamicParameters();
			parameters.Add("@categoryName", updateCategoryDto.CategoryName);
			parameters.Add("@status", updateCategoryDto.Status);
			parameters.Add("@categoryId", updateCategoryDto.CategoryId);

			try
			{
				using (var connection = _context.CreateConnection())
				{
					await connection.ExecuteAsync(updateQuery, parameters);
				}
				_logger.LogInformation("Category updated successfully: " + updateCategoryDto.CategoryId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occured while updating the category: " + updateCategoryDto.CategoryId);
				throw;
			}
		}

		public async Task<List<ResultCategoryDto>> ListVehicleCount()
		{
			string listGetQuery = "SELECT c.CategoryId, c.CategoryName, COUNT(v.VehicleId) AS VehicleCount FROM Categories c LEFT JOIN Vehicles v ON c.CategoryId = v.CategoryId WHERE c.Status = 1 AND v.Status = 1 GROUP BY c.CategoryId, c.CategoryName";

			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultCategoryDto>(listGetQuery);
				return values.ToList();
			}
		}
	}
}
