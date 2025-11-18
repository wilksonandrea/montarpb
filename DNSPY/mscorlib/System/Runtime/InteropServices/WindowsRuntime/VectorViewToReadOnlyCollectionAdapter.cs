using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D8 RID: 2520
	internal sealed class VectorViewToReadOnlyCollectionAdapter
	{
		// Token: 0x06006421 RID: 25633 RVA: 0x0015537F File Offset: 0x0015357F
		private VectorViewToReadOnlyCollectionAdapter()
		{
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x00155388 File Offset: 0x00153588
		[SecurityCritical]
		internal int Count<T>()
		{
			IVectorView<T> vectorView = JitHelpers.UnsafeCast<IVectorView<T>>(this);
			uint size = vectorView.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)size;
		}
	}
}
