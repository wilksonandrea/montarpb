using System;

namespace System.Text
{
	// Token: 0x02000A63 RID: 2659
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DecoderExceptionFallback : DecoderFallback
	{
		// Token: 0x06006794 RID: 26516 RVA: 0x0015DDCB File Offset: 0x0015BFCB
		[__DynamicallyInvokable]
		public DecoderExceptionFallback()
		{
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x0015DDD3 File Offset: 0x0015BFD3
		[__DynamicallyInvokable]
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderExceptionFallbackBuffer();
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06006796 RID: 26518 RVA: 0x0015DDDA File Offset: 0x0015BFDA
		[__DynamicallyInvokable]
		public override int MaxCharCount
		{
			[__DynamicallyInvokable]
			get
			{
				return 0;
			}
		}

		// Token: 0x06006797 RID: 26519 RVA: 0x0015DDE0 File Offset: 0x0015BFE0
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			return value is DecoderExceptionFallback;
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x0015DDFA File Offset: 0x0015BFFA
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return 879;
		}
	}
}
