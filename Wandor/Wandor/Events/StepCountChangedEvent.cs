using System;

namespace Wandor.Events
{
    public class StepCountChangedEvent : EventArgs
    {
        public int Value { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
