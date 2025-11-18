using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000711 RID: 1809
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("4A33D662-2210-463A-BE9F-FBDF1AA554E3")]
	[ComImport]
	internal interface ICompatibleFrameworksMetadataEntry
	{
		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060050FE RID: 20734
		CompatibleFrameworksMetadataEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060050FF RID: 20735
		string SupportUrl
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
