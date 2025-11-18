using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000927 RID: 2343
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibVarFlags
	{
		// Token: 0x04002AA2 RID: 10914
		FReadOnly = 1,
		// Token: 0x04002AA3 RID: 10915
		FSource = 2,
		// Token: 0x04002AA4 RID: 10916
		FBindable = 4,
		// Token: 0x04002AA5 RID: 10917
		FRequestEdit = 8,
		// Token: 0x04002AA6 RID: 10918
		FDisplayBind = 16,
		// Token: 0x04002AA7 RID: 10919
		FDefaultBind = 32,
		// Token: 0x04002AA8 RID: 10920
		FHidden = 64,
		// Token: 0x04002AA9 RID: 10921
		FRestricted = 128,
		// Token: 0x04002AAA RID: 10922
		FDefaultCollelem = 256,
		// Token: 0x04002AAB RID: 10923
		FUiDefault = 512,
		// Token: 0x04002AAC RID: 10924
		FNonBrowsable = 1024,
		// Token: 0x04002AAD RID: 10925
		FReplaceable = 2048,
		// Token: 0x04002AAE RID: 10926
		FImmediateBind = 4096
	}
}
