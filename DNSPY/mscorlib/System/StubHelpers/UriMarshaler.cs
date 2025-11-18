using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200059B RID: 1435
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class UriMarshaler
	{
		// Token: 0x060042EE RID: 17134
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetRawUriFromNative(IntPtr pUri);

		// Token: 0x060042EF RID: 17135
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr CreateNativeUriInstanceHelper(char* rawUri, int strLen);

		// Token: 0x060042F0 RID: 17136 RVA: 0x000F95F4 File Offset: 0x000F77F4
		[SecurityCritical]
		internal unsafe static IntPtr CreateNativeUriInstance(string rawUri)
		{
			char* ptr = rawUri;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UriMarshaler.CreateNativeUriInstanceHelper(ptr, rawUri.Length);
		}
	}
}
