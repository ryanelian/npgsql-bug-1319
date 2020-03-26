using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NpgsqlBug1319.Entities
{
    public class MyDbContext : DbContext, IDataProtectionKeyContext
    {
        public static string MigrationAssemblyName => typeof(MyDbContext).Assembly.FullName;

        public MyDbContext(DbContextOptions<MyDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<UserProfile> UserProfiles { set; get; }

        public DbSet<AppRole> AppRoles { set; get; }

        public DbSet<UserProfileAppRole> UserProfileAppRoles { set; get; }

        public DbSet<DataProtectionKey> DataProtectionKeys { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasGeneratedTsVectorColumn(Q => Q.SearchVector, "english", Q => new { Q.FullName, Q.Email, Q.Username })
                .HasIndex(Q => Q.SearchVector)
                .HasMethod("GIN"); // FTS

            modelBuilder.Entity<UserProfile>().HasIndex(Q => Q.Email).IsUnique();
            modelBuilder.Entity<UserProfile>().HasIndex(Q => Q.Username).IsUnique();

            modelBuilder.Entity<AppRole>().HasIndex(Q => Q.Name).IsUnique();
            modelBuilder.Entity<AppRole>()
                .HasGeneratedTsVectorColumn(Q => Q.SearchVector, "english", Q => new { Q.Name, Q.Description })
                .HasIndex(Q => Q.SearchVector)
                .HasMethod("GIN"); // FTS

            modelBuilder.Entity<UserProfileAppRole>()
                .HasIndex(Q => new { Q.UserProfileID, Q.AppRoleID })
                .IsUnique(); // FK

            modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            {
                UserProfileID = new Guid("00000000-0000-0000-0000-000000000001"),
                FullName = "Administrator",
                Username = "administrator",
                Email = "admin@company.com",
                CreatedAt = new DateTimeOffset(new DateTime(2020, 1, 1), new TimeSpan()),
                UpdatedAt = new DateTimeOffset(new DateTime(2020, 1, 1), new TimeSpan())
            });

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                AppRoleID = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "Administrator",
                Description = "Users in this role can add, remove, and manage other users.",
                CreatedAt = new DateTimeOffset(new DateTime(2020, 1, 1), new TimeSpan()),
                UpdatedAt = new DateTimeOffset(new DateTime(2020, 1, 1), new TimeSpan())
            });

            modelBuilder.Entity<UserProfileAppRole>().HasData(new UserProfileAppRole
            {
                // seed with negative data so won't collide with autoincrements...
                UserProfileAppRoleID = -1,
                UserProfileID = new Guid("00000000-0000-0000-0000-000000000001"),
                AppRoleID = new Guid("00000000-0000-0000-0000-000000000001"),
            });
        }
    }
}
