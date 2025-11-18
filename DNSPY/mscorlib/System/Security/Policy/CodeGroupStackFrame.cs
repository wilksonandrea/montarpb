using System;

namespace System.Security.Policy
{
	// Token: 0x02000366 RID: 870
	internal sealed class CodeGroupStackFrame
	{
		// Token: 0x06002B17 RID: 11031 RVA: 0x000A07D7 File Offset: 0x0009E9D7
		public CodeGroupStackFrame()
		{
		}

		// Token: 0x0400118D RID: 4493
		internal CodeGroup current;

		// Token: 0x0400118E RID: 4494
		internal PolicyStatement policy;

		// Token: 0x0400118F RID: 4495
		internal CodeGroupStackFrame parent;
	}
}
