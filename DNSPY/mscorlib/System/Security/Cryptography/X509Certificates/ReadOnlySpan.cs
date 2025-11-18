using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D3 RID: 723
	internal struct ReadOnlySpan<T>
	{
		// Token: 0x0600257A RID: 9594 RVA: 0x00088B73 File Offset: 0x00086D73
		public ReadOnlySpan(ArraySegment<T> segment)
		{
			this._Segment = segment;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00088B7C File Offset: 0x00086D7C
		public ReadOnlySpan(T[] array, int offset, int count)
		{
			this = new ReadOnlySpan<T>((array != null || offset != 0 || count != 0) ? new ArraySegment<T>(array, offset, count) : default(ArraySegment<T>));
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x00088BAC File Offset: 0x00086DAC
		public ReadOnlySpan(T[] array)
		{
			this = new ReadOnlySpan<T>((array != null) ? new ArraySegment<T>(array) : default(ArraySegment<T>));
		}

		// Token: 0x170004A1 RID: 1185
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
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x00088C0F File Offset: 0x00086E0F
		public bool IsEmpty
		{
			get
			{
				return this._Segment.Count == 0;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x00088C1F File Offset: 0x00086E1F
		public bool IsNull
		{
			get
			{
				return this._Segment.Array == null;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x00088C2F File Offset: 0x00086E2F
		public int Length
		{
			get
			{
				return this._Segment.Count;
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00088C3C File Offset: 0x00086E3C
		public ReadOnlySpan<T> Slice(int start)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new ReadOnlySpan<T>(this._Segment.Array, this._Segment.Offset + start, this._Segment.Count - start);
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00088C72 File Offset: 0x00086E72
		public ReadOnlySpan<T> Slice(int start, int length)
		{
			if (start < 0)
			{
				throw new InvalidOperationException();
			}
			if (length > this._Segment.Count - start)
			{
				throw new InvalidOperationException();
			}
			return new ReadOnlySpan<T>(this._Segment.Array, this._Segment.Offset + start, length);
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x00088CB4 File Offset: 0x00086EB4
		public T[] ToArray()
		{
			T[] array = new T[this._Segment.Count];
			if (!this.IsEmpty)
			{
				Array.Copy(this._Segment.Array, this._Segment.Offset, array, 0, this._Segment.Count);
			}
			return array;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x00088D04 File Offset: 0x00086F04
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

		// Token: 0x06002585 RID: 9605 RVA: 0x00088D70 File Offset: 0x00086F70
		public bool Overlaps(ReadOnlySpan<T> destination)
		{
			int num;
			return this.Overlaps(destination, out num);
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x00088D88 File Offset: 0x00086F88
		public bool Overlaps(ReadOnlySpan<T> destination, out int elementOffset)
		{
			elementOffset = 0;
			if (this.IsEmpty || destination.IsEmpty)
			{
				return false;
			}
			if (this._Segment.Array != destination._Segment.Array)
			{
				return false;
			}
			elementOffset = destination._Segment.Offset - this._Segment.Offset;
			if (elementOffset >= this._Segment.Count || elementOffset <= -destination._Segment.Count)
			{
				elementOffset = 0;
				return false;
			}
			return true;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x00088E06 File Offset: 0x00087006
		public ArraySegment<T> DangerousGetArraySegment()
		{
			return this._Segment;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00088E0E File Offset: 0x0008700E
		public static implicit operator ReadOnlySpan<T>(T[] array)
		{
			return new ReadOnlySpan<T>(array);
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00088E16 File Offset: 0x00087016
		// Note: this type is marked as 'beforefieldinit'.
		static ReadOnlySpan()
		{
		}

		// Token: 0x04000E24 RID: 3620
		public static readonly Span<T> Empty;

		// Token: 0x04000E25 RID: 3621
		private ArraySegment<T> _Segment;
	}
}
