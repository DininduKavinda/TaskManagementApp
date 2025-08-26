using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementApp.DTOs;

namespace TaskManagementApp.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetAllTasksAsync();
        Task<TaskDTO> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId);
        Task<IEnumerable<TaskDTO>> GetTasksByCategoryIdAsync(int categoryId);
        Task<TaskDTO> CreateTaskAsync(CreateTaskDTO taskDto);
        Task UpdateTaskAsync(int id, UpdateTaskDTO taskDto);
        Task DeleteTaskAsync(int id);
    }
}