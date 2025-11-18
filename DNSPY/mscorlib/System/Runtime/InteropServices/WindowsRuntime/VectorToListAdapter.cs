using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D6 RID: 2518
	internal sealed class VectorToListAdapter
	{
		// Token: 0x0600640F RID: 25615 RVA: 0x0015500D File Offset: 0x0015320D
		private VectorToListAdapter()
		{
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x00155018 File Offset: 0x00153218
		[SecurityCritical]
		internal T Indexer_Get<T>(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			return VectorToListAdapter.GetAt<T>(vector, (uint)index);
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x00155044 File Offset: 0x00153244
		[SecurityCritical]
		internal void Indexer_Set<T>(int index, T value)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			VectorToListAdapter.SetAt<T>(vector, (uint)index, value);
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x00155070 File Offset: 0x00153270
		[SecurityCritical]
		internal int IndexOf<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint num;
			if (!vector.IndexOf(item, out num))
			{
				return -1;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)num;
		}

		// Token: 0x06006413 RID: 25619 RVA: 0x001550AC File Offset: 0x001532AC
		[SecurityCritical]
		internal void Insert<T>(int index, T item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			VectorToListAdapter.InsertAtHelper<T>(vector, (uint)index, item);
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x001550D8 File Offset: 0x001532D8
		[SecurityCritical]
		internal void RemoveAt<T>(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			VectorToListAdapter.RemoveAtHelper<T>(vector, (uint)index);
		}

		// Token: 0x06006415 RID: 25621 RVA: 0x00155104 File Offset: 0x00153304
		internal static T GetAt<T>(IVector<T> _this, uint index)
		{
			T at;
			try
			{
				at = _this.GetAt(index);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
			return at;
		}

		// Token: 0x06006416 RID: 25622 RVA: 0x00155148 File Offset: 0x00153348
		private static void SetAt<T>(IVector<T> _this, uint index, T value)
		{
			try
			{
				_this.SetAt(index, value);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
		}

		// Token: 0x06006417 RID: 25623 RVA: 0x0015518C File Offset: 0x0015338C
		private static void InsertAtHelper<T>(IVector<T> _this, uint index, T item)
		{
			try
			{
				_this.InsertAt(index, item);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
		}

		// Token: 0x06006418 RID: 25624 RVA: 0x001551D0 File Offset: 0x001533D0
		internal static void RemoveAtHelper<T>(IVector<T> _this, uint index)
		{
			try
			{
				_this.RemoveAt(index);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				throw;
			}
		}
	}
}
