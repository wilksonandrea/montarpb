using System;
using System.Collections;
using System.Collections.Generic;

namespace System
{
	// Token: 0x02000057 RID: 87
	[__DynamicallyInvokable]
	[Serializable]
	public struct ArraySegment<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x0600031B RID: 795 RVA: 0x00007D26 File Offset: 0x00005F26
		[__DynamicallyInvokable]
		public ArraySegment(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this._array = array;
			this._offset = 0;
			this._count = array.Length;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00007D50 File Offset: 0x00005F50
		[__DynamicallyInvokable]
		public ArraySegment(T[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			this._array = array;
			this._offset = offset;
			this._count = count;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00007DCA File Offset: 0x00005FCA
		[__DynamicallyInvokable]
		public T[] Array
		{
			[__DynamicallyInvokable]
			get
			{
				return this._array;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00007DD2 File Offset: 0x00005FD2
		[__DynamicallyInvokable]
		public int Offset
		{
			[__DynamicallyInvokable]
			get
			{
				return this._offset;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00007DDA File Offset: 0x00005FDA
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._count;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00007DE2 File Offset: 0x00005FE2
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this._array != null)
			{
				return this._array.GetHashCode() ^ this._offset ^ this._count;
			}
			return 0;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00007E07 File Offset: 0x00006007
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is ArraySegment<T> && this.Equals((ArraySegment<T>)obj);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00007E1F File Offset: 0x0000601F
		[__DynamicallyInvokable]
		public bool Equals(ArraySegment<T> obj)
		{
			return obj._array == this._array && obj._offset == this._offset && obj._count == this._count;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00007E4D File Offset: 0x0000604D
		[__DynamicallyInvokable]
		public static bool operator ==(ArraySegment<T> a, ArraySegment<T> b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00007E57 File Offset: 0x00006057
		[__DynamicallyInvokable]
		public static bool operator !=(ArraySegment<T> a, ArraySegment<T> b)
		{
			return !(a == b);
		}

		// Token: 0x1700003B RID: 59
		[__DynamicallyInvokable]
		T IList<T>.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._array[this._offset + index];
			}
			[__DynamicallyInvokable]
			set
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._array[this._offset + index] = value;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00007F08 File Offset: 0x00006108
		[__DynamicallyInvokable]
		int IList<T>.IndexOf(T item)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			int num = System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			if (num < 0)
			{
				return -1;
			}
			return num - this._offset;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00007F54 File Offset: 0x00006154
		[__DynamicallyInvokable]
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00007F5B File Offset: 0x0000615B
		[__DynamicallyInvokable]
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700003C RID: 60
		[__DynamicallyInvokable]
		T IReadOnlyList<T>.this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._array == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
				}
				if (index < 0 || index >= this._count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._array[this._offset + index];
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00007FB4 File Offset: 0x000061B4
		[__DynamicallyInvokable]
		bool ICollection<T>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00007FB7 File Offset: 0x000061B7
		[__DynamicallyInvokable]
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00007FBE File Offset: 0x000061BE
		[__DynamicallyInvokable]
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00007FC8 File Offset: 0x000061C8
		[__DynamicallyInvokable]
		bool ICollection<T>.Contains(T item)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			int num = System.Array.IndexOf<T>(this._array, item, this._offset, this._count);
			return num >= 0;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000800D File Offset: 0x0000620D
		[__DynamicallyInvokable]
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			System.Array.Copy(this._array, this._offset, array, arrayIndex, this._count);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008040 File Offset: 0x00006240
		[__DynamicallyInvokable]
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00008047 File Offset: 0x00006247
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			return new ArraySegment<T>.ArraySegmentEnumerator(this);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000806C File Offset: 0x0000626C
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._array == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullArray"));
			}
			return new ArraySegment<T>.ArraySegmentEnumerator(this);
		}

		// Token: 0x040001F0 RID: 496
		private T[] _array;

		// Token: 0x040001F1 RID: 497
		private int _offset;

		// Token: 0x040001F2 RID: 498
		private int _count;

		// Token: 0x02000AC8 RID: 2760
		[Serializable]
		private sealed class ArraySegmentEnumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060069CB RID: 27083 RVA: 0x0016C8D0 File Offset: 0x0016AAD0
			internal ArraySegmentEnumerator(ArraySegment<T> arraySegment)
			{
				this._array = arraySegment._array;
				this._start = arraySegment._offset;
				this._end = this._start + arraySegment._count;
				this._current = this._start - 1;
			}

			// Token: 0x060069CC RID: 27084 RVA: 0x0016C91C File Offset: 0x0016AB1C
			public bool MoveNext()
			{
				if (this._current < this._end)
				{
					this._current++;
					return this._current < this._end;
				}
				return false;
			}

			// Token: 0x170011EB RID: 4587
			// (get) Token: 0x060069CD RID: 27085 RVA: 0x0016C94C File Offset: 0x0016AB4C
			public T Current
			{
				get
				{
					if (this._current < this._start)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._current >= this._end)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this._array[this._current];
				}
			}

			// Token: 0x170011EC RID: 4588
			// (get) Token: 0x060069CE RID: 27086 RVA: 0x0016C9A6 File Offset: 0x0016ABA6
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060069CF RID: 27087 RVA: 0x0016C9B3 File Offset: 0x0016ABB3
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x060069D0 RID: 27088 RVA: 0x0016C9C3 File Offset: 0x0016ABC3
			public void Dispose()
			{
			}

			// Token: 0x040030E0 RID: 12512
			private T[] _array;

			// Token: 0x040030E1 RID: 12513
			private int _start;

			// Token: 0x040030E2 RID: 12514
			private int _end;

			// Token: 0x040030E3 RID: 12515
			private int _current;
		}
	}
}
