using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasketeer.Models;
using Tasketeer.ViewModels;

namespace Tasketeer.Controllers
{
    [Route("/")]
    public class TodosController : Controller
    {
        private readonly TodoContext _context;

        public TodosController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var todos = await _context.Todos
                .ToListAsync();

            var todosViewModel = new TodosViewModel();
            todosViewModel.Todos = todos.Select(x => new TodoDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                isCompleted = x.isCompleted
            }).OrderBy(x => x.isCompleted).ToArray();

            return View(todosViewModel);
        }

        [HttpGet]
        [Route("/todos/add")]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddTodoViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [Route("/todos/add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newTodo = new Todo();
            newTodo.Name = model.Name;
            newTodo.Description = model.Description;

            _context.Todos.Add(newTodo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _context.Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Todos.Remove(todo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
