using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.DAL.Models
{
    /// <summary>
    /// Food Item.
    /// </summary>
    [Table("FoodItems")]
    public class FoodItem
    {
        [Key]
        [Required()]
        [Editable(allowEdit: false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Food Item Name")]
        [DataType(DataType.Text)]
        [MinLength(1, ErrorMessage = "The name is too short!")]
        [MaxLength(256, ErrorMessage = "The name is too long!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Food Item Description")]
        [DataType(DataType.Text)]
        [MinLength(1, ErrorMessage = "The name is too short!")]
        [MaxLength(256, ErrorMessage = "The name is too long!")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Food parcel id")]
        [Editable(allowEdit: false)]
        public long FoodParcelId { get; set; }

        public FoodParcel FoodParcel { get; set; }

    }
}
