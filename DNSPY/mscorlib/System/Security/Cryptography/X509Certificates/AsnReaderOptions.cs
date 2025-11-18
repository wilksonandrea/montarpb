using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B6 RID: 694
	internal struct AsnReaderOptions
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000852AF File Offset: 0x000834AF
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x000852C5 File Offset: 0x000834C5
		public int UtcTimeTwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == 0)
				{
					return 2049;
				}
				return (int)this._twoDigitYearMax;
			}
			set
			{
				if (value < 1 || value > 9999)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._twoDigitYearMax = (ushort)value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x000852E6 File Offset: 0x000834E6
		// (set) Token: 0x060024D7 RID: 9431 RVA: 0x000852EE File Offset: 0x000834EE
		public bool SkipSetSortOrderVerification
		{
			get
			{
				return this._skipSetSortOrderVerification;
			}
			set
			{
				this._skipSetSortOrderVerification = value;
			}
		}

		// Token: 0x04000DC6 RID: 3526
		private const int DefaultTwoDigitMax = 2049;

		// Token: 0x04000DC7 RID: 3527
		private ushort _twoDigitYearMax;

		// Token: 0x04000DC8 RID: 3528
		private bool _skipSetSortOrderVerification;
	}
}
