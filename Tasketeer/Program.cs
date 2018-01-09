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
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase());
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<TodoContext>();
        }

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
                Id = 1,
                Name = "Groceries",
                Description = "Buy ham and cheese at the local supermarket",
                isCompleted = false,
                UserId = user1.Id
            };

            var todo2 = new Models.Todo
            {
                Id = 2,
                Name = "Party",
                Description = "Beerpong to the max",
                isCompleted = true,
                UserId = user2.Id
            };

            context.Todos.AddRange(todo1, todo2);

            context.SaveChanges();
        }
    }
}
