using Prism.Navigation;

namespace Wandor.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        public AppShellViewModel(INavigationService navigationService) : base(navigationService) {
            Title = "AppShell";
        }
    }
}
