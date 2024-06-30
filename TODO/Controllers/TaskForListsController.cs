using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TODO.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TODO.Controllers
{
    public class TaskForListsController : Controller
    {
        private readonly SiteContex _context;
        private readonly XmlStorageService _xmlStorageService;

        public TaskForListsController(SiteContex context)
        {
            _context = context;
            _xmlStorageService = new XmlStorageService("tasks.xml");
        }

        public async Task<IActionResult> Index(string storageMode = "Database")
        {
            List<TaskForList> taskForLists;
            List<Category> categories;

            if (storageMode == "XML")
            {
                taskForLists = _xmlStorageService.LoadTasks();
                categories = _xmlStorageService.LoadCategories();
            }
            else
            {
                taskForLists = await _context.TaskForLists
                    .Include(t => t.Category)
                    .ToListAsync();
                categories = await _context.Categories.ToListAsync();
            }

            var viewModel = new TaskListViewModel
            {
                TaskList = taskForLists,
                StorageMode = storageMode
            };

            ViewData["CategoryId"] = new SelectList(categories, "Id", "CategoryName");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskListViewModel model)
        {
            if (model.NewTask.TaskName != null && model.NewTask.CategoryId > 0)
            {
                var newTask = new TaskForList
                {
                    TaskName = model.NewTask.TaskName,
                    Deadline = model.NewTask.Deadline,
                    CategoryId = model.NewTask.CategoryId,
                    Completed = false
                };

                if (model.StorageMode == "XML")
                {
                    var tasks = _xmlStorageService.LoadTasks();
                    newTask.Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
                    tasks.Add(newTask);
                    _xmlStorageService.SaveTasks(tasks);
                }
                else
                {
                    _context.Add(newTask);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index), new { storageMode = model.StorageMode });
            }

            model.TaskList = model.StorageMode == "XML"
                ? _xmlStorageService.LoadTasks()
                : await _context.TaskForLists.Include(t => t.Category).ToListAsync();

            ViewData["CategoryId"] = new SelectList(
                model.StorageMode == "XML"
                    ? _xmlStorageService.LoadCategories()
                    : await _context.Categories.ToListAsync(), "Id", "CategoryName", model.NewTask.CategoryId);

            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsCompleted(int id, string storageMode)
        {
            if (storageMode == "XML")
            {
                var tasks = _xmlStorageService.LoadTasks();
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task != null)
                {
                    task.Completed = true;
                    _xmlStorageService.SaveTasks(tasks);
                }
            }
            else
            {
                var task = await _context.TaskForLists.FindAsync(id);
                if (task != null)
                {
                    task.Completed = true;
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index), new { storageMode });
        }
    }
}
