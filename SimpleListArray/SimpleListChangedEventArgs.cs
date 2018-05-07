using System;

namespace SimpleListArray
{
    class SimpleListChangedEventArgs <T> : EventArgs where T : struct {
        public T Value { get; private set; }
       
        public SimpleListChangedEventArgs(T value) {
            this.Value = value;
        }
    }
}