using System;
using System.Globalization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A67 RID: 2663
	[__DynamicallyInvokable]
	public abstract class DecoderFallbackBuffer
	{
		// Token: 0x060067AD RID: 26541
		[__DynamicallyInvokable]
		public abstract bool Fallback(byte[] bytesUnknown, int index);

		// Token: 0x060067AE RID: 26542
		[__DynamicallyInvokable]
		public abstract char GetNextChar();

		// Token: 0x060067AF RID: 26543
		[__DynamicallyInvokable]
		public abstract bool MovePrevious();

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x060067B0 RID: 26544
		[__DynamicallyInvokable]
		public abstract int Remaining
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x060067B1 RID: 26545 RVA: 0x0015E028 File Offset: 0x0015C228
		[__DynamicallyInvokable]
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x0015E032 File Offset: 0x0015C232
		[SecurityCritical]
		internal void InternalReset()
		{
			this.byteStart = null;
			this.Reset();
		}

		// Token: 0x060067B3 RID: 26547 RVA: 0x0015E042 File Offset: 0x0015C242
		[SecurityCritical]
		internal unsafe void InternalInitialize(byte* byteStart, char* charEnd)
		{
			this.byteStart = byteStart;
			this.charEnd = charEnd;
		}

		// Token: 0x060067B4 RID: 26548 RVA: 0x0015E054 File Offset: 0x0015C254
		[SecurityCritical]
		internal unsafe virtual bool InternalFallback(byte[] bytes, byte* pBytes, ref char* chars)
		{
			if (this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				char* ptr = chars;
				bool flag = false;
				char nextChar;
				while ((nextChar = this.GetNextChar()) != '\0')
				{
					if (char.IsSurrogate(nextChar))
					{
						if (char.IsHighSurrogate(nextChar))
						{
							if (flag)
							{
								throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
							}
							flag = true;
						}
						else
						{
							if (!flag)
							{
								throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
							}
							flag = false;
						}
					}
					if (ptr >= this.charEnd)
					{
						return false;
					}
					*(ptr++) = nextChar;
				}
				if (flag)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
				}
				chars = ptr;
			}
			return true;
		}

		// Token: 0x060067B5 RID: 26549 RVA: 0x0015E0F4 File Offset: 0x0015C2F4
		[SecurityCritical]
		internal unsafe virtual int InternalFallback(byte[] bytes, byte* pBytes)
		{
			if (!this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				return 0;
			}
			int num = 0;
			bool flag = false;
			char nextChar;
			while ((nextChar = this.GetNextChar()) != '\0')
			{
				if (char.IsSurrogate(nextChar))
				{
					if (char.IsHighSurrogate(nextChar))
					{
						if (flag)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
						}
						flag = false;
					}
				}
				num++;
			}
			if (flag)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
			}
			return num;
		}

		// Token: 0x060067B6 RID: 26550 RVA: 0x0015E184 File Offset: 0x0015C384
		internal void ThrowLastBytesRecursive(byte[] bytesUnknown)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "\\x{0:X2}", bytesUnknown[num]));
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallbackBytes", new object[] { stringBuilder.ToString() }), "bytesUnknown");
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x0015E216 File Offset: 0x0015C416
		[__DynamicallyInvokable]
		protected DecoderFallbackBuffer()
		{
		}

		// Token: 0x04002E56 RID: 11862
		[SecurityCritical]
		internal unsafe byte* byteStart;

		// Token: 0x04002E57 RID: 11863
		[SecurityCritical]
		internal unsafe char* charEnd;
	}
}
