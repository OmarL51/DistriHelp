using DistriHelp.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Area> Areas{ get; set; }

        public DbSet<Solution> Solutions { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RequestType>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<Area>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<Solution>().HasIndex(x => x.Tittle).IsUnique();
            modelBuilder.Entity<Solution>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<Status>().HasIndex(x => x.Description).IsUnique();
        }


    }
}
