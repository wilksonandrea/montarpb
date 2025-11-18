using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002FD RID: 765
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060026EF RID: 9967 RVA: 0x0008CCE3 File Offset: 0x0008AEE3
		public UrlIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x0008CCEC File Offset: 0x0008AEEC
		// (set) Token: 0x060026F1 RID: 9969 RVA: 0x0008CCF4 File Offset: 0x0008AEF4
		public string Url
		{
			get
			{
				return this.m_url;
			}
			set
			{
				this.m_url = value;
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0008CCFD File Offset: 0x0008AEFD
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new UrlIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_url == null)
			{
				return new UrlIdentityPermission(PermissionState.None);
			}
			return new UrlIdentityPermission(this.m_url);
		}

		// Token: 0x04000F0B RID: 3851
		private string m_url;
	}
}
