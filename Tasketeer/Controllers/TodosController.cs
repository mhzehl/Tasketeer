using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasketeer.Models;

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
        public async Task<IActionResult> Get()
        {
            var todos = await _context.Todos
                .ToListAsync();

            return Ok(todos);
        }

    }
}
