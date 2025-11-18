using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D4 RID: 2516
	internal sealed class EnumerableToBindableIterableAdapter
	{
		// Token: 0x06006407 RID: 25607 RVA: 0x00154EAF File Offset: 0x001530AF
		private EnumerableToBindableIterableAdapter()
		{
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x00154EB8 File Offset: 0x001530B8
		[SecurityCritical]
		internal IBindableIterator First_Stub()
		{
			IEnumerable enumerable = JitHelpers.UnsafeCast<IEnumerable>(this);
			return new EnumeratorToIteratorAdapter<object>(new EnumerableToBindableIterableAdapter.NonGenericToGenericEnumerator(enumerable.GetEnumerator()));
		}

		// Token: 0x02000CA5 RID: 3237
		internal sealed class NonGenericToGenericEnumerator : IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06007138 RID: 28984 RVA: 0x0018561D File Offset: 0x0018381D
			public NonGenericToGenericEnumerator(IEnumerator enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x17001369 RID: 4969
			// (get) Token: 0x06007139 RID: 28985 RVA: 0x0018562C File Offset: 0x0018382C
			public object Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x0600713A RID: 28986 RVA: 0x00185639 File Offset: 0x00183839
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x0600713B RID: 28987 RVA: 0x00185646 File Offset: 0x00183846
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x0600713C RID: 28988 RVA: 0x00185653 File Offset: 0x00183853
			public void Dispose()
			{
			}

			// Token: 0x0400388B RID: 14475
			private IEnumerator enumerator;
		}
	}
}
