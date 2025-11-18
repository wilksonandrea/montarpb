using System;
using System.Collections.Generic;

namespace Plugin.Core.Utility
{
	public class SafeList<T>
	{
		private readonly List<T> list_0;

		private readonly object object_0;

		public SafeList()
		{
		}

		public void Add(T Value)
		{
			lock (this.object_0)
			{
				this.list_0.Add(Value);
			}
		}

		public void Clear()
		{
			lock (this.object_0)
			{
				this.list_0.Clear();
			}
		}

		public bool Contains(T Value)
		{
			bool flag;
			lock (this.object_0)
			{
				flag = this.list_0.Contains(Value);
			}
			return flag;
		}

		public int Count()
		{
			int count;
			lock (this.object_0)
			{
				count = this.list_0.Count;
			}
			return count;
		}

		public bool Remove(T Value)
		{
			bool flag;
			lock (this.object_0)
			{
				flag = this.list_0.Remove(Value);
			}
			return flag;
		}
	}
}