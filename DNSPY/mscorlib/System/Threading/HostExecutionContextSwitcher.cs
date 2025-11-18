using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004FA RID: 1274
	internal class HostExecutionContextSwitcher
	{
		// Token: 0x06003C3F RID: 15423 RVA: 0x000E3D14 File Offset: 0x000E1F14
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Undo(object switcherObject)
		{
			if (switcherObject == null)
			{
				return;
			}
			HostExecutionContextManager currentHostExecutionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
			if (currentHostExecutionContextManager != null)
			{
				currentHostExecutionContextManager.Revert(switcherObject);
			}
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x000E3D35 File Offset: 0x000E1F35
		public HostExecutionContextSwitcher()
		{
		}

		// Token: 0x04001999 RID: 6553
		internal ExecutionContext executionContext;

		// Token: 0x0400199A RID: 6554
		internal HostExecutionContext previousHostContext;

		// Token: 0x0400199B RID: 6555
		internal HostExecutionContext currentHostContext;
	}
}
