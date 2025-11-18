using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002F0 RID: 752
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeAccessSecurityAttribute : SecurityAttribute
	{
		// Token: 0x06002662 RID: 9826 RVA: 0x0008C1E3 File Offset: 0x0008A3E3
		protected CodeAccessSecurityAttribute(SecurityAction action)
			: base(action)
		{
		}
	}
}
