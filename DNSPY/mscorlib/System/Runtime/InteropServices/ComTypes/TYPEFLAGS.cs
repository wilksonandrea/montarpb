using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A3D RID: 2621
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TYPEFLAGS : short
	{
		// Token: 0x04002D75 RID: 11637
		[__DynamicallyInvokable]
		TYPEFLAG_FAPPOBJECT = 1,
		// Token: 0x04002D76 RID: 11638
		[__DynamicallyInvokable]
		TYPEFLAG_FCANCREATE = 2,
		// Token: 0x04002D77 RID: 11639
		[__DynamicallyInvokable]
		TYPEFLAG_FLICENSED = 4,
		// Token: 0x04002D78 RID: 11640
		[__DynamicallyInvokable]
		TYPEFLAG_FPREDECLID = 8,
		// Token: 0x04002D79 RID: 11641
		[__DynamicallyInvokable]
		TYPEFLAG_FHIDDEN = 16,
		// Token: 0x04002D7A RID: 11642
		[__DynamicallyInvokable]
		TYPEFLAG_FCONTROL = 32,
		// Token: 0x04002D7B RID: 11643
		[__DynamicallyInvokable]
		TYPEFLAG_FDUAL = 64,
		// Token: 0x04002D7C RID: 11644
		[__DynamicallyInvokable]
		TYPEFLAG_FNONEXTENSIBLE = 128,
		// Token: 0x04002D7D RID: 11645
		[__DynamicallyInvokable]
		TYPEFLAG_FOLEAUTOMATION = 256,
		// Token: 0x04002D7E RID: 11646
		[__DynamicallyInvokable]
		TYPEFLAG_FRESTRICTED = 512,
		// Token: 0x04002D7F RID: 11647
		[__DynamicallyInvokable]
		TYPEFLAG_FAGGREGATABLE = 1024,
		// Token: 0x04002D80 RID: 11648
		[__DynamicallyInvokable]
		TYPEFLAG_FREPLACEABLE = 2048,
		// Token: 0x04002D81 RID: 11649
		[__DynamicallyInvokable]
		TYPEFLAG_FDISPATCHABLE = 4096,
		// Token: 0x04002D82 RID: 11650
		[__DynamicallyInvokable]
		TYPEFLAG_FREVERSEBIND = 8192,
		// Token: 0x04002D83 RID: 11651
		[__DynamicallyInvokable]
		TYPEFLAG_FPROXY = 16384
	}
}
