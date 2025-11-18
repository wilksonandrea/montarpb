using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000693 RID: 1683
	[Guid("9cdaae75-246e-4b00-a26d-b9aec137a3eb")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumIDENTITY_ATTRIBUTE
	{
		// Token: 0x06004F8F RID: 20367
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDENTITY_ATTRIBUTE[] rgAttributes);

		// Token: 0x06004F90 RID: 20368
		[SecurityCritical]
		IntPtr CurrentIntoBuffer([In] IntPtr Available, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] Data);

		// Token: 0x06004F91 RID: 20369
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004F92 RID: 20370
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F93 RID: 20371
		[SecurityCritical]
		IEnumIDENTITY_ATTRIBUTE Clone();
	}
}
