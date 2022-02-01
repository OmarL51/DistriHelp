using DistriHelp.API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Area> Areas { get; set; }

        public DbSet<Solution> Solutions { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RequestType>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<Area>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<Solution>().HasIndex(x => x.Tittle).IsUnique();
            modelBuilder.Entity<Solution>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<Status>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<Request>().HasIndex(x => x.Tittle).IsUnique();
            modelBuilder.Entity<Request>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.Description).IsUnique();
        }


    }
}
