using Project.Shared.DTOs.OurServices;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.OurServiceRepository
{
    public interface IOurServiceRepository
    {
        Task<List<ResultOurServiceDto>> ListAllServices();
        Task<List<ResultOurServiceDto>> ListActiveServices();
        Task<ResultOurServiceDto> GetServiceById(int id);
        Task<List<ResultOurServiceDto>> GetSelectedFourServices();
        Task InsertService(InsertOurServiceDto insertOurServiceDto);
        Task UpdateService(UpdateOurServiceDto updateOurServiceDto);
        Task RemoveService(int id);
    }
}
