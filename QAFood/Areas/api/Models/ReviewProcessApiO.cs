using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.Areas.api.Models
{
    /// <summary>
    /// The review process view model
    /// </summary>
    [NotMapped]
    public class ReviewProcessApiO
    {
        /// <summary>
        /// Create an instance of the ReviewProcessApiO
        /// </summary>
        /// <remarks>Api Objects need a parameterless constructor so they can be discovered.</remarks>
        public ReviewProcessApiO()
        { }

        [Required]
        public long FoodParcelId { get; set; }

        [Required]
        public long TestId { get; set; }

        [Required]
        public long SelectedFoodItemId { get; set; }

        [Required]
        [Range(minimum: 0, maximum: 5, ErrorMessage = "Please choose a value between 0 and 5")]
        public byte PresentationValue { get; set; }

        [Required]
        [Range(minimum: 0, maximum: 5, ErrorMessage = "Please choose a value between 0 and 5")]
        public byte TextureValue { get; set; }

        [Required]
        [Range(minimum: 0, maximum: 5, ErrorMessage = "Please choose a value between 0 and 5")]
        public byte AromaValue { get; set; }

        [Required]
        [Range(minimum: 0, maximum: 5, ErrorMessage = "Please choose a value between 0 and 5")]
        public byte FlavourValue { get; set; }
    }
}
