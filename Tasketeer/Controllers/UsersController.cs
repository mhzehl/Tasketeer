using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasketeer.Models;

namespace Tasketeer.Controllers
{
    [Route("/users")]
    public class UsersController : Controller
    {
        private readonly TodoContext _context;

        public UsersController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users
                .ToListAsync();

            return Ok(users);
        }

    }
}
