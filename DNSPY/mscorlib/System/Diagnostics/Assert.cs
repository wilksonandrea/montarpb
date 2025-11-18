using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003E1 RID: 993
	internal static class Assert
	{
		// Token: 0x060032F1 RID: 13041 RVA: 0x000C4801 File Offset: 0x000C2A01
		static Assert()
		{
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000C480D File Offset: 0x000C2A0D
		internal static void Check(bool condition, string conditionString, string message)
		{
			if (!condition)
			{
				Assert.Fail(conditionString, message, null, -2146232797);
			}
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000C481F File Offset: 0x000C2A1F
		internal static void Check(bool condition, string conditionString, string message, int exitCode)
		{
			if (!condition)
			{
				Assert.Fail(conditionString, message, null, exitCode);
			}
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000C482D File Offset: 0x000C2A2D
		internal static void Fail(string conditionString, string message)
		{
			Assert.Fail(conditionString, message, null, -2146232797);
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000C483C File Offset: 0x000C2A3C
		internal static void Fail(string conditionString, string message, string windowTitle, int exitCode)
		{
			Assert.Fail(conditionString, message, windowTitle, exitCode, StackTrace.TraceFormat.Normal, 0);
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000C4849 File Offset: 0x000C2A49
		internal static void Fail(string conditionString, string message, int exitCode, StackTrace.TraceFormat stackTraceFormat)
		{
			Assert.Fail(conditionString, message, null, exitCode, stackTraceFormat, 0);
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000C4858 File Offset: 0x000C2A58
		[SecuritySafeCritical]
		internal static void Fail(string conditionString, string message, string windowTitle, int exitCode, StackTrace.TraceFormat stackTraceFormat, int numStackFramesToSkip)
		{
			StackTrace stackTrace = new StackTrace(numStackFramesToSkip, true);
			AssertFilters assertFilters = Assert.Filter.AssertFailure(conditionString, message, stackTrace, stackTraceFormat, windowTitle);
			if (assertFilters == AssertFilters.FailDebug)
			{
				if (Debugger.IsAttached)
				{
					Debugger.Break();
					return;
				}
				if (!Debugger.Launch())
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DebuggerLaunchFailed"));
				}
			}
			else if (assertFilters == AssertFilters.FailTerminate)
			{
				if (Debugger.IsAttached)
				{
					Environment._Exit(exitCode);
					return;
				}
				Environment.FailFast(message, (uint)exitCode);
			}
		}

		// Token: 0x060032F8 RID: 13048
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ShowDefaultAssertDialog(string conditionString, string message, string stackTrace, string windowTitle);

		// Token: 0x04001699 RID: 5785
		internal const int COR_E_FAILFAST = -2146232797;

		// Token: 0x0400169A RID: 5786
		private static AssertFilter Filter = new DefaultFilter();
	}
}
