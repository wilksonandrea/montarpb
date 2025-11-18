using System;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x0200072C RID: 1836
	[Serializable]
	public enum Consistency
	{
		// Token: 0x04002438 RID: 9272
		MayCorruptProcess,
		// Token: 0x04002439 RID: 9273
		MayCorruptAppDomain,
		// Token: 0x0400243A RID: 9274
		MayCorruptInstance,
		// Token: 0x0400243B RID: 9275
		WillNotCorruptState
	}
}
