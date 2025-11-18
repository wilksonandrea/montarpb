using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DE RID: 2526
	internal sealed class BindableVectorToListAdapter
	{
		// Token: 0x0600644F RID: 25679 RVA: 0x00155D5A File Offset: 0x00153F5A
		private BindableVectorToListAdapter()
		{
		}

		// Token: 0x06006450 RID: 25680 RVA: 0x00155D64 File Offset: 0x00153F64
		[SecurityCritical]
		internal object Indexer_Get(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			return BindableVectorToListAdapter.GetAt(bindableVector, (uint)index);
		}

		// Token: 0x06006451 RID: 25681 RVA: 0x00155D90 File Offset: 0x00153F90
		[SecurityCritical]
		internal void Indexer_Set(int index, object value)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			BindableVectorToListAdapter.SetAt(bindableVector, (uint)index, value);
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x00155DBC File Offset: 0x00153FBC
		[SecurityCritical]
		internal int Add(object value)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			bindableVector.Append(value);
			uint size = bindableVector.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)(size - 1U);
		}

		// Token: 0x06006453 RID: 25683 RVA: 0x00155DFC File Offset: 0x00153FFC
		[SecurityCritical]
		internal bool Contains(object item)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num;
			return bindableVector.IndexOf(item, out num);
		}

		// Token: 0x06006454 RID: 25684 RVA: 0x00155E1C File Offset: 0x0015401C
		[SecurityCritical]
		internal void Clear()
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			bindableVector.Clear();
		}

		// Token: 0x06006455 RID: 25685 RVA: 0x00155E36 File Offset: 0x00154036
		[SecurityCritical]
		internal bool IsFixedSize()
		{
			return false;
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x00155E39 File Offset: 0x00154039
		[SecurityCritical]
		internal bool IsReadOnly()
		{
			return false;
		}

		// Token: 0x06006457 RID: 25687 RVA: 0x00155E3C File Offset: 0x0015403C
		[SecurityCritical]
		internal int IndexOf(object item)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num;
			if (!bindableVector.IndexOf(item, out num))
			{
				return -1;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)num;
		}

		// Token: 0x06006458 RID: 25688 RVA: 0x00155E78 File Offset: 0x00154078
		[SecurityCritical]
		internal void Insert(int index, object item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			BindableVectorToListAdapter.InsertAtHelper(bindableVector, (uint)index, item);
		}

		// Token: 0x06006459 RID: 25689 RVA: 0x00155EA4 File Offset: 0x001540A4
		[SecurityCritical]
		internal void Remove(object item)
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num;
			bool flag = bindableVector.IndexOf(item, out num);
			if (flag)
			{
				if (2147483647U < num)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
				}
				BindableVectorToListAdapter.RemoveAtHelper(bindableVector, num);
			}
		}

		// Token: 0x0600645A RID: 25690 RVA: 0x00155EE4 File Offset: 0x001540E4
		[SecurityCritical]
		internal void RemoveAt(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			BindableVectorToListAdapter.RemoveAtHelper(bindableVector, (uint)index);
		}

		// Token: 0x0600645B RID: 25691 RVA: 0x00155F10 File Offset: 0x00154110
		private static object GetAt(IBindableVector _this, uint index)
		{
			object at;
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

		// Token: 0x0600645C RID: 25692 RVA: 0x00155F54 File Offset: 0x00154154
		private static void SetAt(IBindableVector _this, uint index, object value)
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

		// Token: 0x0600645D RID: 25693 RVA: 0x00155F98 File Offset: 0x00154198
		private static void InsertAtHelper(IBindableVector _this, uint index, object item)
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

		// Token: 0x0600645E RID: 25694 RVA: 0x00155FDC File Offset: 0x001541DC
		private static void RemoveAtHelper(IBindableVector _this, uint index)
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
