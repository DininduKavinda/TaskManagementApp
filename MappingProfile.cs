using AutoMapper;
using TaskManagementApp.Models;
using TaskManagementApp.DTOs;

namespace TaskManagementApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Task mappings
            CreateMap<TaskItem, TaskDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username));
                
            CreateMap<CreateTaskDTO, TaskItem>();
            CreateMap<UpdateTaskDTO, TaskItem>();
            
            // Category mappings
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            
            // User mappings
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}