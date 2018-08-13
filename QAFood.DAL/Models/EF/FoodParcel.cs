using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.DAL.Models
{
    /// <summary>
    /// Food Parcel.
    /// </summary>
    [Table("FoodParcels")]
    public class FoodParcel
    {
        [Key]
        [Required()]
        [Editable(allowEdit: false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Food Parcel Name")]
        [DataType(DataType.Text)]
        [MinLength(1, ErrorMessage = "The name is too short!")]
        [MaxLength(256, ErrorMessage = "The name is too long!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Posted Date")]
        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; }

        public List<FoodItem> FoodItems { get; set; }

        public List<TestResult> TestResults { get; set; }
    }
}
