using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementApp.DTOs;

namespace TaskManagementApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto);
        Task UpdateCategoryAsync(int id, CategoryDTO categoryDto);
        Task DeleteCategoryAsync(int id);
    }
}