using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F0 RID: 2544
	internal sealed class IterableToEnumerableAdapter
	{
		// Token: 0x060064B9 RID: 25785 RVA: 0x00156EDF File Offset: 0x001550DF
		private IterableToEnumerableAdapter()
		{
		}

		// Token: 0x060064BA RID: 25786 RVA: 0x00156EE8 File Offset: 0x001550E8
		[SecurityCritical]
		internal IEnumerator<T> GetEnumerator_Stub<T>()
		{
			IIterable<T> iterable = JitHelpers.UnsafeCast<IIterable<T>>(this);
			return new IteratorToEnumeratorAdapter<T>(iterable.First());
		}

		// Token: 0x060064BB RID: 25787 RVA: 0x00156F08 File Offset: 0x00155108
		[SecurityCritical]
		internal IEnumerator<T> GetEnumerator_Variance_Stub<T>() where T : class
		{
			bool flag;
			Delegate targetForAmbiguousVariantCall = StubHelpers.GetTargetForAmbiguousVariantCall(this, typeof(IEnumerable<T>).TypeHandle.Value, out flag);
			if (targetForAmbiguousVariantCall != null)
			{
				return JitHelpers.UnsafeCast<GetEnumerator_Delegate<T>>(targetForAmbiguousVariantCall)();
			}
			if (flag)
			{
				return JitHelpers.UnsafeCast<IEnumerator<T>>(this.GetEnumerator_Stub<string>());
			}
			return this.GetEnumerator_Stub<T>();
		}
	}
}
