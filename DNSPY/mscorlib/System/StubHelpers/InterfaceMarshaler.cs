using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200059A RID: 1434
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[FriendAccessAllowed]
	internal static class InterfaceMarshaler
	{
		// Token: 0x060042EA RID: 17130
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ConvertToNative(object objSrc, IntPtr itfMT, IntPtr classMT, int flags);

		// Token: 0x060042EB RID: 17131
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object ConvertToManaged(IntPtr pUnk, IntPtr itfMT, IntPtr classMT, int flags);

		// Token: 0x060042EC RID: 17132
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall")]
		internal static extern void ClearNative(IntPtr pUnk);

		// Token: 0x060042ED RID: 17133
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object ConvertToManagedWithoutUnboxing(IntPtr pNative);
	}
}
