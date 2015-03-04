using Microsoft.Phone.Tasks;

namespace PictureGallery.Commands
{
    public class SendMailCommand : CommandBase
    {
        protected override void ExecuteCore(object parameter)
        {
            EmailComposeTask emailTask = new EmailComposeTask();
            emailTask.To = App.SupportMail;
            emailTask.Subject = string.Format("{0} Feedback", App.ApplicationName);
            emailTask.Show();
        }
    }
}
