using System;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x020001F0 RID: 496
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract class SecurityState
	{
		// Token: 0x06001E06 RID: 7686 RVA: 0x00068A3D File Offset: 0x00066C3D
		protected SecurityState()
		{
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00068A48 File Offset: 0x00066C48
		[SecurityCritical]
		public bool IsStateAvailable()
		{
			AppDomainManager currentAppDomainManager = AppDomainManager.CurrentAppDomainManager;
			return currentAppDomainManager != null && currentAppDomainManager.CheckSecuritySettings(this);
		}

		// Token: 0x06001E08 RID: 7688
		public abstract void EnsureState();
	}
}
