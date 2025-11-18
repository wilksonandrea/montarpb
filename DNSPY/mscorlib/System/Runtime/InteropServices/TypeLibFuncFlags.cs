using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000926 RID: 2342
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibFuncFlags
	{
		// Token: 0x04002A94 RID: 10900
		FRestricted = 1,
		// Token: 0x04002A95 RID: 10901
		FSource = 2,
		// Token: 0x04002A96 RID: 10902
		FBindable = 4,
		// Token: 0x04002A97 RID: 10903
		FRequestEdit = 8,
		// Token: 0x04002A98 RID: 10904
		FDisplayBind = 16,
		// Token: 0x04002A99 RID: 10905
		FDefaultBind = 32,
		// Token: 0x04002A9A RID: 10906
		FHidden = 64,
		// Token: 0x04002A9B RID: 10907
		FUsesGetLastError = 128,
		// Token: 0x04002A9C RID: 10908
		FDefaultCollelem = 256,
		// Token: 0x04002A9D RID: 10909
		FUiDefault = 512,
		// Token: 0x04002A9E RID: 10910
		FNonBrowsable = 1024,
		// Token: 0x04002A9F RID: 10911
		FReplaceable = 2048,
		// Token: 0x04002AA0 RID: 10912
		FImmediateBind = 4096
	}
}
