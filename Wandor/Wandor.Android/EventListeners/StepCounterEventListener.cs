using System;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using Wandor.Services;

namespace Wandor.Droid.EventListeners
{
    public class StepCounterEventListener : Java.Lang.Object, ISensorEventListener, IStepService
    {
        private const string HISTORY_STEP_TIMESTAMP = "laststeptimestamp";
        private const string HISTORY_STEP_COUNT = "laststepcount";
        private const string OFFSET_VALUE = "offsetvalue";
        private const string TOTAL_ENERGY = "totalenergy";

        private const int UninitializedCount = -1;
        private readonly ISharedPreferences _sharedPreferences;
        private DateTime _stepTimestamp;
        private long _historyStepTimestamp;
        private int _offsetStepValue;

        public StepCounterEventListener(ISharedPreferences sharedPreferences)
        {
            _sharedPreferences = sharedPreferences;
            UpdateHistoryData();
        }

        public int StepCount { get; set; }
        private DateTime LastStepTimestap => new DateTime(_historyStepTimestamp, DateTimeKind.Utc);

        public event EventHandler<int> StepCountChanged;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            int currentTotalStepCount = (int)e.Values[0];
            _stepTimestamp = DateTime.UtcNow;
            if (DateTime.Compare(LastStepTimestap, DateTime.Today) < 0)
            {
                _offsetStepValue = currentTotalStepCount;
                Save();
            }
            else if (_offsetStepValue == UninitializedCount)
            {
                _offsetStepValue = _sharedPreferences.GetInt(OFFSET_VALUE, currentTotalStepCount);
            }

            StepCount = currentTotalStepCount - _offsetStepValue;
            OnStepCountChanged();
        }

        public void Save()
        {
            _sharedPreferences.Edit()
                .PutInt(HISTORY_STEP_COUNT, StepCount)
                .PutLong(HISTORY_STEP_TIMESTAMP, _stepTimestamp.Ticks)
                .PutInt(OFFSET_VALUE, _offsetStepValue)
                .Apply();
            UpdateHistoryData();
        }

        private void OnStepCountChanged()
        {
            StepCountChanged?.Invoke(this, StepCount);
        }

        private void UpdateHistoryData()
        {
            _historyStepTimestamp = _sharedPreferences.GetLong(HISTORY_STEP_TIMESTAMP, UninitializedCount);
            _offsetStepValue = _sharedPreferences.GetInt(OFFSET_VALUE, UninitializedCount);
        }
    }
}
