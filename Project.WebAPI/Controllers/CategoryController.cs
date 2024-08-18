using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.CategoryDtos;
using Project.WebAPI.Repositories.CategoryRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		readonly ICategoryRepository _categoryRepository;

		public CategoryController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		[HttpGet("GetAllCategories")]
		public async Task<IActionResult> GetAllCategories()
		{
			var values = await _categoryRepository.ListAllCategories();
			return Ok(values);
		}

		[HttpGet("GetActiveCategories")]
		public async Task<IActionResult> GetActiveCategories()
		{
			var values = await _categoryRepository.ListActiveCategories();
			return Ok(values);
		}

		[HttpPost("CreateCategory")]
		public async Task<IActionResult> CreateCategory(InsertCategoryDto createCategoryDto)
		{
			await _categoryRepository.CreateCategory(createCategoryDto);
			return Ok("New category inserted successfully.");
		}

		[HttpPut("UpdateCategory")]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			await _categoryRepository.UpdateCategory(updateCategoryDto);
			return Ok("Category updated successfully");
		}

		[HttpPut("RemoveCategory/{id}")]
		public async Task<IActionResult> RemoveCategory(int id)
		{
			await _categoryRepository.RemoveCategory(id);
			return Ok("Category removed successfully");
		}

		[HttpGet("GetCategory/{id}")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			var values = await _categoryRepository.GetCategoryById(id);
			return Ok(values);
		}

	}
}
