using Prism;
using Prism.Ioc;
using Prism.Unity;
using Wandor.Services;
using Wandor.ViewModels;
using Wandor.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Wandor
{
    public partial class App : PrismApplication
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }

        protected override void RegisterTypes(IContainerRegistry container) {
            RegisterServices(container);

            RegisterViews(container);
        }

        private static void RegisterServices(IContainerRegistry container) {
            if (UseMockDataStore) {
                container.Register<MockDataStore>();
            } else {
                container.Register<AzureDataStore>();
            }
        }

        private static void RegisterViews(IContainerRegistry container) {
            container.RegisterForNavigation<NavigationPage>();
            container.RegisterForNavigation<MainPage>();
            container.RegisterForNavigation<StepCounterPage>();
        }

        protected override async void OnInitialized() {
            InitializeComponent();

            var result = await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }
    }
}
