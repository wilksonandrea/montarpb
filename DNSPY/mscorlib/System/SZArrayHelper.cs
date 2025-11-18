using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x02000056 RID: 86
	internal sealed class SZArrayHelper
	{
		// Token: 0x0600030D RID: 781 RVA: 0x00007BB9 File Offset: 0x00005DB9
		private SZArrayHelper()
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00007BC4 File Offset: 0x00005DC4
		[SecuritySafeCritical]
		internal IEnumerator<T> GetEnumerator<T>()
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			int num = array.Length;
			if (num != 0)
			{
				return new SZArrayHelper.SZGenericArrayEnumerator<T>(array, num);
			}
			return SZArrayHelper.SZGenericArrayEnumerator<T>.Empty;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00007BEC File Offset: 0x00005DEC
		[SecuritySafeCritical]
		private void CopyTo<T>(T[] array, int index)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			T[] array2 = JitHelpers.UnsafeCast<T[]>(this);
			Array.Copy(array2, 0, array, index, array2.Length);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00007C28 File Offset: 0x00005E28
		[SecuritySafeCritical]
		internal int get_Count<T>()
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			return array.Length;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00007C40 File Offset: 0x00005E40
		[SecuritySafeCritical]
		internal T get_Item<T>(int index)
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			if (index >= array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return array[index];
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00007C68 File Offset: 0x00005E68
		[SecuritySafeCritical]
		internal void set_Item<T>(int index, T value)
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			if (index >= array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			array[index] = value;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00007C8F File Offset: 0x00005E8F
		private void Add<T>(T value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00007CA0 File Offset: 0x00005EA0
		[SecuritySafeCritical]
		private bool Contains<T>(T value)
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			return Array.IndexOf<T>(array, value) != -1;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00007CC1 File Offset: 0x00005EC1
		private bool get_IsReadOnly<T>()
		{
			return true;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00007CC4 File Offset: 0x00005EC4
		private void Clear<T>()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ReadOnlyCollection"));
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00007CD8 File Offset: 0x00005ED8
		[SecuritySafeCritical]
		private int IndexOf<T>(T value)
		{
			T[] array = JitHelpers.UnsafeCast<T[]>(this);
			return Array.IndexOf<T>(array, value);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00007CF3 File Offset: 0x00005EF3
		private void Insert<T>(int index, T value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00007D04 File Offset: 0x00005F04
		private bool Remove<T>(T value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00007D15 File Offset: 0x00005F15
		private void RemoveAt<T>(int index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_FixedSizeCollection"));
		}

		// Token: 0x02000AC7 RID: 2759
		[Serializable]
		private sealed class SZGenericArrayEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060069C4 RID: 27076 RVA: 0x0016C806 File Offset: 0x0016AA06
			internal SZGenericArrayEnumerator(T[] array, int endIndex)
			{
				this._array = array;
				this._index = -1;
				this._endIndex = endIndex;
			}

			// Token: 0x060069C5 RID: 27077 RVA: 0x0016C823 File Offset: 0x0016AA23
			public bool MoveNext()
			{
				if (this._index < this._endIndex)
				{
					this._index++;
					return this._index < this._endIndex;
				}
				return false;
			}

			// Token: 0x170011E9 RID: 4585
			// (get) Token: 0x060069C6 RID: 27078 RVA: 0x0016C854 File Offset: 0x0016AA54
			public T Current
			{
				get
				{
					if (this._index < 0)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._index >= this._endIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this._array[this._index];
				}
			}

			// Token: 0x170011EA RID: 4586
			// (get) Token: 0x060069C7 RID: 27079 RVA: 0x0016C8A9 File Offset: 0x0016AAA9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060069C8 RID: 27080 RVA: 0x0016C8B6 File Offset: 0x0016AAB6
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x060069C9 RID: 27081 RVA: 0x0016C8BF File Offset: 0x0016AABF
			public void Dispose()
			{
			}

			// Token: 0x060069CA RID: 27082 RVA: 0x0016C8C1 File Offset: 0x0016AAC1
			// Note: this type is marked as 'beforefieldinit'.
			static SZGenericArrayEnumerator()
			{
			}

			// Token: 0x040030DC RID: 12508
			private T[] _array;

			// Token: 0x040030DD RID: 12509
			private int _index;

			// Token: 0x040030DE RID: 12510
			private int _endIndex;

			// Token: 0x040030DF RID: 12511
			internal static readonly SZArrayHelper.SZGenericArrayEnumerator<T> Empty = new SZArrayHelper.SZGenericArrayEnumerator<T>(null, -1);
		}
	}
}
