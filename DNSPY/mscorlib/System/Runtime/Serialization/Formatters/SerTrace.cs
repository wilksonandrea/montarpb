using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000766 RID: 1894
	internal static class SerTrace
	{
		// Token: 0x06005319 RID: 21273 RVA: 0x00123A8A File Offset: 0x00121C8A
		[Conditional("_LOGGING")]
		internal static void InfoLog(params object[] messages)
		{
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x00123A8C File Offset: 0x00121C8C
		[Conditional("SER_LOGGING")]
		internal static void Log(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			int num = 0;
			object obj = messages[0];
			messages[num] = ((obj != null) ? obj.ToString() : null) + " ";
		}
	}
}
