using Android.OS;
using Wandor.Services;

namespace Wandor.Droid.Crosses
{
    public class StepSensorServiceBinder : Binder
    {
        public IStepService StepService { get; set; }
    }
}
