using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000977 RID: 2423
	[Flags]
	public enum RegistrationConnectionType
	{
		// Token: 0x04002BD6 RID: 11222
		SingleUse = 0,
		// Token: 0x04002BD7 RID: 11223
		MultipleUse = 1,
		// Token: 0x04002BD8 RID: 11224
		MultiSeparate = 2,
		// Token: 0x04002BD9 RID: 11225
		Suspended = 4,
		// Token: 0x04002BDA RID: 11226
		Surrogate = 8
	}
}
