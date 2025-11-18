using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000687 RID: 1671
	[Guid("a5c637bf-6eaa-4e5f-b535-55299657e33e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY
	{
		// Token: 0x06004F50 RID: 20304
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_ASSEMBLY[] rgelt);

		// Token: 0x06004F51 RID: 20305
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004F52 RID: 20306
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F53 RID: 20307
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY Clone();
	}
}
