using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            var users = await _context.Users
                .ToListAsync();

            var todosViewModel = new TodosViewModel();
            todosViewModel.Todos = todos.Select(x => new TodoDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                isCompleted = x.isCompleted,
                User = x.User
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

            var user = await _context.Users.FirstOrDefaultAsync();

            var newTodo = new Todo();
            newTodo.Name = model.Name;
            newTodo.Description = model.Description;
            newTodo.User = user;

            _context.Todos.Add(newTodo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("complete/{id:int}")]
        public async Task<IActionResult> Complete(int id)
        {
            var todo = await _context.Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            todo.isCompleted = true;

            _context.Todos.Update(todo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("uncomplete/{id:int}")]
        public async Task<IActionResult> Uncomplete(int id)
        {
            var todo = await _context.Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            todo.isCompleted = false;

            _context.Todos.Update(todo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{id:int}")]
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
