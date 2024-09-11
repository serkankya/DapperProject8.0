using Dapper;
using Project.Shared.DTOs.UserDtos;
using Project.WebAPI.Models.DapperContext;

namespace Project.WebAPI.Repositories.UserRepository
{
	public class UserRepository : IUserRepository
	{
		readonly DapperContext _dapperContext;
		readonly ILogger _logger;

		public UserRepository(DapperContext dapperContext, ILogger<UserRepository> logger)
		{
			_dapperContext = dapperContext;
			_logger = logger;
		}

		public async Task<ResultUserDto> GetUserById(int id)
		{
			string getQuery = "SELECT * FROM Users WHERE UserId = @userId";

			var parameters = new DynamicParameters();
			parameters.Add("@userId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<ResultUserDto>(getQuery, parameters);

				if (values == null)
				{
					_logger.LogError("User not found or inactive: " + id);

					return new ResultUserDto
					{
						UserId = 0,
						Username = "User not found or inactive!"
					};
				}

				return values;
			}
		}

		public async Task InsertUser(InsertUserDto insertUserDto)
		{
			string insertQuery = "INSERT INTO (Username, Password, FirstName, LastName, AboutUser, Email, ImageUrl, City, District) VALUES (@username, @password, @firstName, @lastName, @aboutUser, @email, @imageUrl, @city, @district)";

			var parameters = new DynamicParameters();
			parameters.Add("@username", insertUserDto.Username);
			parameters.Add("@password", insertUserDto.Password);
			parameters.Add("@firstName", insertUserDto.FirstName);
			parameters.Add("@lastName", insertUserDto.LastName);
			parameters.Add("@aboutUser", insertUserDto.AboutUser);
			parameters.Add("@email", insertUserDto.Email);
			parameters.Add("@imageUrl", insertUserDto.ImageUrl);
			parameters.Add("@city", insertUserDto.City);
			parameters.Add("@district", insertUserDto.District);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(insertQuery, parameters);
				}

				_logger.LogInformation("User created successfully: " + insertUserDto.Username);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while creating the user: " + insertUserDto.Username);
				throw;
			}
		}

		public async Task<List<ResultUserDto>> ListActiveUsers()
		{
			string listActivesQuery = "SELECT * FROM Users WHERE Status = 1";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultUserDto>(listActivesQuery);
				return values.ToList();
			}
		}

		public async Task<List<ResultUserDto>> ListAllUsers()
		{
			string listAllQuery = "SELECT * FROM Users";

			using (var connection = _dapperContext.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultUserDto>(listAllQuery);
				return values.ToList();
			}
		}

		public async Task RemoveUser(int id)
		{
			string removeQuery = "UPDATE Users SET Status = 0 WHERE UserId = @userId";

			var parameters = new DynamicParameters();
			parameters.Add("@userId", id);

			using (var connection = _dapperContext.CreateConnection())
			{
				await connection.ExecuteAsync(removeQuery, parameters);
			}
		}

		public async Task UpdateUser(UpdateUserDto updateUserDto)
		{
			string updateQuery = "UPDATE Users SET Username = @username, Password = @password, FirstName = @firstName, LastName = @lastName, AboutUser = @aboutUser, Email = @email, ImageUrl = @imageUrl, City = @city, District = @district, Status = @status WHERE UserId = @userId";

			var parameters = new DynamicParameters();

			parameters.Add("@username", updateUserDto.Username);
			parameters.Add("@password", updateUserDto.Password);
			parameters.Add("@firstName", updateUserDto.FirstName);
			parameters.Add("@lastName", updateUserDto.LastName);
			parameters.Add("@aboutUser", updateUserDto.AboutUser);
			parameters.Add("@email", updateUserDto.Email);
			parameters.Add("@imageUrl", updateUserDto.ImageUrl);
			parameters.Add("@city", updateUserDto.City);
			parameters.Add("@district", updateUserDto.District);
			parameters.Add("@status", updateUserDto.Status);
			parameters.Add("@userId", updateUserDto.UserId);

			try
			{
				using (var connection = _dapperContext.CreateConnection())
				{
					await connection.ExecuteAsync(updateQuery, parameters);
				}

				_logger.LogInformation("User updated successfully: " + updateUserDto.UserId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, " An error occured while updating the user: " + updateUserDto.UserId);
				throw;
			}
		}
	}
}
