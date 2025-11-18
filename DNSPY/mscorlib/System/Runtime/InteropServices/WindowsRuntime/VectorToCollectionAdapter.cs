using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D7 RID: 2519
	internal sealed class VectorToCollectionAdapter
	{
		// Token: 0x06006419 RID: 25625 RVA: 0x00155214 File Offset: 0x00153414
		private VectorToCollectionAdapter()
		{
		}

		// Token: 0x0600641A RID: 25626 RVA: 0x0015521C File Offset: 0x0015341C
		[SecurityCritical]
		internal int Count<T>()
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint size = vector.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)size;
		}

		// Token: 0x0600641B RID: 25627 RVA: 0x00155250 File Offset: 0x00153450
		[SecurityCritical]
		internal bool IsReadOnly<T>()
		{
			return false;
		}

		// Token: 0x0600641C RID: 25628 RVA: 0x00155254 File Offset: 0x00153454
		[SecurityCritical]
		internal void Add<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			vector.Append(item);
		}

		// Token: 0x0600641D RID: 25629 RVA: 0x00155270 File Offset: 0x00153470
		[SecurityCritical]
		internal void Clear<T>()
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			vector.Clear();
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x0015528C File Offset: 0x0015348C
		[SecurityCritical]
		internal bool Contains<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint num;
			return vector.IndexOf(item, out num);
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x001552AC File Offset: 0x001534AC
		[SecurityCritical]
		internal void CopyTo<T>(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length <= arrayIndex && this.Count<T>() > 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
			}
			if (array.Length - arrayIndex < this.Count<T>())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			int num = this.Count<T>();
			for (int i = 0; i < num; i++)
			{
				array[i + arrayIndex] = VectorToListAdapter.GetAt<T>(vector, (uint)i);
			}
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x0015533C File Offset: 0x0015353C
		[SecurityCritical]
		internal bool Remove<T>(T item)
		{
			IVector<T> vector = JitHelpers.UnsafeCast<IVector<T>>(this);
			uint num;
			if (!vector.IndexOf(item, out num))
			{
				return false;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			VectorToListAdapter.RemoveAtHelper<T>(vector, num);
			return true;
		}
	}
}
