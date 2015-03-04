
using Microsoft.Phone.Tasks;
namespace PictureGallery.Commands
{
    public class RateCommand : CommandBase
    {
        protected override void ExecuteCore(object parameter)
        {
            MarketplaceReviewTask reviewTask = new MarketplaceReviewTask();
            reviewTask.Show();
        }
    }
}
