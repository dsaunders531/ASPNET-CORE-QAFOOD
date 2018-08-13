using QAFood.DAL;
using QAFood.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QAFood.XUnitTests.TestRepositories
{
    /// <summary>
    /// A respository of TestResultItems for use by tests.
    /// </summary>
    public class TestResultItemTestRepository : IRepository<TestResultItem>
    {
        private List<TestResultItem> TestItems { get; set; } = new List<TestResultItem>();
        
        public IQueryable<TestResultItem> GetAll
        {
            get {               
                TestItems.Add(new TestResultItem() {
                    Category = new TestItemCategory() { Id = 0, Value = "Aroma" },
                    CategoryId = 0,
                    FoodItem = new FoodItem() { Id = 0, Description = "Test Food Item", Name = "Test Food Item", FoodParcelId = 0 },
                    FoodItemId = 0,
                    Id = 0,
                    Result = 5,
                    TestResult = new TestResult() { Id = 0, TestDate = DateTime.Now, Name = "Test Result Test", UserName = "Test User", FoodParcelId = 0, FoodParcel = null, TestResultItems = null },
                    TestResultId = 0
                });

                return TestItems.AsQueryable();
            }            
        }

        public TestResultItem Get(long id)
        {
            return (from TestResultItem t in this.GetAll where t.Id == id select t).First();
        }

        public void Save()
        {
            // Do nothing, update does all the required work.
        }

        public void Update(TestResultItem item)
        {
            this.TestItems.Clear(); // I only need 1 item in my test data.
            this.TestItems.Add(item);
        }

        // None of these are required for tests so far...
        public void Create(TestResultItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(TestResultItem item)
        {
            throw new NotImplementedException();
        }        
    }
}
