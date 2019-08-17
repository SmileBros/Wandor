namespace Wandor.Droid.Crosses
{
    public static class IStepCountEventPipeExtension
    {
        public static void PipeAfter(this IStepCountEventPipe pipe, IStepCountEventPipe upstream) {
            if (pipe.Upstreams.Contains(upstream)) {
                return;
            }
            pipe.Upstreams.Add(upstream);
            upstream.StepCountChanged += pipe.OnStepCountChanged;
        }

        public static void Destroy(this IStepCountEventPipe pipe) {

            pipe.Upstreams.ForEach((upstream) => upstream.StepCountChanged -= pipe.OnStepCountChanged);
            pipe.Upstreams.Clear();
        }
    }
}
