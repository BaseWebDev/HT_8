using System;

namespace SimpleListArray
{
    class SimpleListChangedEventArgs <T> : EventArgs where T : struct {
        public T[] Value { get; private set; }
        public SimpleListChangedEventArgs() : this(default(T[])) {
        }

        public SimpleListChangedEventArgs(T[] value) {
            this.Value = value;
        }
    }
}