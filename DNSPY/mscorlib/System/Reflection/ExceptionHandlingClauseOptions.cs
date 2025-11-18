using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000613 RID: 1555
	[Flags]
	[ComVisible(true)]
	public enum ExceptionHandlingClauseOptions
	{
		// Token: 0x04001DD2 RID: 7634
		Clause = 0,
		// Token: 0x04001DD3 RID: 7635
		Filter = 1,
		// Token: 0x04001DD4 RID: 7636
		Finally = 2,
		// Token: 0x04001DD5 RID: 7637
		Fault = 4
	}
}
