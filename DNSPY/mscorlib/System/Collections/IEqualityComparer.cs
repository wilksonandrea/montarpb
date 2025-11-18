using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020004A0 RID: 1184
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IEqualityComparer
	{
		// Token: 0x060038C2 RID: 14530
		[__DynamicallyInvokable]
		bool Equals(object x, object y);

		// Token: 0x060038C3 RID: 14531
		[__DynamicallyInvokable]
		int GetHashCode(object obj);
	}
}
