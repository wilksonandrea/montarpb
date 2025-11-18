using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006CC RID: 1740
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("24abe1f7-a396-4a03-9adf-1d5b86a5569f")]
	[ComImport]
	internal interface IMuiResourceIdLookupMapEntry
	{
		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600505D RID: 20573
		MuiResourceIdLookupMapEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600505E RID: 20574
		uint Count
		{
			[SecurityCritical]
			get;
		}
	}
}
