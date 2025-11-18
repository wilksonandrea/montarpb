using System;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A72 RID: 2674
	[__DynamicallyInvokable]
	public abstract class EncoderFallbackBuffer
	{
		// Token: 0x0600680C RID: 26636
		[__DynamicallyInvokable]
		public abstract bool Fallback(char charUnknown, int index);

		// Token: 0x0600680D RID: 26637
		[__DynamicallyInvokable]
		public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);

		// Token: 0x0600680E RID: 26638
		[__DynamicallyInvokable]
		public abstract char GetNextChar();

		// Token: 0x0600680F RID: 26639
		[__DynamicallyInvokable]
		public abstract bool MovePrevious();

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06006810 RID: 26640
		[__DynamicallyInvokable]
		public abstract int Remaining
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x06006811 RID: 26641 RVA: 0x0015F40C File Offset: 0x0015D60C
		[__DynamicallyInvokable]
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x06006812 RID: 26642 RVA: 0x0015F416 File Offset: 0x0015D616
		[SecurityCritical]
		internal void InternalReset()
		{
			this.charStart = null;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
			this.Reset();
		}

		// Token: 0x06006813 RID: 26643 RVA: 0x0015F434 File Offset: 0x0015D634
		[SecurityCritical]
		internal unsafe void InternalInitialize(char* charStart, char* charEnd, EncoderNLS encoder, bool setEncoder)
		{
			this.charStart = charStart;
			this.charEnd = charEnd;
			this.encoder = encoder;
			this.setEncoder = setEncoder;
			this.bUsedEncoder = false;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
		}

		// Token: 0x06006814 RID: 26644 RVA: 0x0015F468 File Offset: 0x0015D668
		internal char InternalGetNextChar()
		{
			char nextChar = this.GetNextChar();
			this.bFallingBack = nextChar > '\0';
			if (nextChar == '\0')
			{
				this.iRecursionCount = 0;
			}
			return nextChar;
		}

		// Token: 0x06006815 RID: 26645 RVA: 0x0015F494 File Offset: 0x0015D694
		[SecurityCritical]
		internal unsafe virtual bool InternalFallback(char ch, ref char* chars)
		{
			int num = (chars - this.charStart) / 2 - 1;
			if (char.IsHighSurrogate(ch))
			{
				if (chars >= this.charEnd)
				{
					if (this.encoder != null && !this.encoder.MustFlush)
					{
						if (this.setEncoder)
						{
							this.bUsedEncoder = true;
							this.encoder.charLeftOver = ch;
						}
						this.bFallingBack = false;
						return false;
					}
				}
				else
				{
					char c = (char)(*chars);
					if (char.IsLowSurrogate(c))
					{
						if (this.bFallingBack)
						{
							int num2 = this.iRecursionCount;
							this.iRecursionCount = num2 + 1;
							if (num2 > 250)
							{
								this.ThrowLastCharRecursive(char.ConvertToUtf32(ch, c));
							}
						}
						chars += 2;
						this.bFallingBack = this.Fallback(ch, c, num);
						return this.bFallingBack;
					}
				}
			}
			if (this.bFallingBack)
			{
				int num2 = this.iRecursionCount;
				this.iRecursionCount = num2 + 1;
				if (num2 > 250)
				{
					this.ThrowLastCharRecursive((int)ch);
				}
			}
			this.bFallingBack = this.Fallback(ch, num);
			return this.bFallingBack;
		}

		// Token: 0x06006816 RID: 26646 RVA: 0x0015F592 File Offset: 0x0015D792
		internal void ThrowLastCharRecursive(int charRecursive)
		{
			throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", new object[] { charRecursive }), "chars");
		}

		// Token: 0x06006817 RID: 26647 RVA: 0x0015F5B7 File Offset: 0x0015D7B7
		[__DynamicallyInvokable]
		protected EncoderFallbackBuffer()
		{
		}

		// Token: 0x04002E72 RID: 11890
		[SecurityCritical]
		internal unsafe char* charStart;

		// Token: 0x04002E73 RID: 11891
		[SecurityCritical]
		internal unsafe char* charEnd;

		// Token: 0x04002E74 RID: 11892
		internal EncoderNLS encoder;

		// Token: 0x04002E75 RID: 11893
		internal bool setEncoder;

		// Token: 0x04002E76 RID: 11894
		internal bool bUsedEncoder;

		// Token: 0x04002E77 RID: 11895
		internal bool bFallingBack;

		// Token: 0x04002E78 RID: 11896
		internal int iRecursionCount;

		// Token: 0x04002E79 RID: 11897
		private const int iMaxRecursion = 250;
	}
}
