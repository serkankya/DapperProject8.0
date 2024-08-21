using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.ModelDtos;
using Project.WebAPI.Repositories.ModelRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ModelController : ControllerBase
	{
		readonly IModelRepository _modelRepository;

		public ModelController(IModelRepository modelRepository)
		{
			_modelRepository = modelRepository;
		}

		[HttpGet("GetAllModels")]
		public async Task<IActionResult> GetAllModels()
		{
			var values = await _modelRepository.ListAllModels();
			return Ok(values);
		}

		[HttpGet("GetActiveModels")]
		public async Task<IActionResult> GetActiveModels()
		{
			var values = await _modelRepository.ListActiveModels();
			return Ok(values);
		}

		[HttpPost("InsertModel")]
		public async Task<IActionResult> InsertModel(InsertModelDto insertModelDto)
		{
			await _modelRepository.InsertModel(insertModelDto);
			return Ok("New model created successfuly.");
		}

		[HttpPut("UpdateModel")]
		public async Task<IActionResult> UpdateModel(UpdateModelDto updateModelDto)
		{
			await _modelRepository.UpdateModel(updateModelDto);
			return Ok("Model updated successfully.");
		}

		[HttpPut("RemoveModel/{id}")]
		public async Task<IActionResult> RemoveModel(int id)
		{
			await _modelRepository.RemoveModel(id);
			return Ok("Model removed successfully");
		}

		[HttpGet("GetModel/{id}")]
		public async Task<IActionResult> GetModelById(int id)
		{
			var values = await _modelRepository.GetModelById(id);
			return Ok(values);
		}
	}
}
