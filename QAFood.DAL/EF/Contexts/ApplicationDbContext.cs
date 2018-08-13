using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QAFood.DAL.Models;

namespace QAFood.DAL
{
    /// <summary>
    /// The database for the application.
    /// </summary>
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FoodParcel> FoodParcels { get; set; }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<TestResultItem> TestResultItems { get; set; }

        public DbSet<TestItemCategory> TestItemCategories { get; set; }
    }
}
