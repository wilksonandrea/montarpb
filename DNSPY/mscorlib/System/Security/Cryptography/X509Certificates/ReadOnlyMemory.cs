using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D5 RID: 725
	internal struct ReadOnlyMemory<T>
	{
		// Token: 0x0600259D RID: 9629 RVA: 0x0008912E File Offset: 0x0008732E
		public ReadOnlyMemory(ArraySegment<T> segment)
		{
			this._Segment = segment;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x00089138 File Offset: 0x00087338
		public ReadOnlyMemory(T[] array, int offset, int count)
		{
			this = new ReadOnlyMemory<T>((array != null || offset != 0 || count != 0) ? new ArraySegment<T>(array, offset, count) : default(ArraySegment<T>));
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x00089168 File Offset: 0x00087368
		public ReadOnlyMemory(T[] array)
		{
			this = new ReadOnlyMemory<T>((array != null) ? new ArraySegment<T>(array) : default(ArraySegment<T>));
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x00089190 File Offset: 0x00087390
		public bool IsEmpty
		{
			get
			{
				return this._Segment.Count == 0;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x000891B0 File Offset: 0x000873B0
		public int Length
		{
			get
			{
				return this._Segment.Count;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000891CB File Offset: 0x000873CB
		public ReadOnlySpan<T> Span
		{
			get
			{
				return new ReadOnlySpan<T>(this._Segment);
			}
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000891D8 File Offset: 0x000873D8
		public ReadOnlyMemory<T> Slice(int start)
		{
			if (start < 0)
			{
				throw new InvalidOperationException();
			}
			return new ReadOnlyMemory<T>(this._Segment.Array, this._Segment.Offset + start, this._Segment.Count - start);
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x00089224 File Offset: 0x00087424
		public ReadOnlyMemory<T> Slice(int start, int length)
		{
			if (start < 0)
			{
				throw new InvalidOperationException();
			}
			if (length > this._Segment.Count - start)
			{
				throw new InvalidOperationException();
			}
			return new ReadOnlyMemory<T>(this._Segment.Array, this._Segment.Offset + start, length);
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x00089278 File Offset: 0x00087478
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

		// Token: 0x060025A6 RID: 9638 RVA: 0x000892EC File Offset: 0x000874EC
		public static implicit operator ReadOnlyMemory<T>(T[] array)
		{
			return new ReadOnlyMemory<T>(array);
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000892F4 File Offset: 0x000874F4
		public static implicit operator ArraySegment<T>(ReadOnlyMemory<T> memory)
		{
			return memory._Segment;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000892FC File Offset: 0x000874FC
		public static implicit operator ReadOnlyMemory<T>(ArraySegment<T> segment)
		{
			return new ReadOnlyMemory<T>(segment);
		}

		// Token: 0x04000E28 RID: 3624
		private readonly ArraySegment<T> _Segment;
	}
}
