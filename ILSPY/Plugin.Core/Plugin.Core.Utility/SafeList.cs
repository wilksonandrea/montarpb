using System.Collections.Generic;

namespace Plugin.Core.Utility;

public class SafeList<T>
{
	private readonly List<T> list_0 = new List<T>();

	private readonly object object_0 = new object();

	public void Add(T Value)
	{
		lock (object_0)
		{
			list_0.Add(Value);
		}
	}

	public void Clear()
	{
		lock (object_0)
		{
			list_0.Clear();
		}
	}

	public bool Contains(T Value)
	{
		lock (object_0)
		{
			return list_0.Contains(Value);
		}
	}

	public int Count()
	{
		lock (object_0)
		{
			return list_0.Count;
		}
	}

	public bool Remove(T Value)
	{
		lock (object_0)
		{
			return list_0.Remove(Value);
		}
	}
}
