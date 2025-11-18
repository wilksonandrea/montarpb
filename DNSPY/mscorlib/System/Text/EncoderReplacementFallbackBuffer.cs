using System;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A74 RID: 2676
	public sealed class EncoderReplacementFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x0600681F RID: 26655 RVA: 0x0015F6A3 File Offset: 0x0015D8A3
		public EncoderReplacementFallbackBuffer(EncoderReplacementFallback fallback)
		{
			this.strDefault = fallback.DefaultString + fallback.DefaultString;
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x0015F6D0 File Offset: 0x0015D8D0
		public override bool Fallback(char charUnknown, int index)
		{
			if (this.fallbackCount >= 1)
			{
				if (char.IsHighSurrogate(charUnknown) && this.fallbackCount >= 0 && char.IsLowSurrogate(this.strDefault[this.fallbackIndex + 1]))
				{
					base.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknown, this.strDefault[this.fallbackIndex + 1]));
				}
				base.ThrowLastCharRecursive((int)charUnknown);
			}
			this.fallbackCount = this.strDefault.Length / 2;
			this.fallbackIndex = -1;
			return this.fallbackCount != 0;
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x0015F75C File Offset: 0x0015D95C
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 55296, 56319 }));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 56320, 57343 }));
			}
			if (this.fallbackCount >= 1)
			{
				base.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknownHigh, charUnknownLow));
			}
			this.fallbackCount = this.strDefault.Length;
			this.fallbackIndex = -1;
			return this.fallbackCount != 0;
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x0015F81C File Offset: 0x0015DA1C
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

		// Token: 0x06006823 RID: 26659 RVA: 0x0015F877 File Offset: 0x0015DA77
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

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06006824 RID: 26660 RVA: 0x0015F8AA File Offset: 0x0015DAAA
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

		// Token: 0x06006825 RID: 26661 RVA: 0x0015F8BD File Offset: 0x0015DABD
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.fallbackCount = -1;
			this.fallbackIndex = 0;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x04002E7B RID: 11899
		private string strDefault;

		// Token: 0x04002E7C RID: 11900
		private int fallbackCount = -1;

		// Token: 0x04002E7D RID: 11901
		private int fallbackIndex = -1;
	}
}
