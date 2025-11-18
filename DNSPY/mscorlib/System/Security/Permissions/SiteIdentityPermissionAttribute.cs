using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002FC RID: 764
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060026EB RID: 9963 RVA: 0x0008CC9E File Offset: 0x0008AE9E
		public SiteIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x0008CCA7 File Offset: 0x0008AEA7
		// (set) Token: 0x060026ED RID: 9965 RVA: 0x0008CCAF File Offset: 0x0008AEAF
		public string Site
		{
			get
			{
				return this.m_site;
			}
			set
			{
				this.m_site = value;
			}
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x0008CCB8 File Offset: 0x0008AEB8
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new SiteIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_site == null)
			{
				return new SiteIdentityPermission(PermissionState.None);
			}
			return new SiteIdentityPermission(this.m_site);
		}

		// Token: 0x04000F0A RID: 3850
		private string m_site;
	}
}
