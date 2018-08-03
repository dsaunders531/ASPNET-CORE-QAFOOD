using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.ViewModels.Authentication
{
    // REVIEW - too many classes in one file.

    /// <summary>
    /// View model for creating an account.
    /// </summary>
    [NotMapped]
    public class CreateAccountViewModel : ViewModelBase
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Your Name")]
        [MinLength(4, ErrorMessage ="The name is too short!")]
        [MaxLength(256, ErrorMessage = "The name is too long!")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]        
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    /// <summary>
    /// View model for logging in.
    /// </summary>
    [NotMapped]
    public class LoginViewModel : ViewModelBase
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [NotMapped]
        public string ReturnUrl { get; set; }
    }
}
