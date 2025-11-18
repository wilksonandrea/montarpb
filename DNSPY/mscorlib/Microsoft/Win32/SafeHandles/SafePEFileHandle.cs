using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001E RID: 30
	[SecurityCritical]
	internal sealed class SafePEFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000165 RID: 357 RVA: 0x000047A5 File Offset: 0x000029A5
		private SafePEFileHandle()
			: base(true)
		{
		}

		// Token: 0x06000166 RID: 358
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ReleaseSafePEFileHandle(IntPtr peFile);

		// Token: 0x06000167 RID: 359 RVA: 0x000047AE File Offset: 0x000029AE
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafePEFileHandle.ReleaseSafePEFileHandle(this.handle);
			return true;
		}
	}
}
