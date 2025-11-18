using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D3 RID: 2515
	internal sealed class EnumerableToIterableAdapter
	{
		// Token: 0x06006405 RID: 25605 RVA: 0x00154E87 File Offset: 0x00153087
		private EnumerableToIterableAdapter()
		{
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x00154E90 File Offset: 0x00153090
		[SecurityCritical]
		internal IIterator<T> First_Stub<T>()
		{
			IEnumerable<T> enumerable = JitHelpers.UnsafeCast<IEnumerable<T>>(this);
			return new EnumeratorToIteratorAdapter<T>(enumerable.GetEnumerator());
		}
	}
}
