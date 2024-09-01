using Project.Shared.DTOs.MissionVisionDtos;

namespace Project.WebAPI.Repositories.MissionVisionRepository
{
    public interface IMissionVisionRepository
    {
        Task<ResultMissionVisionDto> GetMisionAndVision();
        Task UpdateMissionAndVision(UpdateMissionVisionDto updateMissionVisionDto);
    }
}
