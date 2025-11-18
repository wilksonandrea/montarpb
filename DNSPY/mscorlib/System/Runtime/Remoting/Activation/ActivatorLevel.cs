using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200089C RID: 2204
	[ComVisible(true)]
	[Serializable]
	public enum ActivatorLevel
	{
		// Token: 0x040029FE RID: 10750
		Construction = 4,
		// Token: 0x040029FF RID: 10751
		Context = 8,
		// Token: 0x04002A00 RID: 10752
		AppDomain = 12,
		// Token: 0x04002A01 RID: 10753
		Process = 16,
		// Token: 0x04002A02 RID: 10754
		Machine = 20
	}
}
