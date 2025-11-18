using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200099E RID: 2462
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.VARDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		// Token: 0x04002C5B RID: 11355
		public int memid;

		// Token: 0x04002C5C RID: 11356
		public string lpstrSchema;

		// Token: 0x04002C5D RID: 11357
		public ELEMDESC elemdescVar;

		// Token: 0x04002C5E RID: 11358
		public short wVarFlags;

		// Token: 0x04002C5F RID: 11359
		public VarEnum varkind;

		// Token: 0x02000C9D RID: 3229
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x04003863 RID: 14435
			[FieldOffset(0)]
			public int oInst;

			// Token: 0x04003864 RID: 14436
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
