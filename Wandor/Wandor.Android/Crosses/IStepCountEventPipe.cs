using System.Collections.Generic;
using Wandor.Services;

namespace Wandor.Droid.Crosses
{
    public interface IStepCountEventPipe : IStepService
    {
        List<IStepCountEventPipe> Upstreams { get; }
        void OnStepCountChanged(object sender, int e);
    }
}
