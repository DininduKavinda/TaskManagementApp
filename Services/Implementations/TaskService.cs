using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManagementApp.Models;
using TaskManagementApp.DTOs;
using TaskManagementApp.Repositories.Interfaces;
using TaskManagementApp.Services.Interfaces;

namespace TaskManagementApp.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDTO>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public async Task<TaskDTO> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = await _taskRepository.GetTasksByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByCategoryIdAsync(int categoryId)
        {
            var tasks = await _taskRepository.GetTasksByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public async Task<TaskDTO> CreateTaskAsync(CreateTaskDTO taskDto)
        {
            var task = _mapper.Map<TaskItem>(taskDto);
            task.CreatedAt = System.DateTime.Now;
            task.UpdatedAt = System.DateTime.Now;
            
            await _taskRepository.CreateTaskAsync(task);
            
            var createdTask = await _taskRepository.GetTaskByIdAsync(task.Id);
            return _mapper.Map<TaskDTO>(createdTask);
        }

        public async Task UpdateTaskAsync(int id, UpdateTaskDTO taskDto)
        {
            var existingTask = await _taskRepository.GetTaskByIdAsync(id);
            
            if (existingTask == null)
            {
                throw new System.Collections.Generic.KeyNotFoundException("Task not found");
            }
            
            _mapper.Map(taskDto, existingTask);
            existingTask.UpdatedAt = System.DateTime.Now;
            
            await _taskRepository.UpdateTaskAsync(existingTask);
        }

        public async Task DeleteTaskAsync(int id)
        {
            if (!await _taskRepository.TaskExistsAsync(id))
            {
                throw new System.Collections.Generic.KeyNotFoundException("Task not found");
            }
            
            await _taskRepository.DeleteTaskAsync(id);
        }
    }
}