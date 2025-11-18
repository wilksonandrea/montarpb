using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002A8 RID: 680
	[SecurityCritical]
	internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600240B RID: 9227 RVA: 0x00082202 File Offset: 0x00080402
		private SafeCertStoreHandle()
			: base(true)
		{
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0008220B File Offset: 0x0008040B
		internal SafeCertStoreHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x0008221C File Offset: 0x0008041C
		internal static SafeCertStoreHandle InvalidHandle
		{
			get
			{
				SafeCertStoreHandle safeCertStoreHandle = new SafeCertStoreHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertStoreHandle);
				return safeCertStoreHandle;
			}
		}

		// Token: 0x0600240E RID: 9230
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreeCertStoreContext(IntPtr hCertStore);

		// Token: 0x0600240F RID: 9231 RVA: 0x0008223B File Offset: 0x0008043B
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeCertStoreHandle._FreeCertStoreContext(this.handle);
			return true;
		}
	}
}
