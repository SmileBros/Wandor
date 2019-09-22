using Prism.Navigation;

namespace Wandor.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        public AboutPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "About";

            //OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        //public ICommand OpenWebCommand { get; }
    }
}
