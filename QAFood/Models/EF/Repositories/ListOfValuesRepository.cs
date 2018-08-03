using QAFood.Models;
using System.Linq;

namespace QAFood.EF
{
    /// <summary>
    /// The list of values repository. Some people don't like list of values but it does help create applications which can be more easily configured without requireing code changes.
    /// </summary>
    public class ListOfValuesRepository : Repository, IRepository<LOV>
    {
        public ListOfValuesRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<LOV> GetAll
        {
            get
            {
                return this.Context.ListOfValues;
            }
        }

        public LOV Get(long id)
        {
            return (from LOV l in this.GetAll where l.Id == id select l).FirstOrDefault();
        }

        public void Create(LOV item)
        {
            this.Context.ListOfValues.Add(item);
        }

        public void Delete(LOV item)
        {
            this.Context.ListOfValues.Remove(item);
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public void Update(LOV item)
        {
            this.Context.ListOfValues.Update(item);
        }
    }
}
