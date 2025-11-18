namespace Plugin.Core.Utility
{
    using System;
    using System.Collections.Generic;

    public class SafeList<T>
    {
        private readonly List<T> list_0;
        private readonly object object_0;

        public SafeList()
        {
            this.list_0 = new List<T>();
            this.object_0 = new object();
        }

        public void Add(T Value)
        {
            object obj2 = this.object_0;
            lock (obj2)
            {
                this.list_0.Add(Value);
            }
        }

        public void Clear()
        {
            object obj2 = this.object_0;
            lock (obj2)
            {
                this.list_0.Clear();
            }
        }

        public bool Contains(T Value)
        {
            object obj2 = this.object_0;
            lock (obj2)
            {
                return this.list_0.Contains(Value);
            }
        }

        public int Count()
        {
            object obj2 = this.object_0;
            lock (obj2)
            {
                return this.list_0.Count;
            }
        }

        public bool Remove(T Value)
        {
            object obj2 = this.object_0;
            lock (obj2)
            {
                return this.list_0.Remove(Value);
            }
        }
    }
}

