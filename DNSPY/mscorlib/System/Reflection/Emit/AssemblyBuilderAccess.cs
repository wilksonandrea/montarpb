using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000630 RID: 1584
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum AssemblyBuilderAccess
	{
		// Token: 0x04001E85 RID: 7813
		Run = 1,
		// Token: 0x04001E86 RID: 7814
		Save = 2,
		// Token: 0x04001E87 RID: 7815
		RunAndSave = 3,
		// Token: 0x04001E88 RID: 7816
		ReflectionOnly = 6,
		// Token: 0x04001E89 RID: 7817
		RunAndCollect = 9
	}
}
