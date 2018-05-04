using System;

namespace SimpleListArray
{
    enum Operation:byte { add, delete, update }
    class SimpleListChangedEventArgs <T> : EventArgs where T : struct {
        public Operation CurOpretion { get; set; }
        public T[] Value { get; private set; }
        public SimpleListChangedEventArgs() : this(default(T[])) {
        }
        public SimpleListChangedEventArgs(T[] value) {
            this.Value = value;
        }
    }
}