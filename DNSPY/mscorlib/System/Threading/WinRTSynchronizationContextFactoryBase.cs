using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004ED RID: 1261
	[FriendAccessAllowed]
	[SecurityCritical]
	internal class WinRTSynchronizationContextFactoryBase
	{
		// Token: 0x06003B93 RID: 15251 RVA: 0x000E2340 File Offset: 0x000E0540
		[SecurityCritical]
		public virtual SynchronizationContext Create(object coreDispatcher)
		{
			return null;
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000E2343 File Offset: 0x000E0543
		public WinRTSynchronizationContextFactoryBase()
		{
		}
	}
}
