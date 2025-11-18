using System;

namespace System.Runtime
{
	// Token: 0x02000718 RID: 1816
	[__DynamicallyInvokable]
	[Serializable]
	public enum GCLatencyMode
	{
		// Token: 0x040023FE RID: 9214
		[__DynamicallyInvokable]
		Batch,
		// Token: 0x040023FF RID: 9215
		[__DynamicallyInvokable]
		Interactive,
		// Token: 0x04002400 RID: 9216
		[__DynamicallyInvokable]
		LowLatency,
		// Token: 0x04002401 RID: 9217
		[__DynamicallyInvokable]
		SustainedLowLatency,
		// Token: 0x04002402 RID: 9218
		NoGCRegion
	}
}
