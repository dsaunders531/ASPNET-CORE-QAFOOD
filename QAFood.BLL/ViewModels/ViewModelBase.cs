using mezzanine;
using QAFood.BLL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood
{
    /// <summary>
    /// The base view model. To be used by every view model.
    /// </summary>
    [NotMapped]
    public class ViewModelBase : IViewModel
    {
        public ViewModelBase()
        {
            this.PageMeta = new PageMetaModel();
            this.Pagination = null; // The default is null (no pagination)
        }

        public IPageMeta PageMeta { get; set; }

        public IPagination Pagination { get; set; }
    }
}
