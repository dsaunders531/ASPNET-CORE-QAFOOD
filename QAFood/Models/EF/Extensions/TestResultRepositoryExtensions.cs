using QAFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAFood.EF
{
    /// <summary>
    /// Extension methods for IRepository TestResult
    /// </summary>
    public static class TestResultRepositoryExtensions
    {
        /// <summary>
        /// Get a food parcel for a user.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="foodParcelId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static TestResult GetByUser(this IRepository<TestResult> me, long foodParcelId, string username)
        {
            return (from TestResult t in me.GetAll where t.FoodParcelId == foodParcelId && t.UserName == username select t).FirstOrDefault();
        }
    }
}
