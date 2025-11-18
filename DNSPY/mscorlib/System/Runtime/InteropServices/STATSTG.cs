using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200098E RID: 2446
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.STATSTG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		// Token: 0x04002BF1 RID: 11249
		public string pwcsName;

		// Token: 0x04002BF2 RID: 11250
		public int type;

		// Token: 0x04002BF3 RID: 11251
		public long cbSize;

		// Token: 0x04002BF4 RID: 11252
		public FILETIME mtime;

		// Token: 0x04002BF5 RID: 11253
		public FILETIME ctime;

		// Token: 0x04002BF6 RID: 11254
		public FILETIME atime;

		// Token: 0x04002BF7 RID: 11255
		public int grfMode;

		// Token: 0x04002BF8 RID: 11256
		public int grfLocksSupported;

		// Token: 0x04002BF9 RID: 11257
		public Guid clsid;

		// Token: 0x04002BFA RID: 11258
		public int grfStateBits;

		// Token: 0x04002BFB RID: 11259
		public int reserved;
	}
}
