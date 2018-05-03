using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleListArray {
  //  delegate void OnAdded<T>(T i);
  //  delegate void OnAdding<T>(T i);
    class CustomList<T>:IEnumerable<T[]>, IList<T[]> where T : struct {

        public EventHandler<SimpleListAddingEventArgs<T>> OnAdding;

        public EventHandler<SimpleListAddedEventArgs<T>> OnAdded;

        private List<T[]> internalList = new List<T[]>();

        public int Count { get =>internalList.Count; }

        public bool IsReadOnly => ((IList<T[]>)internalList).IsReadOnly;

        public T[] this[int index] { get => ((IList<T[]>)internalList)[index]; set => ((IList<T[]>)internalList)[index] = value; }

        public void Add() {
            T[] arr = new T[] { default(T) };
            internalList.Add(arr);
        }    
        
        public void Add(params T[] arr) {
            if (OnAdding != null) {
                var eventArgs = new SimpleListAddingEventArgs<T>(arr);
                OnAdding(this, eventArgs);
                if (eventArgs.Cancel) {
                    return;
                }
            }
            internalList.Add(arr);

            if (OnAdded != null) {
                OnAdded(this, new SimpleListAddedEventArgs<T>(arr));
            }
        }

        public void Insert(int index, T[] item) {
            ((IList<T[]>)internalList).Insert(index, item);
        }

        public void RemoveAt(int index) {
            if (internalList != null && internalList.Count > 1 && index >= 0 && index < internalList.Count) {
                internalList.RemoveAt(index);
            } else {
                throw new ArgumentOutOfRangeException(@"Значение индекса указано неверно!");
            }
        }

        public IEnumerator<T[]> GetEnumerator() {
            return internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return internalList.GetEnumerator();
        }

        public int IndexOf(T[] item) {
            return ((IList<T[]>)internalList).IndexOf(item);
        }

        public void Clear() {
            ((IList<T[]>)internalList).Clear();
        }

        public bool Contains(T[] item) {
            return ((IList<T[]>)internalList).Contains(item);
        }

        public void CopyTo(T[][] array, int arrayIndex) {
            ((IList<T[]>)internalList).CopyTo(array, arrayIndex);
        }

        public bool Remove(T[] item) {
            return ((IList<T[]>)internalList).Remove(item);
        }
    }
}