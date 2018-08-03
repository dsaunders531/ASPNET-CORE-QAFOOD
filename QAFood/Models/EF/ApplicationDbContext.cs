using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QAFood.Models;

namespace QAFood.EF
{
    /// <summary>
    /// The database for the application.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FoodParcel> FoodParcels { get; set; }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<TestResultItem> TestResultItems { get; set; }

        public DbSet<LOV> ListOfValues { get; set; }
    }

    public interface IRepository<T>
    {
        void Save();
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        IQueryable<T> GetAll { get; }
        T Get(long id);
    }

    /// <summary>
    /// The base repository class. All repositories need  to inherit from this and implement IRepository T 
    /// </summary>
    public abstract class Repository
    {
        public ApplicationDbContext Context { get; set; } = null;

        public Repository(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}
