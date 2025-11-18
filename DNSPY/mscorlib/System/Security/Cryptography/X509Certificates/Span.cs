using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D4 RID: 724
	internal struct Span<T>
	{
		// Token: 0x0600258A RID: 9610 RVA: 0x00088E18 File Offset: 0x00087018
		public Span(ArraySegment<T> segment)
		{
			this._Segment = segment;
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00088E24 File Offset: 0x00087024
		public Span(T[] array, int offset, int count)
		{
			this = new Span<T>((array != null || offset != 0 || count != 0) ? new ArraySegment<T>(array, offset, count) : default(ArraySegment<T>));
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00088E54 File Offset: 0x00087054
		public Span(T[] array)
		{
			this = new Span<T>((array != null) ? new ArraySegment<T>(array) : default(ArraySegment<T>));
		}

		// Token: 0x170004A5 RID: 1189
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this._Segment.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._Segment.Array[index + this._Segment.Offset];
			}
			set
			{
				if (index < 0 || index >= this._Segment.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._Segment.Array[index + this._Segment.Offset] = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x00088EF4 File Offset: 0x000870F4
		public bool IsEmpty
		{
			get
			{
				return this._Segment.Count == 0;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x00088F04 File Offset: 0x00087104
		public int Length
		{
			get
			{
				return this._Segment.Count;
			}
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x00088F11 File Offset: 0x00087111
		public Span<T> Slice(int start)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new Span<T>(this._Segment.Array, this._Segment.Offset + start, this._Segment.Count - start);
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00088F47 File Offset: 0x00087147
		public Span<T> Slice(int start, int length)
		{
			if (start < 0 || length > this._Segment.Count - start)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new Span<T>(this._Segment.Array, this._Segment.Offset + start, length);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x00088F84 File Offset: 0x00087184
		public void Fill(T value)
		{
			for (int i = this._Segment.Offset; i < this._Segment.Count - this._Segment.Offset; i++)
			{
				this._Segment.Array[i] = value;
			}
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x00088FD0 File Offset: 0x000871D0
		public void Clear()
		{
			for (int i = this._Segment.Offset; i < this._Segment.Count - this._Segment.Offset; i++)
			{
				this._Segment.Array[i] = default(T);
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x00089024 File Offset: 0x00087224
		public T[] ToArray()
		{
			T[] array = new T[this._Segment.Count];
			if (!this.IsEmpty)
			{
				Array.Copy(this._Segment.Array, this._Segment.Offset, array, 0, this._Segment.Count);
			}
			return array;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x00089074 File Offset: 0x00087274
		public void CopyTo(Span<T> destination)
		{
			if (destination.Length < this.Length)
			{
				throw new InvalidOperationException("Destination too short");
			}
			if (!this.IsEmpty)
			{
				ArraySegment<T> arraySegment = destination.DangerousGetArraySegment();
				Array.Copy(this._Segment.Array, this._Segment.Offset, arraySegment.Array, arraySegment.Offset, this._Segment.Count);
			}
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000890E0 File Offset: 0x000872E0
		public bool Overlaps(ReadOnlySpan<T> destination, out int elementOffset)
		{
			return this.Overlaps(destination, out elementOffset);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x00089102 File Offset: 0x00087302
		public ArraySegment<T> DangerousGetArraySegment()
		{
			return this._Segment;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0008910A File Offset: 0x0008730A
		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x00089112 File Offset: 0x00087312
		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._Segment);
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0008911F File Offset: 0x0008731F
		public T[] DangerousGetArrayForPinning()
		{
			return this._Segment.Array;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0008912C File Offset: 0x0008732C
		// Note: this type is marked as 'beforefieldinit'.
		static Span()
		{
		}

		// Token: 0x04000E26 RID: 3622
		public static readonly Span<T> Empty;

		// Token: 0x04000E27 RID: 3623
		private ArraySegment<T> _Segment;
	}
}
