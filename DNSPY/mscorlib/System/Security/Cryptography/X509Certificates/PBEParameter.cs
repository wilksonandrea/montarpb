using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C9 RID: 713
	internal struct PBEParameter
	{
		// Token: 0x06002543 RID: 9539 RVA: 0x000877E1 File Offset: 0x000859E1
		internal static PBEParameter Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return PBEParameter.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000877F0 File Offset: 0x000859F0
		internal static PBEParameter Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			PBEParameter pbeparameter2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				PBEParameter pbeparameter;
				PBEParameter.DecodeCore(ref asnValueReader, expectedTag, encoded, out pbeparameter);
				asnValueReader.ThrowIfNotEmpty();
				pbeparameter2 = pbeparameter;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return pbeparameter2;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x00087840 File Offset: 0x00085A40
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out PBEParameter decoded)
		{
			PBEParameter.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00087850 File Offset: 0x00085A50
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBEParameter decoded)
		{
			try
			{
				PBEParameter.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x00087888 File Offset: 0x00085A88
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBEParameter decoded)
		{
			decoded = default(PBEParameter);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			ReadOnlySpan<byte> readOnlySpan;
			if (asnValueReader.TryReadPrimitiveOctetString(out readOnlySpan))
			{
				int num;
				decoded.Salt = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			}
			else
			{
				decoded.Salt = asnValueReader.ReadOctetString();
			}
			if (!asnValueReader.TryReadInt32(out decoded.IterationCount))
			{
				asnValueReader.ThrowIfNotEmpty();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E07 RID: 3591
		internal ReadOnlyMemory<byte> Salt;

		// Token: 0x04000E08 RID: 3592
		internal int IterationCount;
	}
}
