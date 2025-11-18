using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200096E RID: 2414
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibExporterFlags
	{
		// Token: 0x04002BB4 RID: 11188
		None = 0,
		// Token: 0x04002BB5 RID: 11189
		OnlyReferenceRegistered = 1,
		// Token: 0x04002BB6 RID: 11190
		CallerResolvedReferences = 2,
		// Token: 0x04002BB7 RID: 11191
		OldNames = 4,
		// Token: 0x04002BB8 RID: 11192
		ExportAs32Bit = 16,
		// Token: 0x04002BB9 RID: 11193
		ExportAs64Bit = 32
	}
}
