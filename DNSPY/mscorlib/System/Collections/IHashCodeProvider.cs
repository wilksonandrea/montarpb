using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020004A1 RID: 1185
	[Obsolete("Please use IEqualityComparer instead.")]
	[ComVisible(true)]
	public interface IHashCodeProvider
	{
		// Token: 0x060038C4 RID: 14532
		int GetHashCode(object obj);
	}
}
