using System;

namespace System
{
	// Token: 0x020000F7 RID: 247
	internal static class LazyHelpers
	{
		// Token: 0x06000F12 RID: 3858 RVA: 0x0002EF09 File Offset: 0x0002D109
		// Note: this type is marked as 'beforefieldinit'.
		static LazyHelpers()
		{
		}

		// Token: 0x04000599 RID: 1433
		internal static readonly object PUBLICATION_ONLY_SENTINEL = new object();
	}
}
