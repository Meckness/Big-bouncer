using Big_bouncer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Big_bouncer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<User>()
                .HasData(
                new User { Id = new Guid("00000000-0000-0000-0000-000000000001"), Username = "User1", Password = BCrypt.Net.BCrypt.HashPassword("Password1") },
                new User { Id = new Guid("00000000-0000-0000-0000-000000000002"), Username = "User2", Password = BCrypt.Net.BCrypt.HashPassword("Password2") }
                );
        }
    }
}
