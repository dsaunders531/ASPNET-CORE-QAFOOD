using Microsoft.EntityFrameworkCore;
using QAFood.Models;
using System.Linq;

namespace QAFood.EF
{
    /// <summary>
    /// The Food Parcel repository
    /// </summary>
    public class FoodParcelRepository : Repository, IRepository<FoodParcel>
    {
        public FoodParcelRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<FoodParcel> GetAll
        {
            get
            {
                IQueryable<FoodParcel> result = this.Context.FoodParcels
                                                    .Include(i => i.FoodItems)
                                                    .Include(t => t.TestResults);
                                                  
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

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public void Update(FoodParcel item)
        {           
            this.Context.FoodParcels.Update(item);
        }
    }
}
