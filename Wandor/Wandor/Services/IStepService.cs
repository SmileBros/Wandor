using System;
using Wandor.Events;

namespace Wandor.Services
{
    public interface IStepService
    {
        int StepCount { get; set; }
        event EventHandler<int> StepCountChanged;
    }
}
