using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x020007AD RID: 1965
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C460E2B4-E199-412a-8456-84DC3E4838C3")]
	[ComVisible(true)]
	public interface IObjectHandle
	{
		// Token: 0x06005513 RID: 21779
		object Unwrap();
	}
}
