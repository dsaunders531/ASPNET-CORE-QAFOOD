using QAFood.Models;
using System.Linq;

namespace QAFood.EF
{
    /// <summary>
    /// The Food item repository
    /// </summary>
    public class FoodItemRespository : Repository, IRepository<FoodItem>
    {
        public FoodItemRespository(ApplicationDbContext context) : base(context) { }

        public IQueryable<FoodItem> GetAll
        {
            get
            {
                return this.Context.FoodItems;
            }
        }

        public FoodItem Get(long id)
        {
            return (from FoodItem f in this.GetAll where f.Id == id select f).FirstOrDefault();
        }

        public void Create(FoodItem item)
        {
            this.Context.FoodItems.Add(item);
        }

        public void Delete(FoodItem item)
        {
            this.Context.FoodItems.Remove(item);
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public void Update(FoodItem item)
        {           
            this.Context.FoodItems.Update(item);
        }
    }
}
