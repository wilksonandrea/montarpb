using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DC RID: 2524
	internal sealed class ListToVectorAdapter
	{
		// Token: 0x06006438 RID: 25656 RVA: 0x001558DD File Offset: 0x00153ADD
		private ListToVectorAdapter()
		{
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x001558E8 File Offset: 0x00153AE8
		[SecurityCritical]
		internal T GetAt<T>(uint index)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count);
			T t;
			try
			{
				t = list[(int)index];
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, ex, "ArgumentOutOfRange_IndexOutOfRange");
			}
			return t;
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x00155938 File Offset: 0x00153B38
		[SecurityCritical]
		internal uint Size<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			return (uint)list.Count;
		}

		// Token: 0x0600643B RID: 25659 RVA: 0x00155954 File Offset: 0x00153B54
		[SecurityCritical]
		internal IReadOnlyList<T> GetView<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			IReadOnlyList<T> readOnlyList = list as IReadOnlyList<T>;
			if (readOnlyList == null)
			{
				readOnlyList = new ReadOnlyCollection<T>(list);
			}
			return readOnlyList;
		}

		// Token: 0x0600643C RID: 25660 RVA: 0x0015597C File Offset: 0x00153B7C
		[SecurityCritical]
		internal bool IndexOf<T>(T value, out uint index)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			int num = list.IndexOf(value);
			if (-1 == num)
			{
				index = 0U;
				return false;
			}
			index = (uint)num;
			return true;
		}

		// Token: 0x0600643D RID: 25661 RVA: 0x001559A8 File Offset: 0x00153BA8
		[SecurityCritical]
		internal void SetAt<T>(uint index, T value)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count);
			try
			{
				list[(int)index] = value;
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, ex, "ArgumentOutOfRange_IndexOutOfRange");
			}
		}

		// Token: 0x0600643E RID: 25662 RVA: 0x001559F4 File Offset: 0x00153BF4
		[SecurityCritical]
		internal void InsertAt<T>(uint index, T value)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count + 1);
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

		// Token: 0x0600643F RID: 25663 RVA: 0x00155A40 File Offset: 0x00153C40
		[SecurityCritical]
		internal void RemoveAt<T>(uint index)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			ListToVectorAdapter.EnsureIndexInt32(index, list.Count);
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

		// Token: 0x06006440 RID: 25664 RVA: 0x00155A88 File Offset: 0x00153C88
		[SecurityCritical]
		internal void Append<T>(T value)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			list.Add(value);
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x00155AA4 File Offset: 0x00153CA4
		[SecurityCritical]
		internal void RemoveAtEnd<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			if (list.Count == 0)
			{
				Exception ex = new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRemoveLastFromEmptyCollection"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			uint count = (uint)list.Count;
			this.RemoveAt<T>(count - 1U);
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x00155AF0 File Offset: 0x00153CF0
		[SecurityCritical]
		internal void Clear<T>()
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			list.Clear();
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x00155B0C File Offset: 0x00153D0C
		[SecurityCritical]
		internal uint GetMany<T>(uint startIndex, T[] items)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			return ListToVectorAdapter.GetManyHelper<T>(list, startIndex, items);
		}

		// Token: 0x06006444 RID: 25668 RVA: 0x00155B28 File Offset: 0x00153D28
		[SecurityCritical]
		internal void ReplaceAll<T>(T[] items)
		{
			IList<T> list = JitHelpers.UnsafeCast<IList<T>>(this);
			list.Clear();
			if (items != null)
			{
				foreach (T t in items)
				{
					list.Add(t);
				}
			}
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x00155B64 File Offset: 0x00153D64
		private static void EnsureIndexInt32(uint index, int listCapacity)
		{
			if (2147483647U <= index || index >= (uint)listCapacity)
			{
				Exception ex = new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x00155BA0 File Offset: 0x00153DA0
		private static uint GetManyHelper<T>(IList<T> sourceList, uint startIndex, T[] items)
		{
			if ((ulong)startIndex == (ulong)((long)sourceList.Count))
			{
				return 0U;
			}
			ListToVectorAdapter.EnsureIndexInt32(startIndex, sourceList.Count);
			if (items == null)
			{
				return 0U;
			}
			uint num = Math.Min((uint)items.Length, (uint)(sourceList.Count - (int)startIndex));
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				items[(int)num2] = sourceList[(int)(num2 + startIndex)];
			}
			if (typeof(T) == typeof(string))
			{
				string[] array = items as string[];
				uint num3 = num;
				while ((ulong)num3 < (ulong)((long)items.Length))
				{
					array[(int)num3] = string.Empty;
					num3 += 1U;
				}
			}
			return num;
		}
	}
}
