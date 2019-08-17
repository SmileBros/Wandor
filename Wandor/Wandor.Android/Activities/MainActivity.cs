using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Prism;
using Wandor.Droid.Crosses;
using Wandor.Droid.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Wandor.Droid.Activities
{
    [Activity(Label = "Wandor", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        private bool _isStepSensorAvaliable;
        private IPlatformInitializer _initializer;
        private StepSensorServiceConnection _connection;

        protected override void OnCreate(Bundle savedInstanceState) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Initialize();

            Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App(_initializer));
        }

        private void Initialize() {
            if (_connection == null) {
                _connection = new StepSensorServiceConnection();
            }

            _initializer = new AndroidInitializer {
                StepService = _connection
            };
        }

        protected override void OnStart() {
            base.OnStart();

            _isStepSensorAvaliable =
                PackageManager.HasSystemFeature(PackageManager.FeatureSensorStepCounter)
                && PackageManager.HasSystemFeature(PackageManager.FeatureSensorStepDetector);

            if (_isStepSensorAvaliable) {
                BindStepSensorService();
            }
        }

        protected override void OnResume() {
            base.OnResume();

            if (_isStepSensorAvaliable) {
                //StartStepSensorService();
            }
        }

        private Intent CreateStepSensorServiceIntent() {
            return new Intent(this, typeof(StepSensorService));
        }

        private void BindStepSensorService() {
            var intent = CreateStepSensorServiceIntent();

            BindService(intent, _connection, Bind.AutoCreate);
        }

        private void UnbindStepSensorService() {
            if (_connection != null) {
                UnbindService(_connection);
            }
        }

        private void StartStepSensorService() {
            var intent = CreateStepSensorServiceIntent();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O) {
                StartForegroundService(intent);
            } else {
                StartService(intent);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode,
                                                        string[] permissions,
                                                        [GeneratedEnum] Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
