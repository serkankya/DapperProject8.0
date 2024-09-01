using Dapper;
using Microsoft.Data.SqlClient;
using Project.Shared.DTOs.AddressDtos;
using Project.Shared.DTOs.HomeContentDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.AddressRepository
{
    public class AddressRepository : IAddressRepository
    {
        readonly DapperContext _dapperContext;
        readonly ILogger _logger;

        public AddressRepository(DapperContext dapperContext, ILogger<AddressRepository> logger)
        {
            _dapperContext = dapperContext;
            _logger = logger;
        }

        public async Task<ResultAddressDto> GetAddress()
        {
            string getQuery = "SELECT * FROM Addresses";

            using (var connection = _dapperContext.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<ResultAddressDto>(getQuery);

                if (values == null)
                {
                    _logger.LogError("Address not found.");
                    return new ResultAddressDto
                    {
                        AddressId = 0,
                        Address = "Address not found."
                    };
                }

                return values;
            }
        }

        public async Task UpdateAddress(UpdateAddressDto updateAddressDto)
        {
            string updateQuery = "UPDATE Addresses SET Address = @address, Phone = @phone, Email = @email, LocationUrl = @locationUrl WHERE AddressId = @addressId";

            var parameters = new DynamicParameters();
            parameters.Add("@address", updateAddressDto.Address);
            parameters.Add("@phone", updateAddressDto.Phone);
            parameters.Add("@email", updateAddressDto.Email);
            parameters.Add("@locationUrl", updateAddressDto.LocationUrl);
            parameters.Add("@addressId", updateAddressDto.AddressId);

            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    await connection.ExecuteAsync(updateQuery, parameters);
                }
                _logger.LogInformation("Address updated successfully: " + updateAddressDto.AddressId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " An error occured while updating the address: " + updateAddressDto.AddressId);
                throw;
            }
        }
    }
}
