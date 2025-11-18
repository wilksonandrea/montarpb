using System;

namespace System.Text
{
	// Token: 0x02000A6E RID: 2670
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class EncoderExceptionFallback : EncoderFallback
	{
		// Token: 0x060067F0 RID: 26608 RVA: 0x0015F0B3 File Offset: 0x0015D2B3
		[__DynamicallyInvokable]
		public EncoderExceptionFallback()
		{
		}

		// Token: 0x060067F1 RID: 26609 RVA: 0x0015F0BB File Offset: 0x0015D2BB
		[__DynamicallyInvokable]
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderExceptionFallbackBuffer();
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x060067F2 RID: 26610 RVA: 0x0015F0C2 File Offset: 0x0015D2C2
		[__DynamicallyInvokable]
		public override int MaxCharCount
		{
			[__DynamicallyInvokable]
			get
			{
				return 0;
			}
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x0015F0C8 File Offset: 0x0015D2C8
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			return value is EncoderExceptionFallback;
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x0015F0E2 File Offset: 0x0015D2E2
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return 654;
		}
	}
}
