using System;

namespace System.Collections.Generic
{
	// Token: 0x020004D1 RID: 1233
	[__DynamicallyInvokable]
	public interface IComparer<in T>
	{
		// Token: 0x06003ACE RID: 15054
		[__DynamicallyInvokable]
		int Compare(T x, T y);
	}
}
