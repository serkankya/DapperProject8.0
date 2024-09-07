using Project.Shared.DTOs.CategoryDtos;

namespace Project.WebAPI.Repositories.CategoryRepository
{
	public interface ICategoryRepository
	{
		Task InsertCategory(InsertCategoryDto createCategoryDto);
		Task<List<ResultCategoryDto>> ListAllCategories();
		Task<List<ResultCategoryDto>> ListActiveCategories();
		Task<List<ResultCategoryDto>> ListVehicleCount();
		Task UpdateCategory(UpdateCategoryDto updateCategoryDto);
		Task RemoveCategory(int id);
		Task<ResultCategoryDto> GetCategoryById(int id);
	}
}
