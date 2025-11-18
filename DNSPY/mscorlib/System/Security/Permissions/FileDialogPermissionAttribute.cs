using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002F2 RID: 754
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x0600266B RID: 9835 RVA: 0x0008C286 File Offset: 0x0008A486
		public FileDialogPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x0008C28F File Offset: 0x0008A48F
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x0008C29C File Offset: 0x0008A49C
		public bool Open
		{
			get
			{
				return (this.m_access & FileDialogPermissionAccess.Open) > FileDialogPermissionAccess.None;
			}
			set
			{
				this.m_access = (value ? (this.m_access | FileDialogPermissionAccess.Open) : (this.m_access & ~FileDialogPermissionAccess.Open));
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x0008C2BA File Offset: 0x0008A4BA
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x0008C2C7 File Offset: 0x0008A4C7
		public bool Save
		{
			get
			{
				return (this.m_access & FileDialogPermissionAccess.Save) > FileDialogPermissionAccess.None;
			}
			set
			{
				this.m_access = (value ? (this.m_access | FileDialogPermissionAccess.Save) : (this.m_access & ~FileDialogPermissionAccess.Save));
			}
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x0008C2E5 File Offset: 0x0008A4E5
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new FileDialogPermission(PermissionState.Unrestricted);
			}
			return new FileDialogPermission(this.m_access);
		}

		// Token: 0x04000EEB RID: 3819
		private FileDialogPermissionAccess m_access;
	}
}
