using QAFood.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.ViewModels
{
    [NotMapped]
    public class TestResultViewModel : ViewModelBase
    {
        public List<FoodParcel> FoodParcels { get; set; }
    }
}
