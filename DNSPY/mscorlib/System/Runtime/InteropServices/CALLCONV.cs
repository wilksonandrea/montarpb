using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009A3 RID: 2467
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.CALLCONV instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum CALLCONV
	{
		// Token: 0x04002C78 RID: 11384
		CC_CDECL = 1,
		// Token: 0x04002C79 RID: 11385
		CC_MSCPASCAL,
		// Token: 0x04002C7A RID: 11386
		CC_PASCAL = 2,
		// Token: 0x04002C7B RID: 11387
		CC_MACPASCAL,
		// Token: 0x04002C7C RID: 11388
		CC_STDCALL,
		// Token: 0x04002C7D RID: 11389
		CC_RESERVED,
		// Token: 0x04002C7E RID: 11390
		CC_SYSCALL,
		// Token: 0x04002C7F RID: 11391
		CC_MPWCDECL,
		// Token: 0x04002C80 RID: 11392
		CC_MPWPASCAL,
		// Token: 0x04002C81 RID: 11393
		CC_MAX
	}
}
