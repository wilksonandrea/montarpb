using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009A1 RID: 2465
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum FUNCKIND
	{
		// Token: 0x04002C6D RID: 11373
		FUNC_VIRTUAL,
		// Token: 0x04002C6E RID: 11374
		FUNC_PUREVIRTUAL,
		// Token: 0x04002C6F RID: 11375
		FUNC_NONVIRTUAL,
		// Token: 0x04002C70 RID: 11376
		FUNC_STATIC,
		// Token: 0x04002C71 RID: 11377
		FUNC_DISPATCH
	}
}
