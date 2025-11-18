using System;

namespace System.Collections
{
	// Token: 0x020004A5 RID: 1189
	[__DynamicallyInvokable]
	public interface IStructuralEquatable
	{
		// Token: 0x060038FD RID: 14589
		[__DynamicallyInvokable]
		bool Equals(object other, IEqualityComparer comparer);

		// Token: 0x060038FE RID: 14590
		[__DynamicallyInvokable]
		int GetHashCode(IEqualityComparer comparer);
	}
}
