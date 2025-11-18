using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000819 RID: 2073
	[ComVisible(true)]
	public interface IDynamicProperty
	{
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060058E6 RID: 22758
		string Name
		{
			[SecurityCritical]
			get;
		}
	}
}
