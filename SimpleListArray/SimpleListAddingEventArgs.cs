using System;

namespace SimpleListArray
{
    class SimpleListAddingEventArgs<T> : EventArgs where T : struct {
        public T[] Value { get; private set; }
     
        public bool Cancel { get; set; }

        public SimpleListAddingEventArgs(T[] value) {
            this.Value = value;
        }
    }
}