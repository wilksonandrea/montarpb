using System;

namespace System.Text
{
	// Token: 0x02000A6C RID: 2668
	[Serializable]
	internal class InternalEncoderBestFitFallback : EncoderFallback
	{
		// Token: 0x060067E2 RID: 26594 RVA: 0x0015EDA1 File Offset: 0x0015CFA1
		internal InternalEncoderBestFitFallback(Encoding encoding)
		{
			this.encoding = encoding;
			this.bIsMicrosoftBestFitFallback = true;
		}

		// Token: 0x060067E3 RID: 26595 RVA: 0x0015EDB7 File Offset: 0x0015CFB7
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalEncoderBestFitFallbackBuffer(this);
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x060067E4 RID: 26596 RVA: 0x0015EDBF File Offset: 0x0015CFBF
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060067E5 RID: 26597 RVA: 0x0015EDC4 File Offset: 0x0015CFC4
		public override bool Equals(object value)
		{
			InternalEncoderBestFitFallback internalEncoderBestFitFallback = value as InternalEncoderBestFitFallback;
			return internalEncoderBestFitFallback != null && this.encoding.CodePage == internalEncoderBestFitFallback.encoding.CodePage;
		}

		// Token: 0x060067E6 RID: 26598 RVA: 0x0015EDF5 File Offset: 0x0015CFF5
		public override int GetHashCode()
		{
			return this.encoding.CodePage;
		}

		// Token: 0x04002E63 RID: 11875
		internal Encoding encoding;

		// Token: 0x04002E64 RID: 11876
		internal char[] arrayBestFit;
	}
}
