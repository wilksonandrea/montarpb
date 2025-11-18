using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime
{
	// Token: 0x0200071C RID: 1820
	public static class ProfileOptimization
	{
		// Token: 0x0600513B RID: 20795
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void InternalSetProfileRoot(string directoryPath);

		// Token: 0x0600513C RID: 20796
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void InternalStartProfile(string profile, IntPtr ptrNativeAssemblyLoadContext);

		// Token: 0x0600513D RID: 20797 RVA: 0x0011E47D File Offset: 0x0011C67D
		[SecurityCritical]
		public static void SetProfileRoot(string directoryPath)
		{
			ProfileOptimization.InternalSetProfileRoot(directoryPath);
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x0011E485 File Offset: 0x0011C685
		[SecurityCritical]
		public static void StartProfile(string profile)
		{
			ProfileOptimization.InternalStartProfile(profile, IntPtr.Zero);
		}
	}
}
