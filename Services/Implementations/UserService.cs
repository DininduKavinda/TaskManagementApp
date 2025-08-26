using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TaskManagementApp.Models;
using TaskManagementApp.DTOs;
using TaskManagementApp.Repositories.Interfaces;
using TaskManagementApp.Services.Interfaces;

namespace TaskManagementApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDto)
        {
            // Check if email already exists
            if (await _userRepository.EmailExistsAsync(userDto.Email))
            {
                throw new System.Exception("Email already exists");
            }
            
            var user = _mapper.Map<User>(userDto);
            // Note: In a real application, you should hash the password
            // For now, we'll set a default password or require it from the DTO
            user.Password = "defaultPassword"; // You should change this
            
            await _userRepository.CreateUserAsync(user);
            
            var createdUser = await _userRepository.GetUserByIdAsync(user.Id);
            return _mapper.Map<UserDTO>(createdUser);
        }

        public async Task UpdateUserAsync(int id, UserDTO userDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            
            if (existingUser == null)
            {
                throw new System.Collections.Generic.KeyNotFoundException("User not found");
            }
            
            // Check if email is being changed to an existing email
            if (existingUser.Email != userDto.Email && await _userRepository.EmailExistsAsync(userDto.Email))
            {
                throw new System.Exception("Email already exists");
            }
            
            _mapper.Map(userDto, existingUser);
            await _userRepository.UpdateUserAsync(existingUser);
        }

        public async Task DeleteUserAsync(int id)
        {
            if (!await _userRepository.UserExistsAsync(id))
            {
                throw new System.Collections.Generic.KeyNotFoundException("User not found");
            }
            
            await _userRepository.DeleteUserAsync(id);
        }
    }
}