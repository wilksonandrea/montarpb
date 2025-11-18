using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000925 RID: 2341
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibTypeFlags
	{
		// Token: 0x04002A85 RID: 10885
		FAppObject = 1,
		// Token: 0x04002A86 RID: 10886
		FCanCreate = 2,
		// Token: 0x04002A87 RID: 10887
		FLicensed = 4,
		// Token: 0x04002A88 RID: 10888
		FPreDeclId = 8,
		// Token: 0x04002A89 RID: 10889
		FHidden = 16,
		// Token: 0x04002A8A RID: 10890
		FControl = 32,
		// Token: 0x04002A8B RID: 10891
		FDual = 64,
		// Token: 0x04002A8C RID: 10892
		FNonExtensible = 128,
		// Token: 0x04002A8D RID: 10893
		FOleAutomation = 256,
		// Token: 0x04002A8E RID: 10894
		FRestricted = 512,
		// Token: 0x04002A8F RID: 10895
		FAggregatable = 1024,
		// Token: 0x04002A90 RID: 10896
		FReplaceable = 2048,
		// Token: 0x04002A91 RID: 10897
		FDispatchable = 4096,
		// Token: 0x04002A92 RID: 10898
		FReverseBind = 8192
	}
}
