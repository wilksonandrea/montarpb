using System;

namespace System.Runtime.Versioning
{
	// Token: 0x0200071F RID: 1823
	[Flags]
	[Serializable]
	public enum ComponentGuaranteesOptions
	{
		// Token: 0x04002412 RID: 9234
		None = 0,
		// Token: 0x04002413 RID: 9235
		Exchange = 1,
		// Token: 0x04002414 RID: 9236
		Stable = 2,
		// Token: 0x04002415 RID: 9237
		SideBySide = 4
	}
}
