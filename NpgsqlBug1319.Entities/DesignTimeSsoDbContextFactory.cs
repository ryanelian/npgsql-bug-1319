using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace NpgsqlBug1319.Entities
{
    public class DesignTimeSsoDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseNpgsql("Database=npgsql_bug_1319;Host=localhost;Username=postgres;Password=HelloWorld!").UseSnakeCaseNamingConvention();

            return new MyDbContext(builder.Options);
        }
    }
}
