using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000708 RID: 1800
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CB73147E-5FC2-4c31-B4E6-58D13DBE1A08")]
	[ComImport]
	internal interface IDescriptionMetadataEntry
	{
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060050E6 RID: 20710
		DescriptionMetadataEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060050E7 RID: 20711
		string Publisher
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x060050E8 RID: 20712
		string Product
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060050E9 RID: 20713
		string SupportUrl
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060050EA RID: 20714
		string IconFile
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060050EB RID: 20715
		string ErrorReportUrl
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x060050EC RID: 20716
		string SuiteName
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
