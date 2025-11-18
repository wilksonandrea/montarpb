using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A7 RID: 1703
	internal struct StoreOperationInstallDeployment
	{
		// Token: 0x06004FD7 RID: 20439 RVA: 0x0011C76F File Offset: 0x0011A96F
		public StoreOperationInstallDeployment(IDefinitionAppId App, StoreApplicationReference reference)
		{
			this = new StoreOperationInstallDeployment(App, true, reference);
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0011C77C File Offset: 0x0011A97C
		[SecuritySafeCritical]
		public StoreOperationInstallDeployment(IDefinitionAppId App, bool UninstallOthers, StoreApplicationReference reference)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationInstallDeployment));
			this.Flags = StoreOperationInstallDeployment.OpFlags.Nothing;
			this.Application = App;
			if (UninstallOthers)
			{
				this.Flags |= StoreOperationInstallDeployment.OpFlags.UninstallOthers;
			}
			this.Reference = reference.ToIntPtr();
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0011C7CA File Offset: 0x0011A9CA
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x04002244 RID: 8772
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002245 RID: 8773
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationInstallDeployment.OpFlags Flags;

		// Token: 0x04002246 RID: 8774
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04002247 RID: 8775
		public IntPtr Reference;

		// Token: 0x02000C4F RID: 3151
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003782 RID: 14210
			Nothing = 0,
			// Token: 0x04003783 RID: 14211
			UninstallOthers = 1
		}

		// Token: 0x02000C50 RID: 3152
		public enum Disposition
		{
			// Token: 0x04003785 RID: 14213
			Failed,
			// Token: 0x04003786 RID: 14214
			AlreadyInstalled,
			// Token: 0x04003787 RID: 14215
			Installed
		}
	}
}
