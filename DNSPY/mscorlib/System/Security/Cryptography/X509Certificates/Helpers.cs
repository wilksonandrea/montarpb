using System;
using System.Diagnostics;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B9 RID: 697
	internal static class Helpers
	{
		// Token: 0x060024F4 RID: 9460 RVA: 0x00085750 File Offset: 0x00083950
		internal static bool SequenceEqual(byte[] left, byte[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00085780 File Offset: 0x00083980
		internal static ReadOnlyMemory<byte> DecodeOctetStringAsMemory(ReadOnlyMemory<byte> encodedOctetString)
		{
			ReadOnlyMemory<byte> readOnlyMemory;
			try
			{
				ReadOnlySpan<byte> span = encodedOctetString.Span;
				ReadOnlySpan<byte> readOnlySpan;
				int num;
				if (AsnDecoder.TryReadPrimitiveOctetString(span, AsnEncodingRules.BER, out readOnlySpan, out num, null))
				{
					if (num != span.Length)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					int num2;
					if (span.Overlaps(readOnlySpan, out num2))
					{
						return encodedOctetString.Slice(num2, readOnlySpan.Length);
					}
					Assert.Fail("input.Overlaps(primitive)", "input.Overlaps(primitive) failed after TryReadPrimitiveOctetString succeeded");
				}
				byte[] array = AsnDecoder.ReadOctetString(span, AsnEncodingRules.BER, out num, null);
				if (num != span.Length)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				readOnlyMemory = array;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return readOnlyMemory;
		}
	}
}
