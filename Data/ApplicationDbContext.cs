using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Books.Models;

namespace Books.Data
{

    /*
        This class is required to create database. Do following steps to create Database:
        1. Create this class like below (And DbSet is not required at 1st run).
        2. Add all Entity Framework Core Nuget Packages that are added in this project.
        3. Add DefaultConnection to appsettings.json.
        4. Update Startup.cs with AddDbContext<...
        5. Create migration (use command in PowerShell add-migration nameOfMigration).
        6. Create Database (use command in PowerShell update-database).
    */
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }
        
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Cover> Cover { get; set; }
    }
}
