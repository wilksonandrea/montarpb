using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200028E RID: 654
	[SecurityCritical]
	internal sealed class SafeProvHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002345 RID: 9029 RVA: 0x0008000E File Offset: 0x0007E20E
		private SafeProvHandle()
			: base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x00080022 File Offset: 0x0007E222
		private SafeProvHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x00080032 File Offset: 0x0007E232
		internal static SafeProvHandle InvalidHandle
		{
			get
			{
				return new SafeProvHandle();
			}
		}

		// Token: 0x06002348 RID: 9032
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FreeCsp(IntPtr pProviderContext);

		// Token: 0x06002349 RID: 9033 RVA: 0x00080039 File Offset: 0x0007E239
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeProvHandle.FreeCsp(this.handle);
			return true;
		}
	}
}
