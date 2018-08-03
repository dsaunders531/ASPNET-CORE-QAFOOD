using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using QAFood.EF;

namespace QAFood
{
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
            if (!context.ListOfValues.Any(item => item.Context == "TestCategory" && item.Value == "Presentation"))
            {
                context.ListOfValues.Add(new Models.LOV { Context = "TestCategory", Value = "Presentation" });

            }

            if (!context.ListOfValues.Any(item => item.Context == "TestCategory" && item.Value == "Texture"))
            {
                context.ListOfValues.Add(new Models.LOV { Context = "TestCategory", Value = "Texture" });

            }

            if (!context.ListOfValues.Any(item => item.Context == "TestCategory" && item.Value == "Aroma"))
            {
                context.ListOfValues.Add(new Models.LOV { Context = "TestCategory", Value = "Aroma" });

            }

            if (!context.ListOfValues.Any(item => item.Context == "TestCategory" && item.Value == "Flavour"))
            {
                context.ListOfValues.Add(new Models.LOV { Context = "TestCategory", Value = "Flavour" });

            }
            #endregion

            context.SaveChanges();
        }
    }
}
