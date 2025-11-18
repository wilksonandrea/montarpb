using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000598 RID: 1432
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class ValueClassMarshaler
	{
		// Token: 0x060042E5 RID: 17125
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertToNative(IntPtr dst, IntPtr src, IntPtr pMT, ref CleanupWorkList pCleanupWorkList);

		// Token: 0x060042E6 RID: 17126
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertToManaged(IntPtr dst, IntPtr src, IntPtr pMT);

		// Token: 0x060042E7 RID: 17127
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr dst, IntPtr pMT);
	}
}
