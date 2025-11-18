using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009FE RID: 2558
	[SecurityCritical]
	internal sealed class IClosableToIDisposableAdapter
	{
		// Token: 0x060064F5 RID: 25845 RVA: 0x00157AEE File Offset: 0x00155CEE
		private IClosableToIDisposableAdapter()
		{
		}

		// Token: 0x060064F6 RID: 25846 RVA: 0x00157AF8 File Offset: 0x00155CF8
		[SecurityCritical]
		private void Dispose()
		{
			IClosable closable = JitHelpers.UnsafeCast<IClosable>(this);
			closable.Close();
		}
	}
}
