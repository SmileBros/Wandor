using System;
using Android.Hardware;
using Android.Runtime;
using Wandor.Services;

namespace Wandor.Droid.EventListeners
{
    public class StepCounterEventListener : Java.Lang.Object, ISensorEventListener, IStepService
    {
        private const int UninitializedCount = -1;

        private int _initStepCount = UninitializedCount;

        public int StepCount { get; set; }

        public event EventHandler<int> StepCountChanged;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy) {
        }

        public void OnSensorChanged(SensorEvent e) {
            //Log.Debug(TAG, $"StepCounter::OnSensorChanged::{e.Values[0]} --- {e.Timestamp} --- {e.Accuracy}");
            int currentTotleStepCount = (int)e.Values[0];
            if (_initStepCount == UninitializedCount) {
                _initStepCount = currentTotleStepCount;
            }
            StepCount = currentTotleStepCount - _initStepCount;
            OnStepCountChanged(e, StepCount);
        }

        public void OnStepCountChanged(object sender, int e) {
            StepCountChanged?.Invoke(sender, e);
        }
    }
}
