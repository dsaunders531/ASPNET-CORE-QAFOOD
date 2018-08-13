using System.ComponentModel.DataAnnotations.Schema;

namespace QAFood.BLL.ViewModels
{
    /// <summary>
    /// The view model after starting a review.
    /// </summary>
    [NotMapped]
    public class ReviewStartResultViewModel : ViewModelBase
    {
        public long FoodParcelId { get; set; }
        public long TestId { get; set; }
        public long SelectedFoodItemId { get; set; }
    }
}
