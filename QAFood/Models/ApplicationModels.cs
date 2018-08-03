using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.Models
{
    // REVIEW - too many classes in one file.

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

        public LOV Category { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Food Item Rating")]
        [Range(minimum:1, maximum:5)]
        public byte Result { get; set; }
    }

    [Table("LOV")]
    public class LOV
    {
        [Key]
        [Required()]
        [Editable(allowEdit: false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "List of values context")]
        [DataType(DataType.Text)]
        [MinLength(1, ErrorMessage = "The context is too short!")]
        [MaxLength(256, ErrorMessage = "The context is too long!")]
        public string Context { get; set; }

        [Required]
        [Display(Name = "Value")]
        [DataType(DataType.Text)]
        [MinLength(1, ErrorMessage = "The value is too short!")]
        [MaxLength(256, ErrorMessage = "The value is too long!")]
        public string Value { get; set; }
    }
}
