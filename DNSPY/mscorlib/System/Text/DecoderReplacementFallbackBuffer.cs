using System;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A69 RID: 2665
	public sealed class DecoderReplacementFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x060067BF RID: 26559 RVA: 0x0015E303 File Offset: 0x0015C503
		public DecoderReplacementFallbackBuffer(DecoderReplacementFallback fallback)
		{
			this.strDefault = fallback.DefaultString;
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x0015E325 File Offset: 0x0015C525
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			if (this.fallbackCount >= 1)
			{
				base.ThrowLastBytesRecursive(bytesUnknown);
			}
			if (this.strDefault.Length == 0)
			{
				return false;
			}
			this.fallbackCount = this.strDefault.Length;
			this.fallbackIndex = -1;
			return true;
		}

		// Token: 0x060067C1 RID: 26561 RVA: 0x0015E360 File Offset: 0x0015C560
		public override char GetNextChar()
		{
			this.fallbackCount--;
			this.fallbackIndex++;
			if (this.fallbackCount < 0)
			{
				return '\0';
			}
			if (this.fallbackCount == 2147483647)
			{
				this.fallbackCount = -1;
				return '\0';
			}
			return this.strDefault[this.fallbackIndex];
		}

		// Token: 0x060067C2 RID: 26562 RVA: 0x0015E3BB File Offset: 0x0015C5BB
		public override bool MovePrevious()
		{
			if (this.fallbackCount >= -1 && this.fallbackIndex >= 0)
			{
				this.fallbackIndex--;
				this.fallbackCount++;
				return true;
			}
			return false;
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x060067C3 RID: 26563 RVA: 0x0015E3EE File Offset: 0x0015C5EE
		public override int Remaining
		{
			get
			{
				if (this.fallbackCount >= 0)
				{
					return this.fallbackCount;
				}
				return 0;
			}
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x0015E401 File Offset: 0x0015C601
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.fallbackCount = -1;
			this.fallbackIndex = -1;
			this.byteStart = null;
		}

		// Token: 0x060067C5 RID: 26565 RVA: 0x0015E419 File Offset: 0x0015C619
		[SecurityCritical]
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return this.strDefault.Length;
		}

		// Token: 0x04002E59 RID: 11865
		private string strDefault;

		// Token: 0x04002E5A RID: 11866
		private int fallbackCount = -1;

		// Token: 0x04002E5B RID: 11867
		private int fallbackIndex = -1;
	}
}
