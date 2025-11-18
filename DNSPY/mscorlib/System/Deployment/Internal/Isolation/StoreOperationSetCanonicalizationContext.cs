using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006AB RID: 1707
	internal struct StoreOperationSetCanonicalizationContext
	{
		// Token: 0x06004FE3 RID: 20451 RVA: 0x0011CA86 File Offset: 0x0011AC86
		[SecurityCritical]
		public StoreOperationSetCanonicalizationContext(string Bases, string Exports)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationSetCanonicalizationContext));
			this.Flags = StoreOperationSetCanonicalizationContext.OpFlags.Nothing;
			this.BaseAddressFilePath = Bases;
			this.ExportsFilePath = Exports;
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x0011CAB2 File Offset: 0x0011ACB2
		public void Destroy()
		{
		}

		// Token: 0x04002258 RID: 8792
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002259 RID: 8793
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationSetCanonicalizationContext.OpFlags Flags;

		// Token: 0x0400225A RID: 8794
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BaseAddressFilePath;

		// Token: 0x0400225B RID: 8795
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ExportsFilePath;

		// Token: 0x02000C55 RID: 3157
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003794 RID: 14228
			Nothing = 0
		}
	}
}
