using System;
using Wandor.Events;

namespace Wandor.Services
{
    public interface IStepService
    {
        event EventHandler<int> StepCountChanged;
    }
}
