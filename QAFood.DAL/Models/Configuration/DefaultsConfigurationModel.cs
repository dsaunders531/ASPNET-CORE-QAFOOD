using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.DAL.Models
{
    /// <summary>
    /// App configuration defaults model.
    /// </summary>
    [NotMapped]
    public class DefaultsConfigurationModel
    {
        public int ListItemsPerPage { get; set; }
    }
}
