using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.VehicleDtos;
using Project.WebAPI.Repositories.VehicleRepository;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet("GetAllVehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            var values = await _vehicleRepository.ListAllVehicles();
            return Ok(values);
        }

        [HttpGet("GetActiveVehicles")]
        public async Task<IActionResult> GetActiveVehicles()
        {
            var values = await _vehicleRepository.ListActiveVehicles();
            return Ok(values);
        }

        [HttpPost("InsertVehicle")]
        public async Task<IActionResult> InsertVehicle(InsertVehicleDto insertVehicleDto)
        {
            await _vehicleRepository.InsertVehicle(insertVehicleDto);
            return Ok("New vehicle inserted successfully.");
        }

        [HttpPut("UpdateVehicle")]
        public async Task<IActionResult> UpdateVehicle(UpdateVehicleDto updateVehicleDto)
        {
            await _vehicleRepository.UpdateVehicle(updateVehicleDto);
            return Ok("Vehicle updated successfully");
        }

        [HttpPut("RemoveVehicle/{id}")]
        public async Task<IActionResult> RemoveVehicle(int id)
        {
            await _vehicleRepository.RemoveVehicle(id);
            return Ok("Vehicle removed successfully");
        }

        [HttpGet("GetVehicle/{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var values = await _vehicleRepository.GetVehicleById(id);
            return Ok(values);
        }

        [HttpGet("GetSelectedCars")]
        public async Task<IActionResult> GetSelectedCars()
        {
            var values = await _vehicleRepository.GetSelectedCars();
            return Ok(values);
        }
    }
}
