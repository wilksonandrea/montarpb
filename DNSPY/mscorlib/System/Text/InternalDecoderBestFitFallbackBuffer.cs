using System;
using System.Security;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000A62 RID: 2658
	internal sealed class InternalDecoderBestFitFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x0600678B RID: 26507 RVA: 0x0015DB6C File Offset: 0x0015BD6C
		private static object InternalSyncObject
		{
			get
			{
				if (InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject, obj, null);
				}
				return InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x0600678C RID: 26508 RVA: 0x0015DB98 File Offset: 0x0015BD98
		public InternalDecoderBestFitFallbackBuffer(InternalDecoderBestFitFallback fallback)
		{
			this.oFallback = fallback;
			if (this.oFallback.arrayBestFit == null)
			{
				object internalSyncObject = InternalDecoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this.oFallback.arrayBestFit == null)
					{
						this.oFallback.arrayBestFit = fallback.encoding.GetBestFitBytesToUnicodeData();
					}
				}
			}
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x0015DC18 File Offset: 0x0015BE18
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.cBestFit = this.TryBestFit(bytesUnknown);
			if (this.cBestFit == '\0')
			{
				this.cBestFit = this.oFallback.cReplacement;
			}
			this.iCount = (this.iSize = 1);
			return true;
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x0015DC5C File Offset: 0x0015BE5C
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

		// Token: 0x0600678F RID: 26511 RVA: 0x0015DC93 File Offset: 0x0015BE93
		public override bool MovePrevious()
		{
			if (this.iCount >= 0)
			{
				this.iCount++;
			}
			return this.iCount >= 0 && this.iCount <= this.iSize;
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06006790 RID: 26512 RVA: 0x0015DCC8 File Offset: 0x0015BEC8
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

		// Token: 0x06006791 RID: 26513 RVA: 0x0015DCDB File Offset: 0x0015BEDB
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.iCount = -1;
			this.byteStart = null;
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x0015DCEC File Offset: 0x0015BEEC
		[SecurityCritical]
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return 1;
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x0015DCF0 File Offset: 0x0015BEF0
		private char TryBestFit(byte[] bytesCheck)
		{
			int num = 0;
			int num2 = this.oFallback.arrayBestFit.Length;
			if (num2 == 0)
			{
				return '\0';
			}
			if (bytesCheck.Length == 0 || bytesCheck.Length > 2)
			{
				return '\0';
			}
			char c;
			if (bytesCheck.Length == 1)
			{
				c = (char)bytesCheck[0];
			}
			else
			{
				c = (char)(((int)bytesCheck[0] << 8) + (int)bytesCheck[1]);
			}
			if (c < this.oFallback.arrayBestFit[0] || c > this.oFallback.arrayBestFit[num2 - 2])
			{
				return '\0';
			}
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = (num3 / 2 + num) & 65534;
				char c2 = this.oFallback.arrayBestFit[i];
				if (c2 == c)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
				if (c2 < c)
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
				if (this.oFallback.arrayBestFit[i] == c)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04002E4B RID: 11851
		internal char cBestFit;

		// Token: 0x04002E4C RID: 11852
		internal int iCount = -1;

		// Token: 0x04002E4D RID: 11853
		internal int iSize;

		// Token: 0x04002E4E RID: 11854
		private InternalDecoderBestFitFallback oFallback;

		// Token: 0x04002E4F RID: 11855
		private static object s_InternalSyncObject;
	}
}
