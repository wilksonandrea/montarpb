using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002D0 RID: 720
	internal struct Triple<T1, T2, T3>
	{
		// Token: 0x06002569 RID: 9577 RVA: 0x00088644 File Offset: 0x00086844
		internal Triple(T1 first, T2 second, T3 third)
		{
			this._first = first;
			this._second = second;
			this._third = third;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x0008865B File Offset: 0x0008685B
		public T1 Item1
		{
			get
			{
				return this._first;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x00088663 File Offset: 0x00086863
		public T2 Item2
		{
			get
			{
				return this._second;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x0008866B File Offset: 0x0008686B
		public T3 Item3
		{
			get
			{
				return this._third;
			}
		}

		// Token: 0x04000E1C RID: 3612
		private readonly T1 _first;

		// Token: 0x04000E1D RID: 3613
		private readonly T2 _second;

		// Token: 0x04000E1E RID: 3614
		private readonly T3 _third;
	}
}
