using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097C RID: 2428
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.BIND_OPTS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public struct BIND_OPTS
	{
		// Token: 0x04002BE9 RID: 11241
		public int cbStruct;

		// Token: 0x04002BEA RID: 11242
		public int grfFlags;

		// Token: 0x04002BEB RID: 11243
		public int grfMode;

		// Token: 0x04002BEC RID: 11244
		public int dwTickCountDeadline;
	}
}
