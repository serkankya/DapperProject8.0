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

		public async Task CreateCategory(InsertCategoryDto createCategoryDto)
		{
			string insertQuery = "INSERT INTO Categories (CategoryName) VALUES (@categoryName)";
			var parameters = new DynamicParameters();
			parameters.Add("@categoryName", createCategoryDto.CategoryName);

			try
			{
				using (var connection = _context.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}
				_logger.LogInformation("Category created successfully: " + createCategoryDto.CategoryName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while creating the category: " + createCategoryDto.CategoryName);
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
				return values;
			}
		}

		public async Task<List<ResultCategoryDto>> ListActiveCategories()
		{
			string listQuery = "SELECT * FROM Categories WHERE Status = 1";
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultCategoryDto>(listQuery);
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
	}
}
