using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009FD RID: 2557
	internal sealed class IDisposableToIClosableAdapter
	{
		// Token: 0x060064F3 RID: 25843 RVA: 0x00157ACB File Offset: 0x00155CCB
		private IDisposableToIClosableAdapter()
		{
		}

		// Token: 0x060064F4 RID: 25844 RVA: 0x00157AD4 File Offset: 0x00155CD4
		[SecurityCritical]
		public void Close()
		{
			IDisposable disposable = JitHelpers.UnsafeCast<IDisposable>(this);
			disposable.Dispose();
		}
	}
}
