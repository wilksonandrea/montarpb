using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068D RID: 1677
	[Guid("19be1967-b2fc-4dc1-9627-f3cb6305d2a7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_CATEGORY_SUBCATEGORY
	{
		// Token: 0x06004F71 RID: 20337
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] STORE_CATEGORY_SUBCATEGORY[] rgElements);

		// Token: 0x06004F72 RID: 20338
		[SecurityCritical]
		void Skip([In] uint ulElements);

		// Token: 0x06004F73 RID: 20339
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F74 RID: 20340
		[SecurityCritical]
		IEnumSTORE_CATEGORY_SUBCATEGORY Clone();
	}
}
