using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000311 RID: 785
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060027AF RID: 10159 RVA: 0x0009045F File Offset: 0x0008E65F
		public GacIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x00090468 File Offset: 0x0008E668
		public override IPermission CreatePermission()
		{
			return new GacIdentityPermission();
		}
	}
}
