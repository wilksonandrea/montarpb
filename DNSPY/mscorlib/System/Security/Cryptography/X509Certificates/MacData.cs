using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C8 RID: 712
	internal struct MacData
	{
		// Token: 0x0600253D RID: 9533 RVA: 0x00087634 File Offset: 0x00085834
		internal static MacData Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return MacData.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x00087644 File Offset: 0x00085844
		internal static MacData Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			MacData macData2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				MacData macData;
				MacData.DecodeCore(ref asnValueReader, expectedTag, encoded, out macData);
				asnValueReader.ThrowIfNotEmpty();
				macData2 = macData;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return macData2;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00087694 File Offset: 0x00085894
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out MacData decoded)
		{
			MacData.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000876A4 File Offset: 0x000858A4
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out MacData decoded)
		{
			try
			{
				MacData.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000876DC File Offset: 0x000858DC
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out MacData decoded)
		{
			decoded = default(MacData);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			DigestInfoAsn.Decode(ref asnValueReader, rebind, out decoded.Mac);
			ReadOnlySpan<byte> readOnlySpan;
			if (asnValueReader.TryReadPrimitiveOctetString(out readOnlySpan))
			{
				int num;
				decoded.MacSalt = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			}
			else
			{
				decoded.MacSalt = asnValueReader.ReadOctetString();
			}
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Integer))
			{
				if (!asnValueReader.TryReadInt32(out decoded.IterationCount))
				{
					asnValueReader.ThrowIfNotEmpty();
				}
			}
			else
			{
				AsnValueReader asnValueReader2 = new AsnValueReader(MacData.s_DefaultIterationCount, AsnEncodingRules.DER);
				if (!asnValueReader2.TryReadInt32(out decoded.IterationCount))
				{
					asnValueReader2.ThrowIfNotEmpty();
				}
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000877C9 File Offset: 0x000859C9
		// Note: this type is marked as 'beforefieldinit'.
		static MacData()
		{
		}

		// Token: 0x04000E03 RID: 3587
		private static readonly byte[] s_DefaultIterationCount = new byte[] { 2, 1, 1 };

		// Token: 0x04000E04 RID: 3588
		internal DigestInfoAsn Mac;

		// Token: 0x04000E05 RID: 3589
		internal ReadOnlyMemory<byte> MacSalt;

		// Token: 0x04000E06 RID: 3590
		internal int IterationCount;
	}
}
