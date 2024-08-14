﻿using Microsoft.EntityFrameworkCore;
using SiteRONA.Models;

namespace SiteRONA.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
        
        }

        public DbSet<Category> Categories{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Category>().HasData(
               
               new Category { Id=1,Name= "Argiles",DisplayOrder=1},
               new Category { Id=2,Name= "Livres",DisplayOrder=2},
               new Category { Id=3,Name= "Crèmes",DisplayOrder=3}
               );
        }
    }
}
