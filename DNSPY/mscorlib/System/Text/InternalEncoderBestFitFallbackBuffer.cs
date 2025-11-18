using System;
using System.Security;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000A6D RID: 2669
	internal sealed class InternalEncoderBestFitFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x060067E7 RID: 26599 RVA: 0x0015EE04 File Offset: 0x0015D004
		private static object InternalSyncObject
		{
			get
			{
				if (InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject, obj, null);
				}
				return InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x060067E8 RID: 26600 RVA: 0x0015EE30 File Offset: 0x0015D030
		public InternalEncoderBestFitFallbackBuffer(InternalEncoderBestFitFallback fallback)
		{
			this.oFallback = fallback;
			if (this.oFallback.arrayBestFit == null)
			{
				object internalSyncObject = InternalEncoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this.oFallback.arrayBestFit == null)
					{
						this.oFallback.arrayBestFit = fallback.encoding.GetBestFitUnicodeToBytesData();
					}
				}
			}
		}

		// Token: 0x060067E9 RID: 26601 RVA: 0x0015EEB0 File Offset: 0x0015D0B0
		public override bool Fallback(char charUnknown, int index)
		{
			this.iCount = (this.iSize = 1);
			this.cBestFit = this.TryBestFit(charUnknown);
			if (this.cBestFit == '\0')
			{
				this.cBestFit = '?';
			}
			return true;
		}

		// Token: 0x060067EA RID: 26602 RVA: 0x0015EEEC File Offset: 0x0015D0EC
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
			this.cBestFit = '?';
			this.iCount = (this.iSize = 2);
			return true;
		}

		// Token: 0x060067EB RID: 26603 RVA: 0x0015EF8C File Offset: 0x0015D18C
		public override char GetNextChar()
		{
			this.iCount--;
			if (this.iCount < 0)
			{
				return '\0';
			}
			if (this.iCount == 2147483647)
			{
				this.iCount = -1;
				return '\0';
			}
			return this.cBestFit;
		}

		// Token: 0x060067EC RID: 26604 RVA: 0x0015EFC3 File Offset: 0x0015D1C3
		public override bool MovePrevious()
		{
			if (this.iCount >= 0)
			{
				this.iCount++;
			}
			return this.iCount >= 0 && this.iCount <= this.iSize;
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060067ED RID: 26605 RVA: 0x0015EFF8 File Offset: 0x0015D1F8
		public override int Remaining
		{
			get
			{
				if (this.iCount <= 0)
				{
					return 0;
				}
				return this.iCount;
			}
		}

		// Token: 0x060067EE RID: 26606 RVA: 0x0015F00B File Offset: 0x0015D20B
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.iCount = -1;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x0015F024 File Offset: 0x0015D224
		private char TryBestFit(char cUnknown)
		{
			int num = 0;
			int num2 = this.oFallback.arrayBestFit.Length;
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = (num3 / 2 + num) & 65534;
				char c = this.oFallback.arrayBestFit[i];
				if (c == cUnknown)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
				if (c < cUnknown)
				{
					num = i;
				}
				else
				{
					num2 = i;
				}
			}
			for (int i = num; i < num2; i += 2)
			{
				if (this.oFallback.arrayBestFit[i] == cUnknown)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04002E65 RID: 11877
		private char cBestFit;

		// Token: 0x04002E66 RID: 11878
		private InternalEncoderBestFitFallback oFallback;

		// Token: 0x04002E67 RID: 11879
		private int iCount = -1;

		// Token: 0x04002E68 RID: 11880
		private int iSize;

		// Token: 0x04002E69 RID: 11881
		private static object s_InternalSyncObject;
	}
}
