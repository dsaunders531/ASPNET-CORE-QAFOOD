using Microsoft.EntityFrameworkCore;
using QAFood.DAL.Models;
using System.Linq;

namespace QAFood.DAL
{
    /// <summary>
    /// The Food Parcel repository
    /// </summary>
    public sealed class FoodParcelRepository : Repository<FoodParcel>, IRepository<FoodParcel>
    {
        public FoodParcelRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<FoodParcel> GetAll
        {
            get
            {
                IQueryable<FoodParcel> result = this.Context.FoodParcels
                                                    .Include(i => i.FoodItems)
                                                    .Include(t => t.TestResults).ThenInclude(q => q.TestResultItems).ThenInclude(f => f.FoodItem)
                                                    .Include(a => a.TestResults).ThenInclude(b => b.TestResultItems).ThenInclude(c => c.Category);

                // populate the linked data
                return result;
            }
        }

        public void Create(FoodParcel item)
        {
            this.Context.FoodParcels.Add(item); 
        }

        public void Delete(FoodParcel item)
        {
            this.Context.FoodParcels.Remove(item);            
        }

        public FoodParcel Get(long id)
        {
            return (from FoodParcel p in this.GetAll where p.Id == id select p).FirstOrDefault();
        }

        public void Update(FoodParcel item)
        {
            // Ignore the then includes
            this.Context.AttachRange(item.TestResults.Select(r => r.TestResultItems));
            this.Context.FoodParcels.Update(item);
        }
    }
}
