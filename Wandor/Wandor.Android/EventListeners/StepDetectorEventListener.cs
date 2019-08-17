using Android.Hardware;
using Android.Runtime;
using Java.Lang;

namespace Wandor.Droid.EventListeners
{
    public class StepDetectorEventListener : Object, ISensorEventListener
    {
        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy) {
        }

        public void OnSensorChanged(SensorEvent e) {
            //Log.Debug(TAG, $"StepDetector::OnSensorChanged::{e.Values[0]} --- {e.Timestamp} --- {e.Accuracy}");
        }
    }
}
