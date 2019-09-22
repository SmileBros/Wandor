using Prism.Navigation;
using Wandor.Services;

namespace Wandor.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IStepService _stepService;
        private int _currentStepCount;

        public MainPageViewModel(INavigationService navigationService, IStepService stepService) : base(navigationService)
        {
            Title = "Wonder";
            _stepService = stepService;
        }

        public int CurrentStepCount { get => _currentStepCount; set => SetProperty(ref _currentStepCount, value); }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            _stepService.StepCountChanged += StepCountChanged;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            _stepService.StepCountChanged -= StepCountChanged;
        }

        private void StepCountChanged(object sender, int e)
        {
            CurrentStepCount = e;
        }
    }
}
