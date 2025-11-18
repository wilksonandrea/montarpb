using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C7 RID: 711
	internal struct EncryptedPrivateKeyInfoAsn
	{
		// Token: 0x06002538 RID: 9528 RVA: 0x00087500 File Offset: 0x00085700
		internal static EncryptedPrivateKeyInfoAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return EncryptedPrivateKeyInfoAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00087510 File Offset: 0x00085710
		internal static EncryptedPrivateKeyInfoAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			EncryptedPrivateKeyInfoAsn encryptedPrivateKeyInfoAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				EncryptedPrivateKeyInfoAsn encryptedPrivateKeyInfoAsn;
				EncryptedPrivateKeyInfoAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out encryptedPrivateKeyInfoAsn);
				asnValueReader.ThrowIfNotEmpty();
				encryptedPrivateKeyInfoAsn2 = encryptedPrivateKeyInfoAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return encryptedPrivateKeyInfoAsn2;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00087560 File Offset: 0x00085760
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out EncryptedPrivateKeyInfoAsn decoded)
		{
			EncryptedPrivateKeyInfoAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x00087570 File Offset: 0x00085770
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedPrivateKeyInfoAsn decoded)
		{
			try
			{
				EncryptedPrivateKeyInfoAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000875A8 File Offset: 0x000857A8
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out EncryptedPrivateKeyInfoAsn decoded)
		{
			decoded = default(EncryptedPrivateKeyInfoAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.EncryptionAlgorithm);
			ReadOnlySpan<byte> readOnlySpan;
			if (asnValueReader.TryReadPrimitiveOctetString(out readOnlySpan))
			{
				int num;
				decoded.EncryptedData = (span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			}
			else
			{
				decoded.EncryptedData = asnValueReader.ReadOctetString();
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E01 RID: 3585
		internal AlgorithmIdentifierAsn EncryptionAlgorithm;

		// Token: 0x04000E02 RID: 3586
		internal ReadOnlyMemory<byte> EncryptedData;
	}
}
