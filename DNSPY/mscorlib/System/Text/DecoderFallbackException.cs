using System;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x02000A65 RID: 2661
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		// Token: 0x0600679F RID: 26527 RVA: 0x0015DEB5 File Offset: 0x0015C0B5
		[__DynamicallyInvokable]
		public DecoderFallbackException()
			: base(Environment.GetResourceString("Arg_ArgumentException"))
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x0015DED2 File Offset: 0x0015C0D2
		[__DynamicallyInvokable]
		public DecoderFallbackException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x060067A1 RID: 26529 RVA: 0x0015DEE6 File Offset: 0x0015C0E6
		[__DynamicallyInvokable]
		public DecoderFallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x060067A2 RID: 26530 RVA: 0x0015DEFB File Offset: 0x0015C0FB
		internal DecoderFallbackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060067A3 RID: 26531 RVA: 0x0015DF05 File Offset: 0x0015C105
		[__DynamicallyInvokable]
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index)
			: base(message)
		{
			this.bytesUnknown = bytesUnknown;
			this.index = index;
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060067A4 RID: 26532 RVA: 0x0015DF1C File Offset: 0x0015C11C
		[__DynamicallyInvokable]
		public byte[] BytesUnknown
		{
			[__DynamicallyInvokable]
			get
			{
				return this.bytesUnknown;
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060067A5 RID: 26533 RVA: 0x0015DF24 File Offset: 0x0015C124
		[__DynamicallyInvokable]
		public int Index
		{
			[__DynamicallyInvokable]
			get
			{
				return this.index;
			}
		}

		// Token: 0x04002E50 RID: 11856
		private byte[] bytesUnknown;

		// Token: 0x04002E51 RID: 11857
		private int index;
	}
}
