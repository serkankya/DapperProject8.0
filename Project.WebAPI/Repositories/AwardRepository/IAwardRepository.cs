using Project.Shared.DTOs.AwardDtos;

namespace Project.WebAPI.Repositories.AwardRepository
{
    public interface IAwardRepository
    {
        Task<List<ResultAwardDto>> ListAllAwards();
        Task<List<ResultAwardDto>> ListActiveAwards();
        Task InsertAward(InsertAwardDto insertAwardDto);
        Task UpdateAward(UpdateAwardDto updateAwardDto);
        Task RemoveAward(int id);
        Task<ResultAwardDto> GetAwardById(int id);
    }
}
