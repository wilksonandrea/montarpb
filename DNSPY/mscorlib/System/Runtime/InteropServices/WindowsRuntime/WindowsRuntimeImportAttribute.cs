using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009C9 RID: 2505
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[FriendAccessAllowed]
	internal sealed class WindowsRuntimeImportAttribute : Attribute
	{
		// Token: 0x060063C7 RID: 25543 RVA: 0x001546EC File Offset: 0x001528EC
		public WindowsRuntimeImportAttribute()
		{
		}
	}
}
