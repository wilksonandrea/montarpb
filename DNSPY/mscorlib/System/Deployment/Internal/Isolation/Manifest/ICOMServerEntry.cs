using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006EA RID: 1770
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3903B11B-FBE8-477c-825F-DB828B5FD174")]
	[ComImport]
	internal interface ICOMServerEntry
	{
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060050A7 RID: 20647
		COMServerEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060050A8 RID: 20648
		Guid Clsid
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060050A9 RID: 20649
		uint Flags
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060050AA RID: 20650
		Guid ConfiguredGuid
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060050AB RID: 20651
		Guid ImplementedClsid
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060050AC RID: 20652
		Guid TypeLibrary
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060050AD RID: 20653
		uint ThreadingModel
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060050AE RID: 20654
		string RuntimeVersion
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060050AF RID: 20655
		string HostFile
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
