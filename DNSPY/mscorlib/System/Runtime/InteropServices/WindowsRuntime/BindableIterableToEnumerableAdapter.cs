using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F1 RID: 2545
	internal sealed class BindableIterableToEnumerableAdapter
	{
		// Token: 0x060064BC RID: 25788 RVA: 0x00156F59 File Offset: 0x00155159
		private BindableIterableToEnumerableAdapter()
		{
		}

		// Token: 0x060064BD RID: 25789 RVA: 0x00156F64 File Offset: 0x00155164
		[SecurityCritical]
		internal IEnumerator GetEnumerator_Stub()
		{
			IBindableIterable bindableIterable = JitHelpers.UnsafeCast<IBindableIterable>(this);
			return new IteratorToEnumeratorAdapter<object>(new BindableIterableToEnumerableAdapter.NonGenericToGenericIterator(bindableIterable.First()));
		}

		// Token: 0x02000CA6 RID: 3238
		private sealed class NonGenericToGenericIterator : IIterator<object>
		{
			// Token: 0x0600713D RID: 28989 RVA: 0x00185655 File Offset: 0x00183855
			public NonGenericToGenericIterator(IBindableIterator iterator)
			{
				this.iterator = iterator;
			}

			// Token: 0x1700136A RID: 4970
			// (get) Token: 0x0600713E RID: 28990 RVA: 0x00185664 File Offset: 0x00183864
			public object Current
			{
				get
				{
					return this.iterator.Current;
				}
			}

			// Token: 0x1700136B RID: 4971
			// (get) Token: 0x0600713F RID: 28991 RVA: 0x00185671 File Offset: 0x00183871
			public bool HasCurrent
			{
				get
				{
					return this.iterator.HasCurrent;
				}
			}

			// Token: 0x06007140 RID: 28992 RVA: 0x0018567E File Offset: 0x0018387E
			public bool MoveNext()
			{
				return this.iterator.MoveNext();
			}

			// Token: 0x06007141 RID: 28993 RVA: 0x0018568B File Offset: 0x0018388B
			public int GetMany(object[] items)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0400388C RID: 14476
			private IBindableIterator iterator;
		}
	}
}
