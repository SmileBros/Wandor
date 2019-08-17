using System;
using System.Collections.Generic;
using System.Text;

namespace Wandor.Events
{
    public class StepCountChangedEvent : EventArgs
    {
        public int Value { get; set; }
    }
}
