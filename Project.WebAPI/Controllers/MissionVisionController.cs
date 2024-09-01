using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.MissionVisionDtos;
using Project.WebAPI.Repositories.MissionVisionRepository;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionVisionController : ControllerBase
    {
        readonly IMissionVisionRepository _missionVisionRepository;

        public MissionVisionController(IMissionVisionRepository missionVisionRepository)
        {
            _missionVisionRepository = missionVisionRepository;
        }

        [HttpGet("GetMissionVision")]
        public async Task<IActionResult> GetMissionVision()
        {
            var values = await _missionVisionRepository.GetMisionAndVision();
            return Ok(values);
        }

        [HttpPut("UpdateMissionVision")]
        public async Task<IActionResult> UpdateMissionVision(UpdateMissionVisionDto updateMissionVisionDto)
        {
            await _missionVisionRepository.UpdateMissionAndVision(updateMissionVisionDto);
            return Ok("Mission and vision updated successfully.");
        }
    }
}
