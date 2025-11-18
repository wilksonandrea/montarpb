using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000764 RID: 1892
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class InternalRM
	{
		// Token: 0x0600530F RID: 21263 RVA: 0x001239C9 File Offset: 0x00121BC9
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x001239CB File Offset: 0x00121BCB
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("SOAP");
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x001239D7 File Offset: 0x00121BD7
		public InternalRM()
		{
		}
	}
}
