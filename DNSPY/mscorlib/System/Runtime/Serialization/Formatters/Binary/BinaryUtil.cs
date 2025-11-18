using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000781 RID: 1921
	internal static class BinaryUtil
	{
		// Token: 0x060053C5 RID: 21445 RVA: 0x00126BF5 File Offset: 0x00124DF5
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, string value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x00126C02 File Offset: 0x00124E02
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, object value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}
	}
}
