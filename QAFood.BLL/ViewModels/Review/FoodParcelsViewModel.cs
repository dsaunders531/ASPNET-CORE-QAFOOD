using QAFood.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.BLL.ViewModels
{
    [NotMapped]
    public class FoodParcelsViewModel : ViewModelBase
    {
        public List<FoodParcel> FoodParcels { get; set; }
    }
}
