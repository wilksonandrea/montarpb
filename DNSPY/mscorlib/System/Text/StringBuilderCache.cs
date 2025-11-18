using System;

namespace System.Text
{
	// Token: 0x02000A5B RID: 2651
	internal static class StringBuilderCache
	{
		// Token: 0x0600673C RID: 26428 RVA: 0x0015BFF8 File Offset: 0x0015A1F8
		public static StringBuilder Acquire(int capacity = 16)
		{
			if (capacity <= 360)
			{
				StringBuilder cachedInstance = StringBuilderCache.CachedInstance;
				if (cachedInstance != null && capacity <= cachedInstance.Capacity)
				{
					StringBuilderCache.CachedInstance = null;
					cachedInstance.Clear();
					return cachedInstance;
				}
			}
			return new StringBuilder(capacity);
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x0015C034 File Offset: 0x0015A234
		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= 360)
			{
				StringBuilderCache.CachedInstance = sb;
			}
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x0015C04C File Offset: 0x0015A24C
		public static string GetStringAndRelease(StringBuilder sb)
		{
			string text = sb.ToString();
			StringBuilderCache.Release(sb);
			return text;
		}

		// Token: 0x04002E2F RID: 11823
		internal const int MAX_BUILDER_SIZE = 360;

		// Token: 0x04002E30 RID: 11824
		[ThreadStatic]
		private static StringBuilder CachedInstance;
	}
}
