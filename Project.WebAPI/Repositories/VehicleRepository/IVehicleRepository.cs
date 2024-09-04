using Project.Shared.DTOs.VehicleAmenitiesDtos;
using Project.Shared.DTOs.VehicleDtos;

namespace Project.WebAPI.Repositories.VehicleRepository
{
	public interface IVehicleRepository
	{
		//Vehicles
		Task InsertVehicle(InsertVehicleDto insertVehicleDto);
		Task<List<ResultVehicleDto>> ListAllVehicles();
		Task<List<ResultVehicleDto>> ListActiveVehicles();
		Task<List<ResultVehicleImagesDto>> ListVehicleImages(int id);
		Task<List<ResultVehicleAmenitiesDto>> ListVehicleAmenities(int id);
		Task UpdateVehicle(UpdateVehicleDto updateVehicleDto);
		Task RemoveVehicle(int id);
		Task<ResultVehicleDto> GetVehicleById(int id);
		Task<List<ResultVehicleDto>> GetSelectedCars();
	}
}
