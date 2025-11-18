using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000685 RID: 1669
	[Guid("5fa4f590-a416-4b22-ac79-7c3f0d31f303")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY
	{
		// Token: 0x06004F45 RID: 20293
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] StoreOperationMetadataProperty[] AppIds);

		// Token: 0x06004F46 RID: 20294
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004F47 RID: 20295
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F48 RID: 20296
		[SecurityCritical]
		IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY Clone();
	}
}
