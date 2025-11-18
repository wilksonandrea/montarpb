using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009DF RID: 2527
	internal sealed class BindableVectorToCollectionAdapter
	{
		// Token: 0x0600645F RID: 25695 RVA: 0x00156020 File Offset: 0x00154220
		private BindableVectorToCollectionAdapter()
		{
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x00156028 File Offset: 0x00154228
		[SecurityCritical]
		internal int Count()
		{
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint size = bindableVector.Size;
			if (2147483647U < size)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			return (int)size;
		}

		// Token: 0x06006461 RID: 25697 RVA: 0x0015605C File Offset: 0x0015425C
		[SecurityCritical]
		internal bool IsSynchronized()
		{
			return false;
		}

		// Token: 0x06006462 RID: 25698 RVA: 0x0015605F File Offset: 0x0015425F
		[SecurityCritical]
		internal object SyncRoot()
		{
			return this;
		}

		// Token: 0x06006463 RID: 25699 RVA: 0x00156064 File Offset: 0x00154264
		[SecurityCritical]
		internal void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			int lowerBound = array.GetLowerBound(0);
			int num = this.Count();
			int length = array.GetLength(0);
			if (arrayIndex < lowerBound)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (num > length - (arrayIndex - lowerBound))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			if (arrayIndex - lowerBound > length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
			}
			IBindableVector bindableVector = JitHelpers.UnsafeCast<IBindableVector>(this);
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				array.SetValue(bindableVector.GetAt(num2), (long)((ulong)num2 + (ulong)((long)arrayIndex)));
				num2 += 1U;
			}
		}
	}
}
