using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000990 RID: 2448
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.DESCKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum DESCKIND
	{
		// Token: 0x04002BFD RID: 11261
		DESCKIND_NONE,
		// Token: 0x04002BFE RID: 11262
		DESCKIND_FUNCDESC,
		// Token: 0x04002BFF RID: 11263
		DESCKIND_VARDESC,
		// Token: 0x04002C00 RID: 11264
		DESCKIND_TYPECOMP,
		// Token: 0x04002C01 RID: 11265
		DESCKIND_IMPLICITAPPOBJ,
		// Token: 0x04002C02 RID: 11266
		DESCKIND_MAX
	}
}
