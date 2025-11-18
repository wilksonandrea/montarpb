using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000997 RID: 2455
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public struct FUNCDESC
	{
		// Token: 0x04002C38 RID: 11320
		public int memid;

		// Token: 0x04002C39 RID: 11321
		public IntPtr lprgscode;

		// Token: 0x04002C3A RID: 11322
		public IntPtr lprgelemdescParam;

		// Token: 0x04002C3B RID: 11323
		public FUNCKIND funckind;

		// Token: 0x04002C3C RID: 11324
		public INVOKEKIND invkind;

		// Token: 0x04002C3D RID: 11325
		public CALLCONV callconv;

		// Token: 0x04002C3E RID: 11326
		public short cParams;

		// Token: 0x04002C3F RID: 11327
		public short cParamsOpt;

		// Token: 0x04002C40 RID: 11328
		public short oVft;

		// Token: 0x04002C41 RID: 11329
		public short cScodes;

		// Token: 0x04002C42 RID: 11330
		public ELEMDESC elemdescFunc;

		// Token: 0x04002C43 RID: 11331
		public short wFuncFlags;
	}
}
