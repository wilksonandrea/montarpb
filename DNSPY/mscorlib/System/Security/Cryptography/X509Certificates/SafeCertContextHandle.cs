using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002A7 RID: 679
	[SecurityCritical]
	internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002405 RID: 9221 RVA: 0x00082196 File Offset: 0x00080396
		private SafeCertContextHandle()
			: base(true)
		{
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0008219F File Offset: 0x0008039F
		internal SafeCertContextHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x000821B0 File Offset: 0x000803B0
		internal static SafeCertContextHandle InvalidHandle
		{
			get
			{
				SafeCertContextHandle safeCertContextHandle = new SafeCertContextHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertContextHandle);
				return safeCertContextHandle;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x000821CF File Offset: 0x000803CF
		internal IntPtr pCertContext
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return Marshal.ReadIntPtr(this.handle);
			}
		}

		// Token: 0x06002409 RID: 9225
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreePCertContext(IntPtr pCert);

		// Token: 0x0600240A RID: 9226 RVA: 0x000821F4 File Offset: 0x000803F4
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeCertContextHandle._FreePCertContext(this.handle);
			return true;
		}
	}
}
