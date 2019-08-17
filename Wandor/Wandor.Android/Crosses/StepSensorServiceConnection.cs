using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;

namespace Wandor.Droid.Crosses
{
    public class StepSensorServiceConnection : Java.Lang.Object, IServiceConnection, IStepCountEventPipe
    {
        public List<IStepCountEventPipe> Upstreams { get; } = new List<IStepCountEventPipe>();

        public void OnServiceConnected(ComponentName name, IBinder service) {
            if (service is IStepCountEventPipe binder) {
                this.PipeAfter(binder);
            }
        }

        public void OnServiceDisconnected(ComponentName name) {
            this.Destroy();
        }

        public void OnStepCountChanged(object sender, int e) {
            StepCountChanged?.Invoke(sender, e);
        }

        public event EventHandler<int> StepCountChanged;
    }
}
