using System;
using System.Globalization;

namespace System.Text
{
	// Token: 0x02000A64 RID: 2660
	public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x06006799 RID: 26521 RVA: 0x0015DE01 File Offset: 0x0015C001
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.Throw(bytesUnknown, index);
			return true;
		}

		// Token: 0x0600679A RID: 26522 RVA: 0x0015DE0C File Offset: 0x0015C00C
		public override char GetNextChar()
		{
			return '\0';
		}

		// Token: 0x0600679B RID: 26523 RVA: 0x0015DE0F File Offset: 0x0015C00F
		public override bool MovePrevious()
		{
			return false;
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x0600679C RID: 26524 RVA: 0x0015DE12 File Offset: 0x0015C012
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600679D RID: 26525 RVA: 0x0015DE18 File Offset: 0x0015C018
		private void Throw(byte[] bytesUnknown, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(bytesUnknown[num].ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append("]");
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new DecoderFallbackException(Environment.GetResourceString("Argument_InvalidCodePageBytesIndex", new object[] { stringBuilder, index }), bytesUnknown, index);
		}

		// Token: 0x0600679E RID: 26526 RVA: 0x0015DEAD File Offset: 0x0015C0AD
		public DecoderExceptionFallbackBuffer()
		{
		}
	}
}
