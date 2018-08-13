using Microsoft.EntityFrameworkCore;
using QAFood.DAL.Models;
using System.Linq;

namespace QAFood.DAL
{
    /// <summary>
    /// The Test Result repository
    /// </summary>
    public sealed class TestResultRepository : Repository<TestResult>, IRepository<TestResult>
    {
        public TestResultRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<TestResult> GetAll
        {
            get
            {
                return this.Context.TestResults.Include(t => t.TestResultItems).ThenInclude(c => c.Category)
                                               .Include(t => t.TestResultItems).ThenInclude(f => f.FoodItem);
            }
        }
        
        public TestResult Get(long id)
        {
            return (from TestResult t in this.GetAll where t.Id == id select t).FirstOrDefault();
        }

        public void Create(TestResult item)
        {
            this.Context.TestResults.Add(item);
        }

        public void Delete(TestResult item)
        {
            this.Context.TestResults.Remove(item);
        }

        public void Update(TestResult item)
        {
            // Ignore the .ThenInclude() items
            this.Context.AttachRange(item.TestResultItems.Select(i => i.FoodItem));
            this.Context.AttachRange(item.TestResultItems.Select(c => c.Category));

            this.Context.TestResults.Update(item);
        }
    }
}
