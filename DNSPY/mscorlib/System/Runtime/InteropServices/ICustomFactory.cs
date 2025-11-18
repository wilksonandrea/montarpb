using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000967 RID: 2407
	[ComVisible(true)]
	public interface ICustomFactory
	{
		// Token: 0x0600622D RID: 25133
		MarshalByRefObject CreateInstance(Type serverType);
	}
}
