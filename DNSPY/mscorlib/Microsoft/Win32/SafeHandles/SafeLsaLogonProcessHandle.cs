using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000027 RID: 39
	[SecurityCritical]
	internal sealed class SafeLsaLogonProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00004946 File Offset: 0x00002B46
		private SafeLsaLogonProcessHandle()
			: base(true)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000494F File Offset: 0x00002B4F
		internal SafeLsaLogonProcessHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000495F File Offset: 0x00002B5F
		internal static SafeLsaLogonProcessHandle InvalidHandle
		{
			get
			{
				return new SafeLsaLogonProcessHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000496B File Offset: 0x00002B6B
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.LsaDeregisterLogonProcess(this.handle) >= 0;
		}
	}
}
