using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006F9 RID: 1785
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8AD3FC86-AFD3-477a-8FD5-146C291195BA")]
	[ComImport]
	internal interface IWindowClassEntry
	{
		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x060050CE RID: 20686
		WindowClassEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x060050CF RID: 20687
		string ClassName
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x060050D0 RID: 20688
		string HostDll
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x060050D1 RID: 20689
		bool fVersioned
		{
			[SecurityCritical]
			get;
		}
	}
}
