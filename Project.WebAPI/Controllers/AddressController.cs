using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Shared.DTOs.AddressDtos;
using Project.WebAPI.Repositories.AddressRepository;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet("GetAddress")]
        public async Task<IActionResult> GetAddress()
        {
            var values = await _addressRepository.GetAddress();
            return Ok(values);
        }

        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(UpdateAddressDto updateAddressDto)
        {
            await _addressRepository.UpdateAddress(updateAddressDto);
            return Ok("Address updated successfully.");
        }
    }
}
