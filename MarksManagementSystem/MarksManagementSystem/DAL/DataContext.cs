﻿using MarksManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarksManagementSystem.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //this.Database.Migrate();
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
