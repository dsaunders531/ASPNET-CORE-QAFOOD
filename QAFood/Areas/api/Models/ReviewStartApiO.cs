using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.Areas.api.Models
{
    [NotMapped]
    public class ReviewStartApiO
    {
        [Required]
        public long FoodParcelId { get; set; }

        [Required]
        public long SelectedFoodItemId { get; set; }

        [Required]
        public long TestId { get; set; }
    }
}
