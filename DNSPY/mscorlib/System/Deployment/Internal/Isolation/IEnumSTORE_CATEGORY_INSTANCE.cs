using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068F RID: 1679
	[Guid("5ba7cb30-8508-4114-8c77-262fcda4fadb")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY_INSTANCE
	{
		// Token: 0x06004F7C RID: 20348
		[SecurityCritical]
		uint Next([In] uint ulElements, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY_INSTANCE[] rgInstances);

		// Token: 0x06004F7D RID: 20349
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x06004F7E RID: 20350
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F7F RID: 20351
		[SecurityCritical]
		IEnumSTORE_CATEGORY_INSTANCE Clone();
	}
}
