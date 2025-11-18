using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Utility
{
	public static class ArrayEXT
	{
		public static void ForEach(this Array array, Action<Array, int[]> action)
		{
			if (array.LongLength == 0)
			{
				return;
			}
			Class8 class8 = new Class8(array);
			do
			{
				action(array, class8.int_0);
			}
			while (class8.method_0());
		}
	}
}