using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.OurServices;
using Project.WebAPI.Repositories.OurServiceRepository;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OurServiceController : ControllerBase
    {
        readonly IOurServiceRepository _ourServiceRepository;

        public OurServiceController(IOurServiceRepository ourServiceRepository)
        {
            _ourServiceRepository = ourServiceRepository;
        }

        [HttpGet("GetActiveServices")]
        public async Task<IActionResult> GetActiveServices()
        {
            var values = await _ourServiceRepository.ListActiveServices();
            return Ok(values);
        }

        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAllServices()
        {
            var values = await _ourServiceRepository.ListAllServices();
            return Ok(values);
        }

        [HttpGet("GetService/{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var values = await _ourServiceRepository.GetServiceById(id);
            return Ok(values);
        }

        [HttpGet("GetSelectedFourServices")]
        public async Task<IActionResult> GetSelectedFourServices()
        {
            var values = await _ourServiceRepository.GetSelectedFourServices();
            return Ok(values);
        }

        [HttpPost("InsertService")]
        public async Task<IActionResult> InsertService(InsertOurServiceDto insertOurServiceDto)
        {
            await _ourServiceRepository.InsertService(insertOurServiceDto);
            return Ok("New service inserted successfully.");
        }

        [HttpPut("UpdateService")]
        public async Task<IActionResult> UpdateService(UpdateOurServiceDto updateOurServiceDto)
        {
            await _ourServiceRepository.UpdateService(updateOurServiceDto);
            return Ok("Service updated successfully.");
        }

        [HttpPut("RemoveService/{id}")]
        public async Task<IActionResult> RemoveService(int id)
        {
            await _ourServiceRepository.RemoveService(id);
            return Ok("Service removed successfully.");
        }
    }
}
