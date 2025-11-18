using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000605 RID: 1541
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum MemberTypes
	{
		// Token: 0x04001D66 RID: 7526
		Constructor = 1,
		// Token: 0x04001D67 RID: 7527
		Event = 2,
		// Token: 0x04001D68 RID: 7528
		Field = 4,
		// Token: 0x04001D69 RID: 7529
		Method = 8,
		// Token: 0x04001D6A RID: 7530
		Property = 16,
		// Token: 0x04001D6B RID: 7531
		TypeInfo = 32,
		// Token: 0x04001D6C RID: 7532
		Custom = 64,
		// Token: 0x04001D6D RID: 7533
		NestedType = 128,
		// Token: 0x04001D6E RID: 7534
		All = 191
	}
}
