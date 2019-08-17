using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Wandor.Droid.Activities;
using Wandor.Droid.Crosses;
using Wandor.Droid.EventListeners;
using Wandor.Services;

namespace Wandor.Droid.Services
{
    [Service]
    public class StepSensorService : Service
    {
        public const int ServiceRunningNotificationId = 10000;
        private const string ApplicationNotificationChannelId = "com.miehaha.wandor.NotificationChannel";

        private const string TAG = "StepService";

        private SensorManager _sensorManager;
        private Sensor _stepCounterSensor;
        //private Sensor _stepDetectorSensor;
        private StepCounterEventListener _stepCounterSensorEventListener;
        //private StepDetectorEventListener _stepDetectorSensorEventListener;
        private StepSensorServiceBinder _binder;

        public override IBinder OnBind(Intent intent) {
            Log.Info(TAG, "OnBind");

            _binder = new StepSensorServiceBinder() {
                StepService = _stepCounterSensorEventListener
            };

            return _binder;
        }

        public override void OnCreate() {
            base.OnCreate();

            _sensorManager = (SensorManager)GetSystemService(SensorService);
            _stepCounterSensor = _sensorManager.GetDefaultSensor(SensorType.StepCounter);//获取计步总数传感器
            _stepCounterSensorEventListener = new StepCounterEventListener();
            //_stepDetectorSensor = _sensorManager.GetDefaultSensor(SensorType.StepDetector);//获取单次计步传感器
            //_stepDetectorSensorEventListener = new StepDetectorEventListener();

            RegisterNotificationChannel();
            RegisterSensorEventListener();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O) {
                StartAsForeground();
            }
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId) {
            Log.Debug(TAG, "OnStartCommand");

            return base.OnStartCommand(intent, flags, startId);
        }

        private void StartAsForeground() {
            // Code not directly related to publishing the notification has been omitted for clarity.
            // Normally, this method would hold the code to be run when the service is started.
            var intent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MainActivity)), PendingIntentFlags.UpdateCurrent);
            using (var notification = new Notification.Builder(this, ApplicationNotificationChannelId)
                .SetContentTitle(Resources.GetString(Resource.String.step_sensor_service_title))
                .SetContentText(Resources.GetString(Resource.String.step_sensor_service_content))
                .SetSmallIcon(Resource.Drawable.xamarin_logo)
                .SetContentIntent(intent)
                .SetOngoing(true)
                .Build()) {
                //Enlist this instance of the service as a foreground service
                StartForeground(ServiceRunningNotificationId, notification);
            }
        }

        public override void OnDestroy() {
            UnregisterListener();
            base.OnDestroy();
        }

        private void RegisterSensorEventListener() {
            Log.Debug(TAG, "RegisterListener");

            _sensorManager.RegisterListener(_stepCounterSensorEventListener, _stepCounterSensor, SensorDelay.Fastest);
            //_sensorManager.RegisterListener(_stepDetectorSensorEventListener, _stepDetectorSensor, SensorDelay.Fastest);
        }

        private void UnregisterListener() {
            Log.Debug(TAG, "UnregisterListener");

            _sensorManager.UnregisterListener(_stepCounterSensorEventListener);
            //_sensorManager.UnregisterListener(_stepDetectorSensorEventListener);
        }

        private void RegisterNotificationChannel() {
            var manager = (NotificationManager)GetSystemService(NotificationService);
            var channel = manager.GetNotificationChannel(ApplicationNotificationChannelId);
            if (channel == null) {
                channel = new NotificationChannel(ApplicationNotificationChannelId,
                                                  Resources.GetString(Resource.String.step_sensor_service_notification_channel_name),
                                                  NotificationImportance.Min);
                channel.EnableLights(false);
                channel.EnableVibration(false);
                manager.CreateNotificationChannel(channel);
            }
        }
    }
}
