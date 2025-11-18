using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006FF RID: 1791
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1583EFE9-832F-4d08-B041-CAC5ACEDB948")]
	[ComImport]
	internal interface IEntryPointEntry
	{
		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x060050D7 RID: 20695
		EntryPointEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060050D8 RID: 20696
		string Name
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060050D9 RID: 20697
		string CommandLine_File
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060050DA RID: 20698
		string CommandLine_Parameters
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x060050DB RID: 20699
		IReferenceIdentity Identity
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x060050DC RID: 20700
		uint Flags
		{
			[SecurityCritical]
			get;
		}
	}
}
