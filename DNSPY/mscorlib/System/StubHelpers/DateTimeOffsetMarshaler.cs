using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000595 RID: 1429
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class DateTimeOffsetMarshaler
	{
		// Token: 0x060042DC RID: 17116 RVA: 0x000F94B4 File Offset: 0x000F76B4
		[SecurityCritical]
		internal static void ConvertToNative(ref DateTimeOffset managedDTO, out DateTimeNative dateTime)
		{
			long utcTicks = managedDTO.UtcTicks;
			dateTime.UniversalTime = utcTicks - 504911232000000000L;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x000F94DC File Offset: 0x000F76DC
		[SecurityCritical]
		internal static void ConvertToManaged(out DateTimeOffset managedLocalDTO, ref DateTimeNative nativeTicks)
		{
			long num = 504911232000000000L + nativeTicks.UniversalTime;
			DateTimeOffset dateTimeOffset = new DateTimeOffset(num, TimeSpan.Zero);
			managedLocalDTO = dateTimeOffset.ToLocalTime(true);
		}

		// Token: 0x04001BD9 RID: 7129
		private const long ManagedUtcTicksAtNativeZero = 504911232000000000L;
	}
}
