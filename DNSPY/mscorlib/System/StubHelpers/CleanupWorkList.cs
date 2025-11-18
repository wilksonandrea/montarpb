using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005AD RID: 1453
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[SecurityCritical]
	internal sealed class CleanupWorkList
	{
		// Token: 0x06004334 RID: 17204 RVA: 0x000FA12C File Offset: 0x000F832C
		public void Add(CleanupWorkListElement elem)
		{
			this.m_list.Add(elem);
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x000FA13C File Offset: 0x000F833C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Destroy()
		{
			for (int i = this.m_list.Count - 1; i >= 0; i--)
			{
				if (this.m_list[i].m_owned)
				{
					StubHelpers.SafeHandleRelease(this.m_list[i].m_handle);
				}
			}
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x000FA18A File Offset: 0x000F838A
		public CleanupWorkList()
		{
		}

		// Token: 0x04001BF4 RID: 7156
		private List<CleanupWorkListElement> m_list = new List<CleanupWorkListElement>();
	}
}
