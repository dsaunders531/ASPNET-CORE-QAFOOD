using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.DAL.Models
{
    /// <summary>
    /// Test Results.
    /// </summary>
    [Table("TestResults")]
    public class TestResult
    {
        [Key]
        [Required()]
        [Editable(allowEdit: false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Test Result Name")]
        [DataType(DataType.Text)]
        [MinLength(1, ErrorMessage = "The name is too short!")]
        [MaxLength(256, ErrorMessage = "The name is too long!")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Test Date")]
        [DataType(DataType.Date)]
        public DateTime TestDate { get; set; }

        [Required]
        [Display(Name = "Tester Name")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Food parcel id")]
        public long FoodParcelId { get; set; }

        public FoodParcel FoodParcel { get; set; }

        public List<TestResultItem> TestResultItems { get; set; }
    }
}
