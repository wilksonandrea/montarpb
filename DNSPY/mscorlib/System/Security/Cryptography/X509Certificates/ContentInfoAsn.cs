using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C3 RID: 707
	internal struct ContentInfoAsn
	{
		// Token: 0x06002524 RID: 9508 RVA: 0x00086F9E File Offset: 0x0008519E
		internal static ContentInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return ContentInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x00086FAC File Offset: 0x000851AC
		internal static ContentInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			ContentInfoAsn contentInfoAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				ContentInfoAsn contentInfoAsn;
				ContentInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out contentInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				contentInfoAsn2 = contentInfoAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return contentInfoAsn2;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x00086FFC File Offset: 0x000851FC
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out ContentInfoAsn decoded)
		{
			ContentInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0008700C File Offset: 0x0008520C
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out ContentInfoAsn decoded)
		{
			try
			{
				ContentInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x00087044 File Offset: 0x00085244
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out ContentInfoAsn decoded)
		{
			decoded = default(ContentInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.ContentType = asnValueReader.ReadObjectIdentifier();
			AsnValueReader asnValueReader2 = asnValueReader.ReadSequence(new Asn1Tag?(new Asn1Tag(TagClass.ContextSpecific, 0)));
			ReadOnlySpan<byte> readOnlySpan = asnValueReader2.ReadEncodedValue();
			int num;
			decoded.Content = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			asnValueReader2.ThrowIfNotEmpty();
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000DF7 RID: 3575
		internal byte[] ContentType;

		// Token: 0x04000DF8 RID: 3576
		internal ReadOnlyMemory<byte> Content;
	}
}
