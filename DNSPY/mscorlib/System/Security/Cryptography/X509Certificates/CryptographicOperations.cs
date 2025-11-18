using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B8 RID: 696
	internal static class CryptographicOperations
	{
		// Token: 0x060024F3 RID: 9459 RVA: 0x00085747 File Offset: 0x00083947
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ZeroMemory(Span<byte> buffer)
		{
			buffer.Clear();
		}
	}
}
