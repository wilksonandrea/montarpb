using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000851 RID: 2129
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public abstract class BaseChannelSinkWithProperties : BaseChannelObjectWithProperties
	{
		// Token: 0x06005A3F RID: 23103 RVA: 0x0013D5F9 File Offset: 0x0013B7F9
		protected BaseChannelSinkWithProperties()
		{
		}
	}
}
