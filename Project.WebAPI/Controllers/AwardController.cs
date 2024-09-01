using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.AwardDtos;
using Project.WebAPI.Repositories.AwardRepository;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        readonly IAwardRepository _awardRepository;

        public AwardController(IAwardRepository awardRepository)
        {
            _awardRepository = awardRepository;
        }

        [HttpGet("GetAllAwards")]
        public async Task<IActionResult> GetAllAwards()
        {
            var values = await _awardRepository.ListAllAwards();
            return Ok(values);
        }

        [HttpGet("GetActiveAwards")]
        public async Task<IActionResult> GetActiveAwards()
        {
            var values = await _awardRepository.ListActiveAwards();
            return Ok(values);
        }

        [HttpGet("GetAward/{id}")]
        public async Task<IActionResult> GetAwardById(int id)
        {
            var values = await _awardRepository.GetAwardById(id);
            return Ok(values);
        }

        [HttpPost("InsertAward")]
        public async Task<IActionResult> InsertAward(InsertAwardDto insertAwardDto)
        {
            await _awardRepository.InsertAward(insertAwardDto);
            return Ok("New award inserted successfully.");
        }

        [HttpPut("UpdateAward")]
        public async Task<IActionResult> UpdateAward(UpdateAwardDto updateAwardDto)
        {
            await _awardRepository.UpdateAward(updateAwardDto);
            return Ok("Award updated successfully.");
        }

        [HttpPut("RemoveAward/{id}")]
        public async Task<IActionResult> RemoveAward(int id)
        {
            await _awardRepository.RemoveAward(id);
            return Ok("Award removed successfully.");
        }
    }
}
