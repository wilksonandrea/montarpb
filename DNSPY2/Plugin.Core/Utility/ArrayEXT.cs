using System;

namespace Plugin.Core.Utility
{
	// Token: 0x02000035 RID: 53
	public static class ArrayEXT
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x00018B00 File Offset: 0x00016D00
		public static void ForEach(this Array array, Action<Array, int[]> action)
		{
			if (array.LongLength == 0L)
			{
				return;
			}
			Class8 @class = new Class8(array);
			do
			{
				action(array, @class.int_0);
			}
			while (@class.method_0());
		}
	}
}
