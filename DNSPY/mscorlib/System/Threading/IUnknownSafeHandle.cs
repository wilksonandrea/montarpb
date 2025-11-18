using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004FC RID: 1276
	[SecurityCritical]
	internal class IUnknownSafeHandle : SafeHandle
	{
		// Token: 0x06003C48 RID: 15432 RVA: 0x000E3DB6 File Offset: 0x000E1FB6
		public IUnknownSafeHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000E3DC4 File Offset: 0x000E1FC4
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x000E3DD6 File Offset: 0x000E1FD6
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			HostExecutionContextManager.ReleaseHostSecurityContext(this.handle);
			return true;
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x000E3DE8 File Offset: 0x000E1FE8
		internal object Clone()
		{
			IUnknownSafeHandle unknownSafeHandle = new IUnknownSafeHandle();
			if (!this.IsInvalid)
			{
				HostExecutionContextManager.CloneHostSecurityContext(this, unknownSafeHandle);
			}
			return unknownSafeHandle;
		}
	}
}
