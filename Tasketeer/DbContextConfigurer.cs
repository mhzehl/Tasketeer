using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasketeer
{
    public static class DbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder builder, string connectionString)
        {
            if (connectionString.ToLowerInvariant().Contains("Server=inMemory".ToLowerInvariant()))
            {
                builder.UseInMemoryDatabase("tasketeer");
            }
            else
            {
                builder.UseSqlServer(connectionString);
            }
        }
    }
}
