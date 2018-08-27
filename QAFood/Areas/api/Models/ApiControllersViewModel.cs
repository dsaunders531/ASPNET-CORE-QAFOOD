using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.Areas.api.Models
{
    [NotMapped]
    public class ApiControllersViewModel : ViewModelBase
    {
        public ApiControllersViewModel() : base()
        {
            this.Controllers = new List<ApiControllerModel>();
        }

        public List<ApiControllerModel> Controllers { get; set; }
    }
}
