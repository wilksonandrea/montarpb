using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002DB RID: 731
	internal static class CryptoPool
	{
		// Token: 0x060025B0 RID: 9648 RVA: 0x00089418 File Offset: 0x00087618
		public static byte[] Rent(int size)
		{
			return new byte[size];
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x00089420 File Offset: 0x00087620
		public static void Return(byte[] array, int clearSize)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(array, 0, clearSize));
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x0008942F File Offset: 0x0008762F
		public static void Return(byte[] array)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(array));
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x0008943C File Offset: 0x0008763C
		public static void Return(ArraySegment<byte> segment, int clearSize)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(segment).Slice(0, clearSize));
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x0008945E File Offset: 0x0008765E
		public static void Return(ArraySegment<byte> segment)
		{
			CryptographicOperations.ZeroMemory(new Span<byte>(segment));
		}
	}
}
