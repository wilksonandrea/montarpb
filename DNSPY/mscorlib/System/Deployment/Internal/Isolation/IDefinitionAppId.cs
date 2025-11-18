using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000696 RID: 1686
	[Guid("d91e12d8-98ed-47fa-9936-39421283d59b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IDefinitionAppId
	{
		// Token: 0x06004F9C RID: 20380
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_SubscriptionId();

		// Token: 0x06004F9D RID: 20381
		void put_SubscriptionId([MarshalAs(UnmanagedType.LPWStr)] [In] string Subscription);

		// Token: 0x06004F9E RID: 20382
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string get_Codebase();

		// Token: 0x06004F9F RID: 20383
		[SecurityCritical]
		void put_Codebase([MarshalAs(UnmanagedType.LPWStr)] [In] string CodeBase);

		// Token: 0x06004FA0 RID: 20384
		[SecurityCritical]
		IEnumDefinitionIdentity EnumAppPath();

		// Token: 0x06004FA1 RID: 20385
		[SecurityCritical]
		void SetAppPath([In] uint cIDefinitionIdentity, [MarshalAs(UnmanagedType.LPArray)] [In] IDefinitionIdentity[] DefinitionIdentity);
	}
}
