using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1D RID: 2589
	[Guid("bbe1fa4c-b0e3-4583-baef-1f1b2e483e56")]
	[ComImport]
	internal interface IVectorView<T> : IIterable<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060065E8 RID: 26088
		T GetAt(uint index);

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060065E9 RID: 26089
		uint Size { get; }

		// Token: 0x060065EA RID: 26090
		bool IndexOf(T value, out uint index);

		// Token: 0x060065EB RID: 26091
		uint GetMany(uint startIndex, [Out] T[] items);
	}
}
