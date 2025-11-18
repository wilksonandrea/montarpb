using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009A9 RID: 2473
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPELIBATTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		// Token: 0x04002CA7 RID: 11431
		public Guid guid;

		// Token: 0x04002CA8 RID: 11432
		public int lcid;

		// Token: 0x04002CA9 RID: 11433
		public SYSKIND syskind;

		// Token: 0x04002CAA RID: 11434
		public short wMajorVerNum;

		// Token: 0x04002CAB RID: 11435
		public short wMinorVerNum;

		// Token: 0x04002CAC RID: 11436
		public LIBFLAGS wLibFlags;
	}
}
