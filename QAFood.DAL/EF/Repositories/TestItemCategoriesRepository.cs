using QAFood.DAL.Models;
using System.Linq;

namespace QAFood.DAL
{
    /// <summary>
    /// The list of values repository. Some people don't like list of values but it does help create applications which can be more easily configured without requireing code changes.
    /// </summary>
    public sealed class TestItemCategoriesRepository : Repository<TestItemCategory>, IRepository<TestItemCategory>
    {
        public TestItemCategoriesRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<TestItemCategory> GetAll
        {
            get
            {
                return this.Context.TestItemCategories;
            }
        }

        public TestItemCategory Get(long id)
        {
            return (from TestItemCategory l in this.GetAll where l.Id == id select l).FirstOrDefault();
        }

        public void Create(TestItemCategory item)
        {
            this.Context.TestItemCategories.Add(item);
        }

        public void Delete(TestItemCategory item)
        {
            this.Context.TestItemCategories.Remove(item);
        }

        public void Update(TestItemCategory item)
        {
            this.Context.TestItemCategories.Update(item);
        }
    }
}
