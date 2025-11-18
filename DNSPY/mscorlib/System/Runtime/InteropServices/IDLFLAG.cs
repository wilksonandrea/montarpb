using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000998 RID: 2456
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum IDLFLAG : short
	{
		// Token: 0x04002C45 RID: 11333
		IDLFLAG_NONE = 0,
		// Token: 0x04002C46 RID: 11334
		IDLFLAG_FIN = 1,
		// Token: 0x04002C47 RID: 11335
		IDLFLAG_FOUT = 2,
		// Token: 0x04002C48 RID: 11336
		IDLFLAG_FLCID = 4,
		// Token: 0x04002C49 RID: 11337
		IDLFLAG_FRETVAL = 8
	}
}
