using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleListArray {
    delegate void OnRemoving<T>(T i);

    class CustomList<T> : IEnumerable<T[]>, IList<T[]>,IEquatable<T[]> where T : struct {
        /// <summary>
        /// Событие до добавления
        /// </summary>
        public event EventHandler<SimpleListChangingEventArgs<T>> OnAdding;
        /// <summary>
        /// Событие после добавления
        /// </summary>
        public event EventHandler<SimpleListChangedEventArgs<T>> OnAdded;
        /// <summary>
        /// Делегат для удаления с помощью RemoveAt
        /// </summary>
        public OnRemoving<int> onRemovingAt;
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
                var eventArgs = new SimpleListChangingEventArgs<T>(arr);
                OnAdding(this, eventArgs);
                if (eventArgs.Cancel) {
                    return;
                }
            }
            internalList.Add(arr);

            if (OnAdded != null) {
                OnAdded(this, new SimpleListChangedEventArgs<T>(arr));
            }
            Updated(new SimpleListChangedEventArgs<T>(arr));
        }

        public void Insert(int index, T[] item) {
            if (OnAdding != null) {
                var eventArgs = new SimpleListChangingEventArgs<T>(item);
                OnAdding(this, eventArgs);
                if (eventArgs.Cancel) {
                    return;
                }
            }
            ((IList<T[]>)internalList).Insert(index, item);
            if (OnAdded != null) {
                OnAdded(this, new SimpleListChangedEventArgs<T>(item));
            }
            Updated(new SimpleListChangedEventArgs<T>(item));
        }

        public void RemoveAt(int index) {
            if (internalList != null && internalList.Count > 1 && index >= 0 && index < internalList.Count) {
                internalList.RemoveAt(index);
            } else {
                throw new ArgumentOutOfRangeException(@"Значение индекса указано неверно!");
            }
            if (onRemovingAt != null) {
                onRemovingAt(index);
            }
            Updated(new SimpleListChangedEventArgs<T>());
        }
        public void Clear() {
            if (OnRemoving != null) {
                var eventArgs = new SimpleListChangingEventArgs<T>();
                OnRemoving(this, eventArgs);
                if (eventArgs.Cancel) {
                    return;
                }
            }
            ((IList<T[]>)internalList).Clear();
            if (OnRemoved != null) {
                OnRemoved(this, new SimpleListChangedEventArgs<T>());
            }
            Updated(new SimpleListChangedEventArgs<T>());
        }
        public bool Remove(T[] item) {
            if (OnRemoving!=null) {
                var eventArgs = new SimpleListChangingEventArgs<T>(item);
                OnRemoving(this,eventArgs);
                if (eventArgs.Cancel) {
                    return false;
                }
            }           
            var temp = ((IList<T[]>)internalList).Remove(item);
            if (OnRemoved!=null) {
                OnRemoved(this, new SimpleListChangedEventArgs<T>(item));
            }
            Updated(new SimpleListChangedEventArgs<T>(item));
            return temp;
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

        public bool Contains(T[] item) {
            return ((IList<T[]>)internalList).Contains(item);
        }

        public void CopyTo(T[][] array, int arrayIndex) {
            ((IList<T[]>)internalList).CopyTo(array, arrayIndex);
        }

        bool IEquatable<T[]>.Equals(T[] other) => Equals(this,other);
        public static bool Equals(T[] arg1 , T[] arg2) {
            return Equals(arg1, arg2);
        }

        public override int GetHashCode() {
            return internalList.GetHashCode();
        }

        void Updated(SimpleListChangedEventArgs<T> arg) {
            if (OnUpdated != null) {
                OnUpdated(this, arg);
            }

        }
    }
}