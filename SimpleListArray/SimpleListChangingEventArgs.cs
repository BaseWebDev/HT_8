﻿using System;

namespace SimpleListArray
{
    class SimpleListChangingEventArgs<T> : EventArgs where T : struct {
       public T Value { get; private set; }
     
        public bool Cancel { get; set; }
        
        public SimpleListChangingEventArgs(T value) {
            this.Value = value;
        }

    }
}