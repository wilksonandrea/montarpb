using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008AE RID: 2222
	public static class RuntimeFeature
	{
		// Token: 0x06005D9D RID: 23965 RVA: 0x00149233 File Offset: 0x00147433
		public static bool IsSupported(string feature)
		{
			return feature == "PortablePdb" && !AppContextSwitches.IgnorePortablePDBsInStackTraces;
		}

		// Token: 0x04002A18 RID: 10776
		public const string PortablePdb = "PortablePdb";
	}
}
