using Microsoft.AspNetCore.Mvc.Rendering;
using QAFood.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace QAFood.BLL.ViewModels
{
    /// <summary>
    /// The view model for starting a review.
    /// </summary>
    [NotMapped]
    public class ReviewStartViewModel : ViewModelBase
    {
        public FoodParcel FoodParcel { get; set; }

        public long FoodParcelId {
            get => this.FoodParcel.Id;
            set
            {
                FoodParcelId = value;
            }
        }

        public string TestName { get; set; }

        public long TestId { get; set; }

        [Required]
        [Display(Name = "Select an item to review")]
        [DataType(DataType.Text)]
        public long SelectedFoodItemId {get ;set; }

        // REVIEW - this should be in an extension so the model does no work.
        public IEnumerable<SelectListItem> FoodItems
        {
            get
            {
                List<SelectListItem> result = new List<SelectListItem>();

                foreach (FoodItem item in this.FoodParcel.FoodItems.OrderBy(o => o.Description))
                {
                    result.Add(new SelectListItem(item.Description, item.Id.ToString()));
                }

                if (result.Count > 0)
                {
                    result.First().Selected = true;
                }
                
                return result;
            }
        }
    }
}
