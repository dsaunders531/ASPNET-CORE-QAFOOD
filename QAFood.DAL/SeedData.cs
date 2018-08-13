using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace QAFood.DAL
{
    /// <summary>
    /// Create the seed data for the application.
    /// </summary>
    public static class SeedData
    {
        public static void EnsurePopulated(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Create food parcels
            if (! context.FoodParcels.Any())
            {
                context.FoodParcels.Add(new Models.FoodParcel() { Name = "Parcel 2", PostedDate = DateTime.Now.AddDays(-7).Date });
                context.FoodParcels.Add(new Models.FoodParcel() { Name = "Parcel 1", PostedDate = DateTime.Now.AddDays(-14).Date });

                context.SaveChanges();

                long parcelId = (from Models.FoodParcel p in context.FoodParcels where p.Name == "Parcel 1" select p.Id).First();

                context.FoodItems.Add(new Models.FoodItem() { Name = "ICE1", Description = "Vanilla Ice Cream", FoodParcelId = parcelId });
                context.FoodItems.Add(new Models.FoodItem() { Name = "CHEESE1", Description = "Organic Cheese", FoodParcelId = parcelId });
                context.FoodItems.Add(new Models.FoodItem() { Name = "MEAT1", Description = "Mystery meat pie", FoodParcelId = parcelId });
                context.FoodItems.Add(new Models.FoodItem() { Name = "CRISP1", Description = "Vegetable Crisps", FoodParcelId = parcelId });

                parcelId = (from Models.FoodParcel p in context.FoodParcels where p.Name == "Parcel 2" select p.Id).First();

                context.FoodItems.Add(new Models.FoodItem() { Name = "MUSH1", Description = "Dried mixed mushrooms", FoodParcelId = parcelId });
                context.FoodItems.Add(new Models.FoodItem() { Name = "CHEESE2", Description = "Stinky Cheese", FoodParcelId = parcelId });
                context.FoodItems.Add(new Models.FoodItem() { Name = "LASAG1", Description = "Lasagne", FoodParcelId = parcelId });
                context.FoodItems.Add(new Models.FoodItem() { Name = "PUD1", Description = "Banana Custard", FoodParcelId = parcelId });
            }

            // create food items in food parcels
            #region "Categories"
            // create category list
            if (!context.TestItemCategories.Any(item => item.Value == "Presentation"))
            {
                context.TestItemCategories.Add(new Models.TestItemCategory { Value = "Presentation" });

            }

            if (!context.TestItemCategories.Any(item => item.Value == "Texture"))
            {
                context.TestItemCategories.Add(new Models.TestItemCategory { Value = "Texture" });

            }

            if (!context.TestItemCategories.Any(item => item.Value == "Aroma"))
            {
                context.TestItemCategories.Add(new Models.TestItemCategory { Value = "Aroma" });

            }

            if (!context.TestItemCategories.Any(item => item.Value == "Flavour"))
            {
                context.TestItemCategories.Add(new Models.TestItemCategory {  Value = "Flavour" });

            }
            #endregion

            context.SaveChanges();
        }
    }
}
