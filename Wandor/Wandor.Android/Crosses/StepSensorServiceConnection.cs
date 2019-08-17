using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Wandor.Services;

namespace Wandor.Droid.Crosses
{
    public class StepSensorServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public IStepService StepCounter { get; set; }

        public void OnServiceConnected(ComponentName name, IBinder service) {
            if (service is StepSensorServiceBinder binder) {
                StepCounter = binder.StepService;
            }
        }

        public void OnServiceDisconnected(ComponentName name) {
            StepCounter = null;
        }
    }
}
