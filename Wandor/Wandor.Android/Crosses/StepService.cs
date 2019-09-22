using System;
using Wandor.Services;

namespace Wandor.Droid.Crosses
{
    public class StepService : IStepService
    {
        private IStepService _stepService;

        public int StepCount => _stepService?.StepCount ?? 0;

        public event EventHandler<int> StepCountChanged;

        public void Wrap(IStepService stepService)
        {
            UnWrap();
            _stepService = stepService;
            _stepService.StepCountChanged += OnStepCountChanged;
            OnStepCountChanged(this, StepCount);
        }

        private void OnStepCountChanged(object sender, int e)
        {
            StepCountChanged?.Invoke(sender, e);
        }

        public void UnWrap()
        {
            if (_stepService != null)
            {
                _stepService.StepCountChanged -= OnStepCountChanged;
            }
        }
    }
}
