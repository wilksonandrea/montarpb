using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006F0 RID: 1776
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1E0422A1-F0D2-44ae-914B-8A2DECCFD22B")]
	[ComImport]
	internal interface ICLRSurrogateEntry
	{
		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060050B5 RID: 20661
		CLRSurrogateEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060050B6 RID: 20662
		Guid Clsid
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060050B7 RID: 20663
		string RuntimeVersion
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060050B8 RID: 20664
		string ClassName
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.LPWStr)]
			get;
		}
	}
}
