using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002DA RID: 730
	internal static class BinaryPrimitives
	{
		// Token: 0x060025AE RID: 9646 RVA: 0x000893DA File Offset: 0x000875DA
		public static bool TryReadUInt16BigEndian(ReadOnlySpan<byte> bytes, out ushort value)
		{
			if (bytes.Length < 2)
			{
				value = 0;
				return false;
			}
			value = (ushort)((int)bytes[1] | ((int)bytes[0] << 8));
			return true;
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x00089402 File Offset: 0x00087602
		public static short ReadInt16BigEndian(ReadOnlySpan<byte> bytes)
		{
			return (short)((int)bytes[1] | ((int)bytes[0] << 8));
		}
	}
}
