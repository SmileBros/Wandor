using Prism;
using Prism.Ioc;
using Wandor.Droid.Services;
using Wandor.Services;

namespace Wandor.Droid.Crosses
{
    public class AndroidInitializer : IPlatformInitializer
    {
        public IStepService StepService { get; set; }

        public void RegisterTypes(IContainerRegistry containerRegistry) {
            // Register any platform specific implementations
            containerRegistry.RegisterInstance(StepService);
        }
    }
}
