using System;

namespace System.Text
{
	// Token: 0x02000A6F RID: 2671
	public sealed class EncoderExceptionFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x060067F5 RID: 26613 RVA: 0x0015F0E9 File Offset: 0x0015D2E9
		public EncoderExceptionFallbackBuffer()
		{
		}

		// Token: 0x060067F6 RID: 26614 RVA: 0x0015F0F1 File Offset: 0x0015D2F1
		public override bool Fallback(char charUnknown, int index)
		{
			throw new EncoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageConversionIndex", new object[]
			{
				(int)charUnknown,
				index
			}), charUnknown, index);
		}

		// Token: 0x060067F7 RID: 26615 RVA: 0x0015F11C File Offset: 0x0015D31C
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
			int num = char.ConvertToUtf32(charUnknownHigh, charUnknownLow);
			throw new EncoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageConversionIndex", new object[] { num, index }), charUnknownHigh, charUnknownLow, index);
		}

		// Token: 0x060067F8 RID: 26616 RVA: 0x0015F1D5 File Offset: 0x0015D3D5
		public override char GetNextChar()
		{
			return '\0';
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x0015F1D8 File Offset: 0x0015D3D8
		public override bool MovePrevious()
		{
			return false;
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x060067FA RID: 26618 RVA: 0x0015F1DB File Offset: 0x0015D3DB
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}
	}
}
