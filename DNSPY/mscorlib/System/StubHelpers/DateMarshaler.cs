using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x02000599 RID: 1433
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class DateMarshaler
	{
		// Token: 0x060042E8 RID: 17128
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern double ConvertToNative(DateTime managedDate);

		// Token: 0x060042E9 RID: 17129
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long ConvertToManaged(double nativeDate);
	}
}
