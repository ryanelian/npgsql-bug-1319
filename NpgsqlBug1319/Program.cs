using Microsoft.EntityFrameworkCore;
using NpgsqlBug1319.Entities;
using System;

namespace NpgsqlBug1319
{
    class Program
    {
        static void Main(string[] args)
        {
            // delete and regenerate NpgsqlBug1319.Entities/Migrations/DB folder using migrations-add.ps1

            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Database=npgsql_bug_1319;Host=localhost;Username=postgres;Password=HelloWorld!");
            using var db = new MyDbContext(optionsBuilder.Options);
            db.Database.Migrate();
        }
    }
}
