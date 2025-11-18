using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000723 RID: 1827
	[Flags]
	public enum ResourceScope
	{
		// Token: 0x0400241B RID: 9243
		None = 0,
		// Token: 0x0400241C RID: 9244
		Machine = 1,
		// Token: 0x0400241D RID: 9245
		Process = 2,
		// Token: 0x0400241E RID: 9246
		AppDomain = 4,
		// Token: 0x0400241F RID: 9247
		Library = 8,
		// Token: 0x04002420 RID: 9248
		Private = 16,
		// Token: 0x04002421 RID: 9249
		Assembly = 32
	}
}
