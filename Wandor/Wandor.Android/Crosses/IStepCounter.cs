using Android.Hardware;

namespace Wandor.Droid.Crosses
{
    public interface IStepCounter : ISensorEventListener, IStepCountEventPipe
    {
        int StepCount { get; set; }
    }
}
