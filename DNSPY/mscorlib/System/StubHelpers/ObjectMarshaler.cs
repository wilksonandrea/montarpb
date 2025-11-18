using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x02000597 RID: 1431
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class ObjectMarshaler
	{
		// Token: 0x060042E2 RID: 17122
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertToNative(object objSrc, IntPtr pDstVariant);

		// Token: 0x060042E3 RID: 17123
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object ConvertToManaged(IntPtr pSrcVariant);

		// Token: 0x060042E4 RID: 17124
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr pVariant);
	}
}
