using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.BLL.ViewModels.Authentication
{
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
