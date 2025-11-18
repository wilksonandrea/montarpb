using System;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A14 RID: 2580
	[Guid("86ddd2d7-ad80-44f6-a12e-63698b52825d")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IWinRTClassActivator
	{
		// Token: 0x060065BE RID: 26046
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.IInspectable)]
		object ActivateInstance([MarshalAs(UnmanagedType.HString)] string activatableClassId);

		// Token: 0x060065BF RID: 26047
		[SecurityCritical]
		IntPtr GetActivationFactory([MarshalAs(UnmanagedType.HString)] string activatableClassId, ref Guid iid);
	}
}
