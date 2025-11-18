using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C4 RID: 708
	internal struct DigestInfoAsn
	{
		// Token: 0x06002529 RID: 9513 RVA: 0x000870D9 File Offset: 0x000852D9
		internal static DigestInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return DigestInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000870E8 File Offset: 0x000852E8
		internal static DigestInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			DigestInfoAsn digestInfoAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				DigestInfoAsn digestInfoAsn;
				DigestInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out digestInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				digestInfoAsn2 = digestInfoAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return digestInfoAsn2;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00087138 File Offset: 0x00085338
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out DigestInfoAsn decoded)
		{
			DigestInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x00087148 File Offset: 0x00085348
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out DigestInfoAsn decoded)
		{
			try
			{
				DigestInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00087180 File Offset: 0x00085380
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out DigestInfoAsn decoded)
		{
			decoded = default(DigestInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.DigestAlgorithm);
			ReadOnlySpan<byte> readOnlySpan;
			if (asnValueReader.TryReadPrimitiveOctetString(out readOnlySpan))
			{
				int num;
				decoded.Digest = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			}
			else
			{
				decoded.Digest = asnValueReader.ReadOctetString();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000DF9 RID: 3577
		internal AlgorithmIdentifierAsn DigestAlgorithm;

		// Token: 0x04000DFA RID: 3578
		internal ReadOnlyMemory<byte> Digest;
	}
}
