using System;
using System.IO;
using System.Security.Permissions;

namespace System.Security.AccessControl
{
	// Token: 0x0200021D RID: 541
	public sealed class DirectorySecurity : FileSystemSecurity
	{
		// Token: 0x06001F60 RID: 8032 RVA: 0x0006D934 File Offset: 0x0006BB34
		[SecuritySafeCritical]
		public DirectorySecurity()
			: base(true)
		{
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0006D940 File Offset: 0x0006BB40
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public DirectorySecurity(string name, AccessControlSections includeSections)
			: base(true, name, includeSections, true)
		{
			string fullPathInternal = Path.GetFullPathInternal(name);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.View, fullPathInternal, false, false);
		}
	}
}
