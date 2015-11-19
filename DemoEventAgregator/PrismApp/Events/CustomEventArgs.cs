using System;

namespace PrismApp.Events
{
    public class CustomEventArgs: EventArgs
    {
        public string Value { get; private set; }

        public CustomEventArgs(string value)
        {
            Value = value;
        }
    }
}
