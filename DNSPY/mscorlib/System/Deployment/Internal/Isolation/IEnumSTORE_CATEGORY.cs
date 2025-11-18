using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068B RID: 1675
	[Guid("b840a2f5-a497-4a6d-9038-cd3ec2fbd222")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY
	{
		// Token: 0x06004F66 RID: 20326
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY[] rgElements);

		// Token: 0x06004F67 RID: 20327
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x06004F68 RID: 20328
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F69 RID: 20329
		[SecurityCritical]
		IEnumSTORE_CATEGORY Clone();
	}
}
