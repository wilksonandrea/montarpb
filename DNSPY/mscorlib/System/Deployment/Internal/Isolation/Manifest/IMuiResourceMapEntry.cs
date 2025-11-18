using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D5 RID: 1749
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("397927f5-10f2-4ecb-bfe1-3c264212a193")]
	[ComImport]
	internal interface IMuiResourceMapEntry
	{
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06005071 RID: 20593
		MuiResourceMapEntry AllData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06005072 RID: 20594
		object ResourceTypeIdInt
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06005073 RID: 20595
		object ResourceTypeIdString
		{
			[SecurityCritical]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
