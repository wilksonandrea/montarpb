using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000996 RID: 2454
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEATTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		// Token: 0x04002C25 RID: 11301
		public const int MEMBER_ID_NIL = -1;

		// Token: 0x04002C26 RID: 11302
		public Guid guid;

		// Token: 0x04002C27 RID: 11303
		public int lcid;

		// Token: 0x04002C28 RID: 11304
		public int dwReserved;

		// Token: 0x04002C29 RID: 11305
		public int memidConstructor;

		// Token: 0x04002C2A RID: 11306
		public int memidDestructor;

		// Token: 0x04002C2B RID: 11307
		public IntPtr lpstrSchema;

		// Token: 0x04002C2C RID: 11308
		public int cbSizeInstance;

		// Token: 0x04002C2D RID: 11309
		public TYPEKIND typekind;

		// Token: 0x04002C2E RID: 11310
		public short cFuncs;

		// Token: 0x04002C2F RID: 11311
		public short cVars;

		// Token: 0x04002C30 RID: 11312
		public short cImplTypes;

		// Token: 0x04002C31 RID: 11313
		public short cbSizeVft;

		// Token: 0x04002C32 RID: 11314
		public short cbAlignment;

		// Token: 0x04002C33 RID: 11315
		public TYPEFLAGS wTypeFlags;

		// Token: 0x04002C34 RID: 11316
		public short wMajorVerNum;

		// Token: 0x04002C35 RID: 11317
		public short wMinorVerNum;

		// Token: 0x04002C36 RID: 11318
		public TYPEDESC tdescAlias;

		// Token: 0x04002C37 RID: 11319
		public IDLDESC idldescType;
	}
}
