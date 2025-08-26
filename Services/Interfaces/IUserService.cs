using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementApp.DTOs;

namespace TaskManagementApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> CreateUserAsync(UserDTO userDto); // This was missing
        Task UpdateUserAsync(int id, UserDTO userDto);
        Task DeleteUserAsync(int id);
    }
}