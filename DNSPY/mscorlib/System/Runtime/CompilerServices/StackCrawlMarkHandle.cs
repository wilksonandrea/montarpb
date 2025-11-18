using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008D7 RID: 2263
	internal struct StackCrawlMarkHandle
	{
		// Token: 0x06005DCC RID: 24012 RVA: 0x0014972F File Offset: 0x0014792F
		internal StackCrawlMarkHandle(IntPtr stackMark)
		{
			this.m_ptr = stackMark;
		}

		// Token: 0x04002A3E RID: 10814
		private IntPtr m_ptr;
	}
}
