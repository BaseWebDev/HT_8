using System;

namespace SimpleListArray
{
    class SimpleListAddedEventArgs <T> : EventArgs where T : struct {
        public T[] Value { get; private set; }

        public SimpleListAddedEventArgs(T[] value) {
            this.Value = value;
        }
    }
}