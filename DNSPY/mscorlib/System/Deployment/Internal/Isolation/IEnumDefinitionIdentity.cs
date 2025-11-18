using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000694 RID: 1684
	[Guid("f3549d9c-fc73-4793-9c00-1cd204254c0c")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumDefinitionIdentity
	{
		// Token: 0x06004F94 RID: 20372
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] IDefinitionIdentity[] DefinitionIdentity);

		// Token: 0x06004F95 RID: 20373
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004F96 RID: 20374
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F97 RID: 20375
		[SecurityCritical]
		IEnumDefinitionIdentity Clone();
	}
}
