using System;
using Microsoft.EntityFrameworkCore;
using Security.Domain.Models;

namespace Security.Domain
{
    public class SecurityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
            :base(options)
        {
        }
    }
}