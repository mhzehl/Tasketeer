using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasketeer.Models;

namespace Tasketeer
{
    //"This class is needed to run \"dotnet ef...\" commands from command line on development. Not used anywhere else"
    public class MultiTenancyDbContextFactory : IDesignTimeDbContextFactory<TodoContext>
    {
        public TodoContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TodoContext>();

            var connectionString = "Server=.\\SQLEXPRESS; Database=tasketeer; Trusted_Connecitdono=True;";
            DbContextConfigurer.Configure(builder, connectionString);

            return new TodoContext(builder.Options);
        }
    }
}
