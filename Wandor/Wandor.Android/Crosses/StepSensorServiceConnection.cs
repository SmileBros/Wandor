using Android.Content;
using Android.OS;

namespace Wandor.Droid.Crosses
{
    public class StepSensorServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public StepService StepService { get; set; }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            if (service is StepSensorServiceBinder binder)
            {
                StepService?.Wrap(binder.StepService);
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            StepService?.UnWrap();
        }
    }
}
