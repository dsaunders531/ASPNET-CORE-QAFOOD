using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.DAL.Models
{
    /// <summary>
    /// Test Result Items
    /// </summary>
    [Table("TestResultItems")]
    public class TestResultItem
    {
        [Key]
        [Required()]
        [Editable(allowEdit: false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required]
        [Editable(allowEdit: false)]
        [Display(Name = "Test result id")]
        public long TestResultId { get; set; }

        public TestResult TestResult { get; set; }

        [Required]
        [Editable(allowEdit: false)]
        [Display(Name = "Food item id")]
        public long FoodItemId { get; set; }

        public FoodItem FoodItem { get; set; }

        [Required]
        [Editable(allowEdit: false)]
        [Display(Name = "Category id")]
        public long CategoryId { get; set; }

        public TestItemCategory Category { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Food Item Rating")]
        [Range(minimum: 1, maximum: 5)]
        public byte Result { get; set; }
    }
}
