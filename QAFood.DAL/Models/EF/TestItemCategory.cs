using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.DAL.Models
{
    /// <summary>
    /// Test Item Categories.
    /// </summary>
    [Table("TestItemCategories")]
    public class TestItemCategory
    {
        [Key]
        [Required()]
        [Editable(allowEdit: false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Value")]
        [DataType(DataType.Text)]
        [MinLength(1, ErrorMessage = "The value is too short!")]
        [MaxLength(256, ErrorMessage = "The value is too long!")]
        public string Value { get; set; }
    }
}
