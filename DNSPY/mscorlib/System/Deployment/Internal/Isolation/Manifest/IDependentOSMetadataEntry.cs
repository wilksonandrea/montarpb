using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200070E RID: 1806
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CF168CF4-4E8F-4d92-9D2A-60E5CA21CF85")]
	[ComImport]
	internal interface IDependentOSMetadataEntry
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x060050F5 RID: 20725
		DependentOSMetadataEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x060050F6 RID: 20726
		string SupportUrl
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x060050F7 RID: 20727
		string Description
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060050F8 RID: 20728
		ushort MajorVersion
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060050F9 RID: 20729
		ushort MinorVersion
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x060050FA RID: 20730
		ushort BuildNumber
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x060050FB RID: 20731
		byte ServicePackMajor
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060050FC RID: 20732
		byte ServicePackMinor
		{
			[SecurityCritical]
			get;
		}
	}
}
