using Prism;
using Prism.Ioc;
using Wandor.Services;

namespace Wandor.Droid.Crosses
{
    public class AndroidInitializer : IPlatformInitializer
    {
        public StepService StepService { get; set; }

        public void RegisterTypes(IContainerRegistry containerRegistry) {
            // Register any platform specific implementations
            containerRegistry.RegisterInstance<IStepService>(StepService);
        }
    }
}
