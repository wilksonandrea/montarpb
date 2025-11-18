using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003E3 RID: 995
	internal class DefaultFilter : AssertFilter
	{
		// Token: 0x060032FB RID: 13051 RVA: 0x000C48C7 File Offset: 0x000C2AC7
		internal DefaultFilter()
		{
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000C48CF File Offset: 0x000C2ACF
		[SecuritySafeCritical]
		public override AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle)
		{
			return (AssertFilters)Assert.ShowDefaultAssertDialog(condition, message, location.ToString(stackTraceFormat), windowTitle);
		}
	}
}
