using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000848 RID: 2120
	[ComVisible(true)]
	[Serializable]
	public enum ServerProcessing
	{
		// Token: 0x04002900 RID: 10496
		Complete,
		// Token: 0x04002901 RID: 10497
		OneWay,
		// Token: 0x04002902 RID: 10498
		Async
	}
}
