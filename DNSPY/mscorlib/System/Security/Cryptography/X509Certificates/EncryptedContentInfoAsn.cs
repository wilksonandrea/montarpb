using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C5 RID: 709
	internal struct EncryptedContentInfoAsn
	{
		// Token: 0x0600252E RID: 9518 RVA: 0x0008720C File Offset: 0x0008540C
		internal static EncryptedContentInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return EncryptedContentInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x0008721C File Offset: 0x0008541C
		internal static EncryptedContentInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			EncryptedContentInfoAsn encryptedContentInfoAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				EncryptedContentInfoAsn encryptedContentInfoAsn;
				EncryptedContentInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out encryptedContentInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				encryptedContentInfoAsn2 = encryptedContentInfoAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return encryptedContentInfoAsn2;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x0008726C File Offset: 0x0008546C
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out EncryptedContentInfoAsn decoded)
		{
			EncryptedContentInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0008727C File Offset: 0x0008547C
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedContentInfoAsn decoded)
		{
			try
			{
				EncryptedContentInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000872B4 File Offset: 0x000854B4
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedContentInfoAsn decoded)
		{
			decoded = default(EncryptedContentInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.ContentType = asnValueReader.ReadObjectIdentifier();
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.ContentEncryptionAlgorithm);
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(new Asn1Tag(TagClass.ContextSpecific, 0)))
			{
				ReadOnlySpan<byte> readOnlySpan;
				if (asnValueReader.TryReadPrimitiveOctetString(out readOnlySpan, new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 0))))
				{
					int num;
					decoded.EncryptedContent = new ReadOnlyMemory<byte>?(span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
				}
				else
				{
					decoded.EncryptedContent = new ReadOnlyMemory<byte>?(asnValueReader.ReadOctetString(new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 0))));
				}
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000DFB RID: 3579
		internal byte[] ContentType;

		// Token: 0x04000DFC RID: 3580
		internal AlgorithmIdentifierAsn ContentEncryptionAlgorithm;

		// Token: 0x04000DFD RID: 3581
		internal ReadOnlyMemory<byte>? EncryptedContent;
	}
}
