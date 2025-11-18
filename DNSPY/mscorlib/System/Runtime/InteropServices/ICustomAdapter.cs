using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000966 RID: 2406
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface ICustomAdapter
	{
		// Token: 0x0600622C RID: 25132
		[__DynamicallyInvokable]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object GetUnderlyingObject();
	}
}
