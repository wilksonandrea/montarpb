using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200099D RID: 2461
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ELEMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		// Token: 0x04002C59 RID: 11353
		public TYPEDESC tdesc;

		// Token: 0x04002C5A RID: 11354
		public ELEMDESC.DESCUNION desc;

		// Token: 0x02000C9C RID: 3228
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x04003861 RID: 14433
			[FieldOffset(0)]
			public IDLDESC idldesc;

			// Token: 0x04003862 RID: 14434
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
