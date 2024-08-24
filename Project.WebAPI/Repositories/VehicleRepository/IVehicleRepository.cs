using Project.Shared.DTOs.VehicleDtos;

namespace Project.WebAPI.Repositories.VehicleRepository
{
	public interface IVehicleRepository
	{
		//Vehicles
		Task InsertVehicle(InsertVehicleDto insertVehicleDto);
		Task<List<ResultVehicleDto>> ListAllVehicles();
		Task<List<ResultVehicleDto>> ListActiveVehicles();
		Task UpdateVehicle(UpdateVehicleDto updateVehicleDto);
		Task RemoveVehicle(int id);
		Task<ResultVehicleDto> GetVehicleById(int id);
		Task<List<ResultVehicleDto>> GetSelectedCars();
	}
}
