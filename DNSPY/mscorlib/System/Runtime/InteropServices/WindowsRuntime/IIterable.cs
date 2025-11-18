using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A17 RID: 2583
	[Guid("faa585ea-6214-4217-afda-7f46de5869b3")]
	[ComImport]
	internal interface IIterable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060065C7 RID: 26055
		IIterator<T> First();
	}
}
