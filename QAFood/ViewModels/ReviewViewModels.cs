using Microsoft.AspNetCore.Mvc.Rendering;
using QAFood.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace QAFood.ViewModels
{
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

    [NotMapped]
    public class ReviewStartResultViewModel : ViewModelBase
    {
        
        public long FoodParcelId { get; set; }
        public long TestId { get; set; }
        public long SelectedFoodItemId { get; set; }
    }

    [NotMapped]
    public class ReviewProcessViewModel : ViewModelBase
    {
        public long FoodParcelId { get; set; }
        public string FoodParcelName { get; set; }
        public long TestId { get; set; }
        public string TestName { get; set; }
        public long SelectedFoodItemId { get; set; }
        public string FoodItemName { get; set; }

        //public List<KeyValuePair<string, byte>> Options { get; set; }
        // ??? for speed - try hardcoding the category and value first
        // The disadvantage is that if a new category is added or changed you will need
        // to update this model, view and controller.

        public byte MinResultValue { get; } = 1;
        public byte MaxResultvalue { get; } = 5;

        [Required]
        [Display(Name = "Presentation")]
        [Range(minimum:0, maximum:5, ErrorMessage = "Please choose a value between 0 and 5")]
        public byte PresentationValue { get; set; }

        [Required]
        [Display(Name = "Texture")]
        [Range(minimum: 0, maximum: 5, ErrorMessage = "Please choose a value between 0 and 5")]
        public byte TextureValue { get; set; }

        [Required]
        [Display(Name = "Aroma")]
        [Range(minimum: 0, maximum: 5, ErrorMessage = "Please choose a value between 0 and 5")]
        public byte AromaValue { get; set; }

        [Required]
        [Display(Name = "Flavour")]
        [Range(minimum: 0, maximum: 5, ErrorMessage = "Please choose a value between 0 and 5")]
        public byte FlavourValue { get; set; }
    }
}
