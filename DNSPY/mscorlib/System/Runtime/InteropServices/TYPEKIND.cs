using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000993 RID: 2451
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum TYPEKIND
	{
		// Token: 0x04002C07 RID: 11271
		TKIND_ENUM,
		// Token: 0x04002C08 RID: 11272
		TKIND_RECORD,
		// Token: 0x04002C09 RID: 11273
		TKIND_MODULE,
		// Token: 0x04002C0A RID: 11274
		TKIND_INTERFACE,
		// Token: 0x04002C0B RID: 11275
		TKIND_DISPATCH,
		// Token: 0x04002C0C RID: 11276
		TKIND_COCLASS,
		// Token: 0x04002C0D RID: 11277
		TKIND_ALIAS,
		// Token: 0x04002C0E RID: 11278
		TKIND_UNION,
		// Token: 0x04002C0F RID: 11279
		TKIND_MAX
	}
}
