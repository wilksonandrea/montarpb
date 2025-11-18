using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E5 RID: 2533
	[Guid("82BA7092-4C88-427D-A7BC-16DD93FEB67E")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IRestrictedErrorInfo
	{
		// Token: 0x06006488 RID: 25736
		void GetErrorDetails([MarshalAs(UnmanagedType.BStr)] out string description, out int error, [MarshalAs(UnmanagedType.BStr)] out string restrictedDescription, [MarshalAs(UnmanagedType.BStr)] out string capabilitySid);

		// Token: 0x06006489 RID: 25737
		void GetReference([MarshalAs(UnmanagedType.BStr)] out string reference);
	}
}
