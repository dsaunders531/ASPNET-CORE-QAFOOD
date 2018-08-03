using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.Models
{
    /// <summary>
    /// The error view model.
    /// </summary>
    [NotMapped]
    public class ErrorViewModel : ViewModelBase
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}