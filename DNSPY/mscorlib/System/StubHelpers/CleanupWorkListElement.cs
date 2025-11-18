using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005AC RID: 1452
	[SecurityCritical]
	internal sealed class CleanupWorkListElement
	{
		// Token: 0x06004333 RID: 17203 RVA: 0x000FA11D File Offset: 0x000F831D
		public CleanupWorkListElement(SafeHandle handle)
		{
			this.m_handle = handle;
		}

		// Token: 0x04001BF2 RID: 7154
		public SafeHandle m_handle;

		// Token: 0x04001BF3 RID: 7155
		public bool m_owned;
	}
}
