using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000689 RID: 1673
	[Guid("a5c6aaa3-03e4-478d-b9f5-2e45908d5e4f")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY_FILE
	{
		// Token: 0x06004F5B RID: 20315
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_ASSEMBLY_FILE[] rgelt);

		// Token: 0x06004F5C RID: 20316
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004F5D RID: 20317
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F5E RID: 20318
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY_FILE Clone();
	}
}
