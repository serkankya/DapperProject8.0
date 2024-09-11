using Project.Shared.DTOs.UserDtos;

namespace Project.WebAPI.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<List<ResultUserDto>> ListAllUsers();
        Task<List<ResultUserDto>> ListActiveUsers();
        Task InsertUser(InsertUserDto insertUserDto);
        Task UpdateUser(UpdateUserDto updateUserDto);
        Task RemoveUser(int id);
        Task<ResultUserDto> GetUserById(int id);
    }
}
