using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Wandor.Droid.Crosses;
using Wandor.Droid.EventListeners;

namespace Wandor.Droid.Services
{
    [Service]
    public class StepSensorService : Service
    {
        public const int ServiceRunningNotificationId = 10000;
        private const string ApplicationNotificationChannelId = "com.miehaha.wandor.NotificationChannel";

        private const string SHARED_PREFERENCES_FILENAME = "StepPreferences";

        private SensorManager _sensorManager;
        private Sensor _stepCounterSensor;
        private StepCounterEventListener _stepCounterSensorEventListener;
        private StepSensorServiceBinder _binder;

        public override IBinder OnBind(Intent intent)
        {
            _binder = new StepSensorServiceBinder()
            {
                StepService = _stepCounterSensorEventListener
            };

            return _binder;
        }

        public override bool OnUnbind(Intent intent)
        {
            _stepCounterSensorEventListener.Save();
            return true;
        }

        public override void OnRebind(Intent intent)
        {
            base.OnRebind(intent);
        }

        public override void OnCreate()
        {
            base.OnCreate();

            var sharedPreferences = GetSharedPreferences(SHARED_PREFERENCES_FILENAME, FileCreationMode.Private);

            _sensorManager = (SensorManager)GetSystemService(SensorService);
            _stepCounterSensor = _sensorManager.GetDefaultSensor(SensorType.StepCounter);//获取计步总数传感器
            _stepCounterSensorEventListener = new StepCounterEventListener(sharedPreferences);

            RegisterNotificationChannel();
            RegisterSensorEventListener();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                StartAsForeground();
            }
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            return base.OnStartCommand(intent, flags, startId);
        }

        private void StartAsForeground()
        {
            // Code not directly related to publishing the notification has been omitted for clarity.
            // Normally, this method would hold the code to be run when the service is started.
            using (var notification = new Notification.Builder(this, ApplicationNotificationChannelId)
                .SetContentTitle(Resources.GetString(Resource.String.step_sensor_service_title))
                .SetContentText(Resources.GetString(Resource.String.step_sensor_service_content))
                .SetSmallIcon(Resource.Drawable.xamarin_logo)
                .SetOngoing(true)
                .Build())
            {
                //Enlist this instance of the service as a foreground service
                StartForeground(ServiceRunningNotificationId, notification);
            }
        }

        public override void OnDestroy()
        {
            UnregisterListener();
            base.OnDestroy();
        }

        private void RegisterSensorEventListener()
        {
            _sensorManager.RegisterListener(_stepCounterSensorEventListener, _stepCounterSensor, SensorDelay.Fastest);
        }

        private void UnregisterListener()
        {
            _stepCounterSensorEventListener.Save();
            _sensorManager.UnregisterListener(_stepCounterSensorEventListener);
        }

        private void RegisterNotificationChannel()
        {
            var manager = (NotificationManager)GetSystemService(NotificationService);
            var channel = manager.GetNotificationChannel(ApplicationNotificationChannelId);
            if (channel == null)
            {
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
