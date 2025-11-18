using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006B7 RID: 1719
	[Guid("81c85208-fe61-4c15-b5bb-ff5ea66baad9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IManifestInformation
	{
		// Token: 0x0600502F RID: 20527
		[SecurityCritical]
		void get_FullPath([MarshalAs(UnmanagedType.LPWStr)] out string FullPath);
	}
}
