using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.BrandDtos;
using Project.Shared.DTOs.CategoryDtos;
using Project.WebAPI.Repositories.BrandRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandController : ControllerBase
	{
		readonly IBrandRepository _brandRepository;

		public BrandController(IBrandRepository brandRepository)
		{
			_brandRepository = brandRepository;
		}

		[HttpGet("GetAllBrands")]
		public async Task<IActionResult> GetAllBrands()
		{
			var values = await _brandRepository.ListAllBrands();
			return Ok(values);
		}

		[HttpGet("GetActiveBrands")]
		public async Task<IActionResult> GetActiveBrands()
		{
			var values = await _brandRepository.ListActiveBrands();
			return Ok(values);
		}

		[HttpPost("InsertBrand")]
		public async Task<IActionResult> InsertBrand(InsertBrandDto insertBrandDto)
		{
			await _brandRepository.InsertBrand(insertBrandDto);
			return Ok("New brand inserted successfully.");
		}

		[HttpPut("UpdateBrand")]
		public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
		{
			await _brandRepository.UpdateBrand(updateBrandDto);
			return Ok("Brand updated successfully.");
		}

		[HttpPut("RemoveBrand/{id}")]
		public async Task<IActionResult> RemoveBrand(int id)
		{
			await _brandRepository.RemoveBrand(id);
			return Ok("Brand removed successfully");
		}

		[HttpGet("GetCategory/{id}")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			var values = await _brandRepository.GetBrandById(id);
			return Ok(values);
		}
	}
}
