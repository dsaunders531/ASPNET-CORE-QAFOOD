using mezzanine;
using QAFood.Models;
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
        }

        public IPageMeta PageMeta { get; set; }
    }
}
