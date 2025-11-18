using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal
{
	// Token: 0x0200066E RID: 1646
	[ComVisible(false)]
	public static class InternalApplicationIdentityHelper
	{
		// Token: 0x06004F1B RID: 20251 RVA: 0x0011C134 File Offset: 0x0011A334
		[SecurityCritical]
		public static object GetInternalAppId(ApplicationIdentity id)
		{
			return id.Identity;
		}
	}
}
