using Project.Shared.DTOs.BrandDtos;

namespace Project.WebAPI.Repositories.BrandRepository
{
	public interface IBrandRepository
	{
		Task InsertBrand(InsertBrandDto insertBrandDto);
		Task<List<ResultBrandDto>> ListAllBrands();
		Task<List<ResultBrandDto>> ListActiveBrands();
		Task UpdateBrand(UpdateBrandDto updateBrandDto);
		Task RemoveBrand(int id);
		Task<ResultBrandDto> GetBrandById(int id);
	}
}
