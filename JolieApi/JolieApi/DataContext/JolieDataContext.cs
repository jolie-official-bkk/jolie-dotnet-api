using System;
using JolieApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JolieApi.DataContext
{
    public class JolieDataContext : DbContext
    {
        public JolieDataContext(DbContextOptions<JolieDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<User> users { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}

