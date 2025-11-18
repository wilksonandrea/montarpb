using System;

namespace System.Text
{
	// Token: 0x02000A61 RID: 2657
	[Serializable]
	internal sealed class InternalDecoderBestFitFallback : DecoderFallback
	{
		// Token: 0x06006786 RID: 26502 RVA: 0x0015DB01 File Offset: 0x0015BD01
		internal InternalDecoderBestFitFallback(Encoding encoding)
		{
			this.encoding = encoding;
			this.bIsMicrosoftBestFitFallback = true;
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x0015DB1F File Offset: 0x0015BD1F
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalDecoderBestFitFallbackBuffer(this);
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06006788 RID: 26504 RVA: 0x0015DB27 File Offset: 0x0015BD27
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x0015DB2C File Offset: 0x0015BD2C
		public override bool Equals(object value)
		{
			InternalDecoderBestFitFallback internalDecoderBestFitFallback = value as InternalDecoderBestFitFallback;
			return internalDecoderBestFitFallback != null && this.encoding.CodePage == internalDecoderBestFitFallback.encoding.CodePage;
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x0015DB5D File Offset: 0x0015BD5D
		public override int GetHashCode()
		{
			return this.encoding.CodePage;
		}

		// Token: 0x04002E48 RID: 11848
		internal Encoding encoding;

		// Token: 0x04002E49 RID: 11849
		internal char[] arrayBestFit;

		// Token: 0x04002E4A RID: 11850
		internal char cReplacement = '?';
	}
}
