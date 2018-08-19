using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using mezzanine;
using mezzanine.Models;

namespace QAFood.BLL.Models
{
    /// <summary>
    /// The pagination model.
    /// </summary>
    [NotMapped]
    public class PaginationModel : Pagination
    {
        // No additional fields required yet...
    }
}
