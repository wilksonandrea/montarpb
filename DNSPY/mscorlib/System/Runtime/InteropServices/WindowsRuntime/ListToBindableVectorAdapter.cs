using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E0 RID: 2528
	internal sealed class ListToBindableVectorAdapter
	{
		// Token: 0x06006464 RID: 25700 RVA: 0x00156119 File Offset: 0x00154319
		private ListToBindableVectorAdapter()
		{
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x00156124 File Offset: 0x00154324
		[SecurityCritical]
		internal object GetAt(uint index)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
			object obj;
			try
			{
				obj = list[(int)index];
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, ex, "ArgumentOutOfRange_IndexOutOfRange");
			}
			return obj;
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x00156174 File Offset: 0x00154374
		[SecurityCritical]
		internal uint Size()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			return (uint)list.Count;
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x00156190 File Offset: 0x00154390
		[SecurityCritical]
		internal IBindableVectorView GetView()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			return new ListToBindableVectorViewAdapter(list);
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x001561AC File Offset: 0x001543AC
		[SecurityCritical]
		internal bool IndexOf(object value, out uint index)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			int num = list.IndexOf(value);
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x001561D8 File Offset: 0x001543D8
		[SecurityCritical]
		internal void SetAt(uint index, object value)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
			try
			{
				list[(int)index] = value;
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, ex, "ArgumentOutOfRange_IndexOutOfRange");
			}
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x00156224 File Offset: 0x00154424
		[SecurityCritical]
		internal void InsertAt(uint index, object value)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count + 1);
			try
			{
				list.Insert((int)index, value);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				ex.SetErrorCode(-2147483637);
				throw;
			}
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x00156270 File Offset: 0x00154470
		[SecurityCritical]
		internal void RemoveAt(uint index)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
			try
			{
				list.RemoveAt((int)index);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				ex.SetErrorCode(-2147483637);
				throw;
			}
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x001562B8 File Offset: 0x001544B8
		[SecurityCritical]
		internal void Append(object value)
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			list.Add(value);
		}

		// Token: 0x0600646D RID: 25709 RVA: 0x001562D4 File Offset: 0x001544D4
		[SecurityCritical]
		internal void RemoveAtEnd()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			if (list.Count == 0)
			{
				Exception ex = new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRemoveLastFromEmptyCollection"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			uint count = (uint)list.Count;
			this.RemoveAt(count - 1U);
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x00156320 File Offset: 0x00154520
		[SecurityCritical]
		internal void Clear()
		{
			IList list = JitHelpers.UnsafeCast<IList>(this);
			list.Clear();
		}

		// Token: 0x0600646F RID: 25711 RVA: 0x0015633C File Offset: 0x0015453C
		private static void EnsureIndexInt32(uint index, int listCapacity)
		{
			if (2147483647U <= index || index >= (uint)listCapacity)
			{
				Exception ex = new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}
	}
}
