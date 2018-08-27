using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.Areas.api.Models
{
    [NotMapped]
    public class ReviewStartApiO
    {
        /// <summary>
        /// Create an instance of the ReviewStartApiO
        /// </summary>
        /// <remarks>Api Objects need a parameterless constructor so they can be discovered.</remarks>
        public ReviewStartApiO()
        { }

        [Required]
        public long FoodParcelId { get; set; }

        [Required]
        public long SelectedFoodItemId { get; set; }

        [Required]
        public long TestId { get; set; }
    }
}
