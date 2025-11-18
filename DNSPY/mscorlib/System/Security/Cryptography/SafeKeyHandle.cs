using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200028F RID: 655
	[SecurityCritical]
	internal sealed class SafeKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600234A RID: 9034 RVA: 0x00080047 File Offset: 0x0007E247
		private SafeKeyHandle()
			: base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0008005B File Offset: 0x0007E25B
		private SafeKeyHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x0008006B File Offset: 0x0007E26B
		internal static SafeKeyHandle InvalidHandle
		{
			get
			{
				return new SafeKeyHandle();
			}
		}

		// Token: 0x0600234D RID: 9037
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FreeKey(IntPtr pKeyCotext);

		// Token: 0x0600234E RID: 9038 RVA: 0x00080072 File Offset: 0x0007E272
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeKeyHandle.FreeKey(this.handle);
			return true;
		}
	}
}
