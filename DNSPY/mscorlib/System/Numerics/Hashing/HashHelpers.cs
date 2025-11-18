using System;

namespace System.Numerics.Hashing
{
	// Token: 0x02000389 RID: 905
	internal static class HashHelpers
	{
		// Token: 0x06002CDE RID: 11486 RVA: 0x000A8FCC File Offset: 0x000A71CC
		public static int Combine(int h1, int h2)
		{
			uint num = (uint)((h1 << 5) | (int)((uint)h1 >> 27));
			return (int)((num + (uint)h1) ^ (uint)h2);
		}
	}
}
