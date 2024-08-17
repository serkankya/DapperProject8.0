using Dapper;
using Project.Shared.DTOs.CategoryDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.CategoryRepository
{
	public class CategoryRepository : ICategoryRepository
	{
		readonly DapperContext _context;

        public CategoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<ResultCategoryDto>> ListCategories()
		{
			string sqlQuery = "SELECT * FROM Categories";

			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultCategoryDto>(sqlQuery);
				return values.ToList();
			}
		}
	}
}
