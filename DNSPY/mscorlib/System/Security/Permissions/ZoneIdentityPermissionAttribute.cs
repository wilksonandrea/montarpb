using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002FA RID: 762
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060026DF RID: 9951 RVA: 0x0008CB85 File Offset: 0x0008AD85
		public ZoneIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x0008CB95 File Offset: 0x0008AD95
		// (set) Token: 0x060026E1 RID: 9953 RVA: 0x0008CB9D File Offset: 0x0008AD9D
		public SecurityZone Zone
		{
			get
			{
				return this.m_flag;
			}
			set
			{
				this.m_flag = value;
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x0008CBA6 File Offset: 0x0008ADA6
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new ZoneIdentityPermission(PermissionState.Unrestricted);
			}
			return new ZoneIdentityPermission(this.m_flag);
		}

		// Token: 0x04000F06 RID: 3846
		private SecurityZone m_flag = SecurityZone.NoZone;
	}
}
