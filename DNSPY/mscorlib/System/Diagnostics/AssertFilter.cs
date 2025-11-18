using System;

namespace System.Diagnostics
{
	// Token: 0x020003E2 RID: 994
	[Serializable]
	internal abstract class AssertFilter
	{
		// Token: 0x060032F9 RID: 13049
		public abstract AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle);

		// Token: 0x060032FA RID: 13050 RVA: 0x000C48BF File Offset: 0x000C2ABF
		protected AssertFilter()
		{
		}
	}
}
