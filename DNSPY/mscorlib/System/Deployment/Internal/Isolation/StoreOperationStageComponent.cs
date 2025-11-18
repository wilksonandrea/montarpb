using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A2 RID: 1698
	internal struct StoreOperationStageComponent
	{
		// Token: 0x06004FC9 RID: 20425 RVA: 0x0011C5C4 File Offset: 0x0011A7C4
		public void Destroy()
		{
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x0011C5C6 File Offset: 0x0011A7C6
		public StoreOperationStageComponent(IDefinitionAppId app, string Manifest)
		{
			this = new StoreOperationStageComponent(app, null, Manifest);
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x0011C5D1 File Offset: 0x0011A7D1
		public StoreOperationStageComponent(IDefinitionAppId app, IDefinitionIdentity comp, string Manifest)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationStageComponent));
			this.Flags = StoreOperationStageComponent.OpFlags.Nothing;
			this.Application = app;
			this.Component = comp;
			this.ManifestPath = Manifest;
		}

		// Token: 0x0400222B RID: 8747
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400222C RID: 8748
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationStageComponent.OpFlags Flags;

		// Token: 0x0400222D RID: 8749
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400222E RID: 8750
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionIdentity Component;

		// Token: 0x0400222F RID: 8751
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ManifestPath;

		// Token: 0x02000C46 RID: 3142
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003767 RID: 14183
			Nothing = 0
		}

		// Token: 0x02000C47 RID: 3143
		public enum Disposition
		{
			// Token: 0x04003769 RID: 14185
			Failed,
			// Token: 0x0400376A RID: 14186
			Installed,
			// Token: 0x0400376B RID: 14187
			Refreshed,
			// Token: 0x0400376C RID: 14188
			AlreadyInstalled
		}
	}
}
