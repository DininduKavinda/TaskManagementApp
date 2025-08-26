using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Add this using directive
using TaskManagementApp.DTOs;
using TaskManagementApp.Services.Interfaces;

namespace TaskManagementApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public TasksController(ITaskService taskService, ICategoryService categoryService, IUserService userService)
        {
            _taskService = taskService;
            _categoryService = categoryService;
            _userService = userService;
        }

        // GET: Tasks/Create
        public async Task<IActionResult> Create() // Add async and return Task<IActionResult>
        {
            await PopulateViewData();
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTaskDTO taskDto)
        {
            if (ModelState.IsValid)
            {
                await _taskService.CreateTaskAsync(taskDto);
                return RedirectToAction(nameof(Index));
            }
            
            await PopulateViewData();
            return View(taskDto);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            
            var updateDto = new UpdateTaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                IsCompleted = task.IsCompleted,
                CategoryId = task.CategoryId
            };
            
            await PopulateViewData();
            return View(updateDto);
        }

        // Helper method to populate dropdowns
        private async Task PopulateViewData()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var users = await _userService.GetAllUsersAsync();
            
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "Username");
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return View(tasks);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateTaskDTO taskDto)
        {
            if (id != taskDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _taskService.UpdateTaskAsync(id, taskDto);
                }
                catch (System.Collections.Generic.KeyNotFoundException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            
            await PopulateViewData();
            return View(taskDto);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}