using System;
using Wandor.Events;

namespace Wandor.Services
{
    public interface IStepService
    {
        int StepCount { get; }
        event EventHandler<int> StepCountChanged;
    }
}
