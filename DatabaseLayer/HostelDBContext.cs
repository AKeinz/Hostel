using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DatabaseLayer
{
    public class HostelDBContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Problem> Problems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString: "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=Hosteldb");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().HasData(new User() { 
            Firstname = "admin",
            Lastname = "admin",
            Login = "admin",
            Password = "admin",
            Role = Roles.Admin});


            modelBuilder
                .Entity<Room>()
                .Property(r => r.Status)
                .HasConversion<string>();
            modelBuilder
                .Entity<User>()
                .Property(r => r.Role)
                .HasConversion<string>();
            modelBuilder
                .Entity<Order>()
                .Property(r => r.Status)
                .HasConversion<string>();
            modelBuilder
                .Entity<Problem>()
                .Property(r => r.Status)
                .HasConversion<string>();
            //base.OnModelCreating(modelBuilder);
        }

        public HostelDBContext()
        {  
            //Database.EnsureDeleted();
            bool isNew = Database.EnsureCreated();
            if (isNew)
            {

            }
        }

        

        
    }
}
