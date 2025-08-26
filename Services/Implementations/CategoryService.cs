using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TaskManagementApp.Models;
using TaskManagementApp.DTOs;
using TaskManagementApp.Repositories.Interfaces;
using TaskManagementApp.Services.Interfaces;

namespace TaskManagementApp.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.CreateCategoryAsync(category);
            
            var createdCategory = await _categoryRepository.GetCategoryByIdAsync(category.Id);
            return _mapper.Map<CategoryDTO>(createdCategory);
        }

        public async Task UpdateCategoryAsync(int id, CategoryDTO categoryDto)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            
            if (existingCategory == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            
            _mapper.Map(categoryDto, existingCategory);
            await _categoryRepository.UpdateCategoryAsync(existingCategory);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            if (!await _categoryRepository.CategoryExistsAsync(id))
            {
                throw new KeyNotFoundException("Category not found");
            }
            
            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}