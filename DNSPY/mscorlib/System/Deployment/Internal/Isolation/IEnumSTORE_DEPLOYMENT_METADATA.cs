using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000683 RID: 1667
	[Guid("f9fd4090-93db-45c0-af87-624940f19cff")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_DEPLOYMENT_METADATA
	{
		// Token: 0x06004F3A RID: 20282
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDefinitionAppId[] AppIds);

		// Token: 0x06004F3B RID: 20283
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004F3C RID: 20284
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F3D RID: 20285
		[SecurityCritical]
		IEnumSTORE_DEPLOYMENT_METADATA Clone();
	}
}
