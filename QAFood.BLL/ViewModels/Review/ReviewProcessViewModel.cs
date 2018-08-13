using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.BLL.ViewModels
{
    /// <summary>
    /// The review process view model
    /// </summary>
    [NotMapped]
    public class ReviewProcessViewModel : ViewModelBase
    {
        public long FoodParcelId { get; set; }
        public string FoodParcelName { get; set; }
        public long TestId { get; set; }
        public string TestName { get; set; }
        public long SelectedFoodItemId { get; set; }
        public string FoodItemName { get; set; }

        public byte MinResultValue { get; } = 1;
        public byte MaxResultvalue { get; } = 5;

        [Required]
        [Display(Name = "Presentation")]
        [Range(minimum: 0, maximum: 5, ErrorMessage = "Please choose a value between 0 and 5")]
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
