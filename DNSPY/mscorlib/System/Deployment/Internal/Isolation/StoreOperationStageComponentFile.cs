using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A3 RID: 1699
	internal struct StoreOperationStageComponentFile
	{
		// Token: 0x06004FCC RID: 20428 RVA: 0x0011C604 File Offset: 0x0011A804
		public StoreOperationStageComponentFile(IDefinitionAppId App, string CompRelPath, string SrcFile)
		{
			this = new StoreOperationStageComponentFile(App, null, CompRelPath, SrcFile);
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x0011C610 File Offset: 0x0011A810
		public StoreOperationStageComponentFile(IDefinitionAppId App, IDefinitionIdentity Component, string CompRelPath, string SrcFile)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationStageComponentFile));
			this.Flags = StoreOperationStageComponentFile.OpFlags.Nothing;
			this.Application = App;
			this.Component = Component;
			this.ComponentRelativePath = CompRelPath;
			this.SourceFilePath = SrcFile;
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x0011C64B File Offset: 0x0011A84B
		public void Destroy()
		{
		}

		// Token: 0x04002230 RID: 8752
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002231 RID: 8753
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationStageComponentFile.OpFlags Flags;

		// Token: 0x04002232 RID: 8754
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x04002233 RID: 8755
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionIdentity Component;

		// Token: 0x04002234 RID: 8756
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ComponentRelativePath;

		// Token: 0x04002235 RID: 8757
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourceFilePath;

		// Token: 0x02000C48 RID: 3144
		[Flags]
		public enum OpFlags
		{
			// Token: 0x0400376E RID: 14190
			Nothing = 0
		}

		// Token: 0x02000C49 RID: 3145
		public enum Disposition
		{
			// Token: 0x04003770 RID: 14192
			Failed,
			// Token: 0x04003771 RID: 14193
			Installed,
			// Token: 0x04003772 RID: 14194
			Refreshed,
			// Token: 0x04003773 RID: 14195
			AlreadyInstalled
		}
	}
}
