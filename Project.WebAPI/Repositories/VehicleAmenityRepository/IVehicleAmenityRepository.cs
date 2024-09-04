using Project.Shared.DTOs.VehicleAmenitiesDtos;

namespace Project.WebAPI.Repositories.VehicleAmenityRepository
{
	public interface IVehicleAmenityRepository
	{
		Task<List<ResultAmenityDto>> ListAllAmenities();
		Task<List<ResultAmenityDto>> ListActiveAmenities();
		Task InsertAmenity(InsertAmenityDto insertAmenityDto);
		Task UpdateAmenity(UpdateAmenityDto updateAmenityDto);
		Task RemoveAmenity(int id);
		Task<ResultAmenityDto> GetAmenityById(int id);
	}
}
