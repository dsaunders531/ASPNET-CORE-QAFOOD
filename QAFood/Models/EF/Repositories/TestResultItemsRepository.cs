using Microsoft.EntityFrameworkCore;
using QAFood.Models;
using System.Linq;

namespace QAFood.EF
{
    /// <summary>
    /// The Test result items repository
    /// </summary>
    public class TestResultItemsRepository : Repository, IRepository<TestResultItem>
    {
        public TestResultItemsRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<TestResultItem> GetAll
        {
            get
            {
                return this.Context.TestResultItems.Include(c => c.Category)
                                                   .Include(f => f.FoodItem)
                                                   .Include(t => t.TestResult);                  
            }
        }

        public TestResultItem Get(long id)
        {
            return (from TestResultItem t in this.GetAll where t.Id == id select t).FirstOrDefault();
        }

        public void Create(TestResultItem item)
        {
            this.Context.TestResultItems.Add(item);
        }

        public void Delete(TestResultItem item)
        {
            this.Context.TestResultItems.Remove(item);
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public void Update(TestResultItem item)
        {           
            this.Context.TestResultItems.Update(item);
        }
    }
}
