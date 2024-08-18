using Project.Shared.DTOs.CategoryDtos;

namespace Project.WebAPI.Repositories.CategoryRepository
{
	public interface ICategoryRepository
	{
		Task CreateCategory(InsertCategoryDto createCategoryDto);
		Task<List<ResultCategoryDto>> ListAllCategories();
		Task<List<ResultCategoryDto>> ListActiveCategories();
		Task UpdateCategory(UpdateCategoryDto updateCategoryDto);
		Task RemoveCategory(int id);
		Task<ResultCategoryDto> GetCategoryById(int id);
	}
}
