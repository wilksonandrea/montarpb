using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A4 RID: 1700
	internal struct StoreApplicationReference
	{
		// Token: 0x06004FCF RID: 20431 RVA: 0x0011C64D File Offset: 0x0011A84D
		public StoreApplicationReference(Guid RefScheme, string Id, string NcData)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreApplicationReference));
			this.Flags = StoreApplicationReference.RefFlags.Nothing;
			this.GuidScheme = RefScheme;
			this.Identifier = Id;
			this.NonCanonicalData = NcData;
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0011C680 File Offset: 0x0011A880
		[SecurityCritical]
		public IntPtr ToIntPtr()
		{
			IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<StoreApplicationReference>(this));
			Marshal.StructureToPtr<StoreApplicationReference>(this, intPtr, false);
			return intPtr;
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x0011C6AC File Offset: 0x0011A8AC
		[SecurityCritical]
		public static void Destroy(IntPtr ip)
		{
			if (ip != IntPtr.Zero)
			{
				Marshal.DestroyStructure(ip, typeof(StoreApplicationReference));
				Marshal.FreeCoTaskMem(ip);
			}
		}

		// Token: 0x04002236 RID: 8758
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002237 RID: 8759
		[MarshalAs(UnmanagedType.U4)]
		public StoreApplicationReference.RefFlags Flags;

		// Token: 0x04002238 RID: 8760
		public Guid GuidScheme;

		// Token: 0x04002239 RID: 8761
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Identifier;

		// Token: 0x0400223A RID: 8762
		[MarshalAs(UnmanagedType.LPWStr)]
		public string NonCanonicalData;

		// Token: 0x02000C4A RID: 3146
		[Flags]
		public enum RefFlags
		{
			// Token: 0x04003775 RID: 14197
			Nothing = 0
		}
	}
}
