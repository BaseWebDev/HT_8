using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleListArray {
    class CustomList<T>:IEnumerable<T[]> where T : struct {
        public int Count {
            get {
                return internalList.Count;
            }
        } 
        private List<T[]> internalList = new List<T[]>();
        public void Add() {
            T[] arr = new T[] { default(T) };
            internalList.Add(arr);
        }      
        public void RemoveAt(int index) {
            internalList.RemoveAt(index);
        }
        public void Add(params T[] arr) {
            internalList.Add(arr);
        }

        public IEnumerator<T[]> GetEnumerator() {
            return internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return internalList.GetEnumerator();
        }
    }
}