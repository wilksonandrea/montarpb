using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002F1 RID: 753
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002663 RID: 9827 RVA: 0x0008C1EC File Offset: 0x0008A3EC
		public EnvironmentPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x0008C1F5 File Offset: 0x0008A3F5
		// (set) Token: 0x06002665 RID: 9829 RVA: 0x0008C1FD File Offset: 0x0008A3FD
		public string Read
		{
			get
			{
				return this.m_read;
			}
			set
			{
				this.m_read = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x0008C206 File Offset: 0x0008A406
		// (set) Token: 0x06002667 RID: 9831 RVA: 0x0008C20E File Offset: 0x0008A40E
		public string Write
		{
			get
			{
				return this.m_write;
			}
			set
			{
				this.m_write = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x0008C217 File Offset: 0x0008A417
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x0008C228 File Offset: 0x0008A428
		public string All
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
			}
			set
			{
				this.m_write = value;
				this.m_read = value;
			}
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x0008C238 File Offset: 0x0008A438
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new EnvironmentPermission(PermissionState.Unrestricted);
			}
			EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
			if (this.m_read != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Read, this.m_read);
			}
			if (this.m_write != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Write, this.m_write);
			}
			return environmentPermission;
		}

		// Token: 0x04000EE9 RID: 3817
		private string m_read;

		// Token: 0x04000EEA RID: 3818
		private string m_write;
	}
}
