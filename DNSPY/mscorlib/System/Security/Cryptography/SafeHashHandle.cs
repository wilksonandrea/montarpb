using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000290 RID: 656
	[SecurityCritical]
	internal sealed class SafeHashHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600234F RID: 9039 RVA: 0x00080080 File Offset: 0x0007E280
		private SafeHashHandle()
			: base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x00080094 File Offset: 0x0007E294
		private SafeHashHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000800A4 File Offset: 0x0007E2A4
		internal static SafeHashHandle InvalidHandle
		{
			get
			{
				return new SafeHashHandle();
			}
		}

		// Token: 0x06002352 RID: 9042
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FreeHash(IntPtr pHashContext);

		// Token: 0x06002353 RID: 9043 RVA: 0x000800AB File Offset: 0x0007E2AB
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeHashHandle.FreeHash(this.handle);
			return true;
		}
	}
}
