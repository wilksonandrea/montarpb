using System;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x02000593 RID: 1427
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class WSTRBufferMarshaler
	{
		// Token: 0x060042D9 RID: 17113 RVA: 0x000F94A8 File Offset: 0x000F76A8
		internal static IntPtr ConvertToNative(string strManaged)
		{
			return IntPtr.Zero;
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x000F94AF File Offset: 0x000F76AF
		internal static string ConvertToManaged(IntPtr bstr)
		{
			return null;
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x000F94B2 File Offset: 0x000F76B2
		internal static void ClearNative(IntPtr pNative)
		{
		}
	}
}
