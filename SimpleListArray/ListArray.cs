using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleListArray {
    delegate void OnRemoving<T>(T i);

    class CustomList<T> : IEnumerable<T>, IList<T> where T : struct {
        /// <summary>
        /// Событие до добавления
        /// </summary>
        public event EventHandler<SimpleListChangingEventArgs<T>> OnAdding;
        /// <summary>
        /// Событие после добавления
        /// </summary>
        public event EventHandler<SimpleListChangedEventArgs<T>> OnAdded;
        /// <summary>
        /// Событие до удаления
        /// </summary>
        public event EventHandler<SimpleListChangingEventArgs<T>> OnRemoving;
        /// <summary>
        /// Событие после удаления
        /// </summary>
        public event EventHandler<SimpleListChangedEventArgs<T>> OnRemoved;
        /// <summary>
        /// Событие на изменение листа
        /// </summary>
        public event EventHandler<SimpleListChangedEventArgs<T>> OnUpdated;

        private List<T> internalList = new List<T>();

        public int Count { get =>internalList.Count; }

        public bool IsReadOnly => false;

        public T this[int index] {
            get => internalList[index];
            set { internalList[index] = value; Updated(new SimpleListChangedEventArgs<T>(value)); }
        }

        public void Add(T item) {       
            internalList.Add(item);
        }

        public void Add(params T[] items) {
            foreach (var item in items) { 
                if (OnAdding != null) {
                    var eventArgs = new SimpleListChangingEventArgs<T>(item);
                    OnAdding(this, eventArgs);
                    if (eventArgs.Cancel) {
                        return;
                    }
                }
                internalList.Add(item);

                if (OnAdded != null) {
                    OnAdded(this, new SimpleListChangedEventArgs<T>(item));
                }
            }
        }

        public void Insert(int index, T item) {
            if (OnAdding != null) {
                var eventArgs = new SimpleListChangingEventArgs<T>(item);
                OnAdding(this, eventArgs);
                if (eventArgs.Cancel) {
                    return;
                }
            }
            internalList.Insert(index, item);
            if (OnAdded != null) {
                OnAdded(this, new SimpleListChangedEventArgs<T>(item));
            }
         }

        public bool Remove(T item) {
            if (OnRemoving != null) {
                var eventArgs = new SimpleListChangingEventArgs<T>(item);
                OnRemoving(this, eventArgs);
                if (eventArgs.Cancel) {
                    return false;
                }
            }
            var temp = internalList.Remove(item);
            if (OnRemoved != null) {
                OnRemoved(this, new SimpleListChangedEventArgs<T>(item));
            }
            return temp;
        }

        public void RemoveAt(int index) {         
            if (internalList != null && internalList.Count > 1 && index >= 0 && index < internalList.Count) {
                if (OnRemoving != null) {
                    var eventArgs = new SimpleListChangingEventArgs<T>(internalList[index]);
                    OnRemoving(this, eventArgs);
                    if (eventArgs.Cancel) {
                        return;
                    }
                }
                internalList.RemoveAt(index);           
                if (OnRemoved != null) {
                    OnRemoved(this, new SimpleListChangedEventArgs<T>(internalList[index]));
                }
            } else {
                throw new ArgumentOutOfRangeException(@"Значение индекса указано неверно!");
            }
        }

        public void Clear() {
            for (int i=0;i<internalList.Count;i++) {
                RemoveAt(i);            
            }
        }     

        public IEnumerator<T> GetEnumerator() {
            return internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return internalList.GetEnumerator();
        }

        public int IndexOf(T item) {
            return internalList.IndexOf(item);
        }

        public bool Contains(T item) {
            return internalList.Contains(item);
        }

        public void CopyTo(T[] itemay, int itemayIndex) {
            internalList.CopyTo(itemay, itemayIndex);
        }

        void Updated(SimpleListChangedEventArgs<T> arg) {
            if (OnUpdated != null) {
                OnUpdated(this, arg);
            }

        }
    }
}