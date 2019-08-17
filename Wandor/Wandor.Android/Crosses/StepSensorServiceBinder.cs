using System;
using System.Collections.Generic;
using Android.OS;

namespace Wandor.Droid.Crosses
{
    public class StepSensorServiceBinder : Binder, IStepCountEventPipe
    {
        public List<IStepCountEventPipe> Upstreams { get; } = new List<IStepCountEventPipe>();

        public event EventHandler<int> StepCountChanged;

        public void OnStepCountChanged(object sender, int e) {
            StepCountChanged?.Invoke(sender, e);
        }
    }


    public class StepSensorServiceBinder2 : Binder
    {
        public IStepCounter StepCounter { get; set; }
    }
}
