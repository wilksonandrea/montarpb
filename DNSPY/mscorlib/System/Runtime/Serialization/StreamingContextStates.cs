using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000747 RID: 1863
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum StreamingContextStates
	{
		// Token: 0x0400246C RID: 9324
		CrossProcess = 1,
		// Token: 0x0400246D RID: 9325
		CrossMachine = 2,
		// Token: 0x0400246E RID: 9326
		File = 4,
		// Token: 0x0400246F RID: 9327
		Persistence = 8,
		// Token: 0x04002470 RID: 9328
		Remoting = 16,
		// Token: 0x04002471 RID: 9329
		Other = 32,
		// Token: 0x04002472 RID: 9330
		Clone = 64,
		// Token: 0x04002473 RID: 9331
		CrossAppDomain = 128,
		// Token: 0x04002474 RID: 9332
		All = 255
	}
}
