using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000697 RID: 1687
	[Guid("054f0bef-9e45-4363-8f5a-2f8e142d9a3b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IReferenceAppId
	{
		// Token: 0x06004FA2 RID: 20386
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_SubscriptionId();

		// Token: 0x06004FA3 RID: 20387
		void put_SubscriptionId([MarshalAs(UnmanagedType.LPWStr)] [In] string Subscription);

		// Token: 0x06004FA4 RID: 20388
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_Codebase();

		// Token: 0x06004FA5 RID: 20389
		void put_Codebase([MarshalAs(UnmanagedType.LPWStr)] [In] string CodeBase);

		// Token: 0x06004FA6 RID: 20390
		[SecurityCritical]
		IEnumReferenceIdentity EnumAppPath();
	}
}
