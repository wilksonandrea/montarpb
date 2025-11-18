using System;

namespace System.Threading
{
	// Token: 0x0200053A RID: 1338
	internal static class TimeoutHelper
	{
		// Token: 0x06003EC7 RID: 16071 RVA: 0x000E980B File Offset: 0x000E7A0B
		public static uint GetTime()
		{
			return (uint)Environment.TickCount;
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x000E9814 File Offset: 0x000E7A14
		public static int UpdateTimeOut(uint startTime, int originalWaitMillisecondsTimeout)
		{
			uint num = TimeoutHelper.GetTime() - startTime;
			if (num > 2147483647U)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}
	}
}
