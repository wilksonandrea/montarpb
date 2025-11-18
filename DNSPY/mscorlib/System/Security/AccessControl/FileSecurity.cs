using System;
using System.IO;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x0200021C RID: 540
	public sealed class FileSecurity : FileSystemSecurity
	{
		// Token: 0x06001F5D RID: 8029 RVA: 0x0006D8E3 File Offset: 0x0006BAE3
		[SecuritySafeCritical]
		public FileSecurity()
			: base(false)
		{
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0006D8EC File Offset: 0x0006BAEC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public FileSecurity(string fileName, AccessControlSections includeSections)
			: base(false, fileName, includeSections, false)
		{
			string fullPathInternal = Path.GetFullPathInternal(fileName);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.View, fullPathInternal, false, false);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0006D914 File Offset: 0x0006BB14
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal FileSecurity(SafeFileHandle handle, string fullPath, AccessControlSections includeSections)
			: base(false, handle, includeSections, false)
		{
			if (fullPath != null)
			{
				FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.View, fullPath, false, true);
				return;
			}
			FileIOPermission.QuickDemand(PermissionState.Unrestricted);
		}
	}
}
