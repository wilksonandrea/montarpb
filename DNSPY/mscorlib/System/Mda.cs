using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x0200010C RID: 268
	internal static class Mda
	{
		// Token: 0x0600104C RID: 4172
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportStreamWriterBufferedDataLost(string text);

		// Token: 0x0600104D RID: 4173
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsStreamWriterBufferedDataLostEnabled();

		// Token: 0x0600104E RID: 4174
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsStreamWriterBufferedDataLostCaptureAllocatedCallStack();

		// Token: 0x0600104F RID: 4175
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void MemberInfoCacheCreation();

		// Token: 0x06001050 RID: 4176
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DateTimeInvalidLocalFormat();

		// Token: 0x06001051 RID: 4177
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInvalidGCHandleCookieProbeEnabled();

		// Token: 0x06001052 RID: 4178
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FireInvalidGCHandleCookieProbe(IntPtr cookie);

		// Token: 0x06001053 RID: 4179
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportErrorSafeHandleRelease(Exception ex);

		// Token: 0x02000AF3 RID: 2803
		internal static class StreamWriterBufferedDataLost
		{
			// Token: 0x170011EF RID: 4591
			// (get) Token: 0x06006A17 RID: 27159 RVA: 0x0016D1F6 File Offset: 0x0016B3F6
			internal static bool Enabled
			{
				[SecuritySafeCritical]
				get
				{
					if (Mda.StreamWriterBufferedDataLost._enabledState == 0)
					{
						if (Mda.IsStreamWriterBufferedDataLostEnabled())
						{
							Mda.StreamWriterBufferedDataLost._enabledState = 1;
						}
						else
						{
							Mda.StreamWriterBufferedDataLost._enabledState = 2;
						}
					}
					return Mda.StreamWriterBufferedDataLost._enabledState == 1;
				}
			}

			// Token: 0x170011F0 RID: 4592
			// (get) Token: 0x06006A18 RID: 27160 RVA: 0x0016D224 File Offset: 0x0016B424
			internal static bool CaptureAllocatedCallStack
			{
				[SecuritySafeCritical]
				get
				{
					if (Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState == 0)
					{
						if (Mda.IsStreamWriterBufferedDataLostCaptureAllocatedCallStack())
						{
							Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState = 1;
						}
						else
						{
							Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState = 2;
						}
					}
					return Mda.StreamWriterBufferedDataLost._captureAllocatedCallStackState == 1;
				}
			}

			// Token: 0x06006A19 RID: 27161 RVA: 0x0016D252 File Offset: 0x0016B452
			[SecuritySafeCritical]
			internal static void ReportError(string text)
			{
				Mda.ReportStreamWriterBufferedDataLost(text);
			}

			// Token: 0x040031B3 RID: 12723
			private static volatile int _enabledState;

			// Token: 0x040031B4 RID: 12724
			private static volatile int _captureAllocatedCallStackState;
		}
	}
}
