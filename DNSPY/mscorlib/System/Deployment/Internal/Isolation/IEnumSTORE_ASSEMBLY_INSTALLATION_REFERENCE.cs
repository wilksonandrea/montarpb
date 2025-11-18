using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000682 RID: 1666
	[Guid("d8b1aacb-5142-4abb-bcc1-e9dc9052a89e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE
	{
		// Token: 0x06004F36 RID: 20278
		[SecurityCritical]
		uint Next([In] uint celt, [MarshalAs(UnmanagedType.LPArray)] [Out] StoreApplicationReference[] rgelt);

		// Token: 0x06004F37 RID: 20279
		[SecurityCritical]
		void Skip([In] uint celt);

		// Token: 0x06004F38 RID: 20280
		[SecurityCritical]
		void Reset();

		// Token: 0x06004F39 RID: 20281
		[SecurityCritical]
		IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE Clone();
	}
}
