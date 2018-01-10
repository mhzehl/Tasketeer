using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tasketeer.Models;
using Microsoft.AspNetCore.Builder;

namespace Tasketeer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TodoContext>();
                    AddSeedData(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static void AddSeedData(TodoContext context)
        {
            var user1 = new Models.User
            {
                Id = 1,
                FirstName = "Melvin",
                Lastname = "Zehl"
            };

            var user2 = new Models.User
            {
                Id = 2,
                FirstName = "Peter",
                Prefix = "Van",
                Lastname = "Mahoniehout"
            };

            context.Users.AddRange(user1, user2);

            var todo1 = new Models.Todo
            {
                Name = "Groceries",
                Description = "Buy ham and cheese at the local supermarket",
                isCompleted = false
            };

            var todo2 = new Models.Todo
            {
                Name = "Party",
                Description = "Beerpong to the max",
                isCompleted = true
            };

            context.Todos.AddRange(todo1, todo2);

            context.SaveChanges();
        }

    }
}
