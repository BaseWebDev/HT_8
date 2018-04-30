using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleListArray {
    class CustomList<T>:IEnumerable<T[]> where T : struct {
        public int Count { get; set; } = 0;
        private List<T[]> internalList = new List<T[]>();
        public void Add() {
            T[] arr = new T[] { default(T) };
            ++Count;
            internalList.Add(arr);
        }
        public void Add( T inValue)   {
            T[] arr = new T[] {inValue};
            ++Count;
            internalList.Add(arr);
        }
        public void Add(T inValue1, T inValue2) {
            T[] arr = new T[] { inValue1, inValue2};
            ++Count;
            internalList.Add(arr);
        }
        public void Add(T[] arr) {
            ++Count;
            internalList.Add(arr);
        }
        public void RemoveAt(int index) {
            --Count;
            internalList.RemoveAt(index);
        }

        public IEnumerator<T[]> GetEnumerator() {
            return internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return internalList.GetEnumerator();
        }
    }
}