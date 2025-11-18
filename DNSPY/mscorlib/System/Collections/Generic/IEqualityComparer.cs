using System;

namespace System.Collections.Generic
{
	// Token: 0x020004D5 RID: 1237
	[__DynamicallyInvokable]
	public interface IEqualityComparer<in T>
	{
		// Token: 0x06003AD9 RID: 15065
		[__DynamicallyInvokable]
		bool Equals(T x, T y);

		// Token: 0x06003ADA RID: 15066
		[__DynamicallyInvokable]
		int GetHashCode(T obj);
	}
}
