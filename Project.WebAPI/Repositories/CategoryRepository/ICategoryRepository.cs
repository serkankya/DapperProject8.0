using Project.Shared.DTOs.CategoryDtos;

namespace Project.WebAPI.Repositories.CategoryRepository
{
	public interface ICategoryRepository
	{
		Task<List<ResultCategoryDto>> ListCategories();
	}
}
