using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A8 RID: 1704
	internal struct StoreOperationUninstallDeployment
	{
		// Token: 0x06004FDA RID: 20442 RVA: 0x0011C7D7 File Offset: 0x0011A9D7
		[SecuritySafeCritical]
		public StoreOperationUninstallDeployment(IDefinitionAppId appid, StoreApplicationReference AppRef)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationUninstallDeployment));
			this.Flags = StoreOperationUninstallDeployment.OpFlags.Nothing;
			this.Application = appid;
			this.Reference = AppRef.ToIntPtr();
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0011C809 File Offset: 0x0011AA09
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04002248 RID: 8776
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002249 RID: 8777
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationUninstallDeployment.OpFlags Flags;

		// Token: 0x0400224A RID: 8778
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400224B RID: 8779
		public IntPtr Reference;

		// Token: 0x02000C51 RID: 3153
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003789 RID: 14217
			Nothing = 0
		}

		// Token: 0x02000C52 RID: 3154
		public enum Disposition
		{
			// Token: 0x0400378B RID: 14219
			Failed,
			// Token: 0x0400378C RID: 14220
			DidNotExist,
			// Token: 0x0400378D RID: 14221
			Uninstalled
		}
	}
}
