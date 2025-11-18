using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200070B RID: 1803
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CFA3F59F-334D-46bf-A5A5-5D11BB2D7EBC")]
	[ComImport]
	internal interface IDeploymentMetadataEntry
	{
		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x060050EE RID: 20718
		DeploymentMetadataEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x060050EF RID: 20719
		string DeploymentProviderCodebase
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x060050F0 RID: 20720
		string MinimumRequiredVersion
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x060050F1 RID: 20721
		ushort MaximumAge
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x060050F2 RID: 20722
		byte MaximumAge_Unit
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x060050F3 RID: 20723
		uint DeploymentFlags
		{
			[SecurityCritical]
			get;
		}
	}
}
