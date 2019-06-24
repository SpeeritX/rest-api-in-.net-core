﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StaplesBackend.Data;

namespace StaplesBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<CurrentOrder> Orders { get; set; }
        public DbSet<ArchivedOrder> ArchivedOrders { get; set; }
        


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API

            modelBuilder.Entity<Client>().HasIndex(c => c.Login).IsUnique();
        }
    }
}
