using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.VehicleAmenitiesDtos;
using Project.WebAPI.Repositories.VehicleAmenityRepository;

namespace Project.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VehicleAmenityController : ControllerBase
	{
		readonly IVehicleAmenityRepository _vehicleAmenityRepository;

		public VehicleAmenityController(IVehicleAmenityRepository vehicleAmenityRepository)
		{
			_vehicleAmenityRepository = vehicleAmenityRepository;
		}

		[HttpGet("GetAllAmenities")]
		public async Task<IActionResult> GetAllAmenities()
		{
			var values = await _vehicleAmenityRepository.ListAllAmenities();
			return Ok(values);
		}

		[HttpGet("GetActiveAmenities")]
		public async Task<IActionResult> GetActiveAmenities()
		{
			var values = await _vehicleAmenityRepository.ListActiveAmenities();
			return Ok(values);
		}

		[HttpPost("InsertAmenity")]
		public async Task<IActionResult> InsertAmenity(InsertAmenityDto insertAmenityDto)
		{
			await _vehicleAmenityRepository.InsertAmenity(insertAmenityDto);
			return Ok("Amenity inserted successfully.");
		}

		[HttpPut("UpdateAmenity")]
		public async Task<IActionResult> UpdateAmenity(UpdateAmenityDto updateAmenityDto)
		{
			await _vehicleAmenityRepository.UpdateAmenity(updateAmenityDto);
			return Ok("Amenity updated successfully.");
		}

		[HttpPut("RemoveAmenity/{id}")]
		public async Task<IActionResult> RemoveAmenity(int id)
		{
			await _vehicleAmenityRepository.RemoveAmenity(id);
			return Ok("Amenity removed successfully.");	
		}

		[HttpGet("GetAmenity/{id}")]
		public async Task<IActionResult> GetAmenityById(int id)
		{
			var values = await _vehicleAmenityRepository.GetAmenityById(id);
			return Ok(values);
		}
	}
}
