using System;
using System.Collections;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E1 RID: 2529
	internal sealed class ListToBindableVectorViewAdapter : IBindableVectorView, IBindableIterable
	{
		// Token: 0x06006470 RID: 25712 RVA: 0x00156377 File Offset: 0x00154577
		internal ListToBindableVectorViewAdapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			this.list = list;
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x00156394 File Offset: 0x00154594
		private static void EnsureIndexInt32(uint index, int listCapacity)
		{
			if (2147483647U <= index || index >= (uint)listCapacity)
			{
				Exception ex = new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x001563D0 File Offset: 0x001545D0
		public IBindableIterator First()
		{
			IEnumerator enumerator = this.list.GetEnumerator();
			return new EnumeratorToIteratorAdapter<object>(new EnumerableToBindableIterableAdapter.NonGenericToGenericEnumerator(enumerator));
		}

		// Token: 0x06006473 RID: 25715 RVA: 0x001563F4 File Offset: 0x001545F4
		public object GetAt(uint index)
		{
			ListToBindableVectorViewAdapter.EnsureIndexInt32(index, this.list.Count);
			object obj;
			try
			{
				obj = this.list[(int)index];
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, ex, "ArgumentOutOfRange_IndexOutOfRange");
			}
			return obj;
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06006474 RID: 25716 RVA: 0x00156444 File Offset: 0x00154644
		public uint Size
		{
			get
			{
				return (uint)this.list.Count;
			}
		}

		// Token: 0x06006475 RID: 25717 RVA: 0x00156454 File Offset: 0x00154654
		public bool IndexOf(object value, out uint index)
		{
			int num = this.list.IndexOf(value);
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x04002CF6 RID: 11510
		private readonly IList list;
	}
}
