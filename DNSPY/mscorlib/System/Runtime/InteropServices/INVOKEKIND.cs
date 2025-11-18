using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009A2 RID: 2466
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.INVOKEKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum INVOKEKIND
	{
		// Token: 0x04002C73 RID: 11379
		INVOKE_FUNC = 1,
		// Token: 0x04002C74 RID: 11380
		INVOKE_PROPERTYGET,
		// Token: 0x04002C75 RID: 11381
		INVOKE_PROPERTYPUT = 4,
		// Token: 0x04002C76 RID: 11382
		INVOKE_PROPERTYPUTREF = 8
	}
}
