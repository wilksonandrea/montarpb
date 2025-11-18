using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000300 RID: 768
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IsolatedStorageFilePermissionAttribute : IsolatedStoragePermissionAttribute
	{
		// Token: 0x06002700 RID: 9984 RVA: 0x0008CE19 File Offset: 0x0008B019
		public IsolatedStorageFilePermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x0008CE24 File Offset: 0x0008B024
		public override IPermission CreatePermission()
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission;
			if (this.m_unrestricted)
			{
				isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.Unrestricted);
			}
			else
			{
				isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.None);
				isolatedStorageFilePermission.UserQuota = this.m_userQuota;
				isolatedStorageFilePermission.UsageAllowed = this.m_allowed;
			}
			return isolatedStorageFilePermission;
		}
	}
}
