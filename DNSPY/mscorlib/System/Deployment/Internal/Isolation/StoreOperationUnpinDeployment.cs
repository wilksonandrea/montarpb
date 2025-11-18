using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A6 RID: 1702
	internal struct StoreOperationUnpinDeployment
	{
		// Token: 0x06004FD5 RID: 20437 RVA: 0x0011C730 File Offset: 0x0011A930
		[SecuritySafeCritical]
		public StoreOperationUnpinDeployment(IDefinitionAppId app, StoreApplicationReference reference)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationUnpinDeployment));
			this.Flags = StoreOperationUnpinDeployment.OpFlags.Nothing;
			this.Application = app;
			this.Reference = reference.ToIntPtr();
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0011C762 File Offset: 0x0011A962
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04002240 RID: 8768
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002241 RID: 8769
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationUnpinDeployment.OpFlags Flags;

		// Token: 0x04002242 RID: 8770
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04002243 RID: 8771
		public IntPtr Reference;

		// Token: 0x02000C4D RID: 3149
		[Flags]
		public enum OpFlags
		{
			// Token: 0x0400377D RID: 14205
			Nothing = 0
		}

		// Token: 0x02000C4E RID: 3150
		public enum Disposition
		{
			// Token: 0x0400377F RID: 14207
			Failed,
			// Token: 0x04003780 RID: 14208
			Unpinned
		}
	}
}
