using System;

namespace Plugin.Core.Utility;

public static class ArrayEXT
{
	public static void ForEach(this Array array, Action<Array, int[]> action)
	{
		if (array.LongLength != 0L)
		{
			Class8 @class = new Class8(array);
			do
			{
				action(array, @class.int_0);
			}
			while (@class.method_0());
		}
	}
}
