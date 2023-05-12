using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataLayer
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext()
        {

        }

        public PeopleDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-GK4F9OM\\SQLEXPRESS;Database=PeopleDb;Trusted_Connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Interest> Interests { get; set; }
    }
}
