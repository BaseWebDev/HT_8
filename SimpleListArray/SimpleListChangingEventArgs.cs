using System;

namespace SimpleListArray
{
    class SimpleListChangingEventArgs<T> : EventArgs where T : struct {
        public Operation CurOpretion { get; set; }
        public T[] Value { get; private set; }
     
        public bool Cancel { get; set; }
        public SimpleListChangingEventArgs():this(default(T[])) {
        }

        public SimpleListChangingEventArgs(T[] value) {
            this.Value = value;
        }

    }
}