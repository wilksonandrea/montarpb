using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008D5 RID: 2261
	internal struct StringHandleOnStack
	{
		// Token: 0x06005DCA RID: 24010 RVA: 0x0014971D File Offset: 0x0014791D
		internal StringHandleOnStack(IntPtr pString)
		{
			this.m_ptr = pString;
		}

		// Token: 0x04002A3C RID: 10812
		private IntPtr m_ptr;
	}
}
