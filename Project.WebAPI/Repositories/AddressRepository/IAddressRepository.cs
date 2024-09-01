using Project.Shared.DTOs.AddressDtos;

namespace Project.WebAPI.Repositories.AddressRepository
{
    public interface IAddressRepository
    {
        Task<ResultAddressDto> GetAddress();
        Task UpdateAddress(UpdateAddressDto updateAddressDto); 
    }
}
