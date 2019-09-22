using System;
using Android.Content;
using Android.Hardware;
using Android.Runtime;

namespace Wandor.Droid.EventListeners
{
    public class StepCounterEventListener : Java.Lang.Object, ISensorEventListener
    {
        private readonly Action<int> _onSensorChanged;

        public StepCounterEventListener(Action<int> onSensorChanged, ISharedPreferences sharedPreferences)
        {
            _onSensorChanged = onSensorChanged;
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            int currentTotalStepCount = (int)e.Values[0];
            _onSensorChanged(currentTotalStepCount);
        }
    }
}
