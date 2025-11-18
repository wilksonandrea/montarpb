using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A5 RID: 1701
	internal struct StoreOperationPinDeployment
	{
		// Token: 0x06004FD2 RID: 20434 RVA: 0x0011C6D1 File Offset: 0x0011A8D1
		[SecuritySafeCritical]
		public StoreOperationPinDeployment(IDefinitionAppId AppId, StoreApplicationReference Ref)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationPinDeployment));
			this.Flags = StoreOperationPinDeployment.OpFlags.NeverExpires;
			this.Application = AppId;
			this.Reference = Ref.ToIntPtr();
			this.ExpirationTime = 0L;
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x0011C70B File Offset: 0x0011A90B
		public StoreOperationPinDeployment(IDefinitionAppId AppId, DateTime Expiry, StoreApplicationReference Ref)
		{
			this = new StoreOperationPinDeployment(AppId, Ref);
			this.Flags |= StoreOperationPinDeployment.OpFlags.NeverExpires;
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x0011C723 File Offset: 0x0011A923
		[SecurityCritical]
		public void Destroy()
		{
			StoreApplicationReference.Destroy(this.Reference);
		}

		// Token: 0x0400223B RID: 8763
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400223C RID: 8764
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationPinDeployment.OpFlags Flags;

		// Token: 0x0400223D RID: 8765
		[MarshalAs(UnmanagedType.Interface)]
		public IDefinitionAppId Application;

		// Token: 0x0400223E RID: 8766
		[MarshalAs(UnmanagedType.I8)]
		public long ExpirationTime;

		// Token: 0x0400223F RID: 8767
		public IntPtr Reference;

		// Token: 0x02000C4B RID: 3147
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003777 RID: 14199
			Nothing = 0,
			// Token: 0x04003778 RID: 14200
			NeverExpires = 1
		}

		// Token: 0x02000C4C RID: 3148
		public enum Disposition
		{
			// Token: 0x0400377A RID: 14202
			Failed,
			// Token: 0x0400377B RID: 14203
			Pinned
		}
	}
}
