using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002CA RID: 714
	internal struct PBES2Params
	{
		// Token: 0x06002548 RID: 9544 RVA: 0x0008791C File Offset: 0x00085B1C
		internal static PBES2Params Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return PBES2Params.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x0008792C File Offset: 0x00085B2C
		internal static PBES2Params Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			PBES2Params pbes2Params2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				PBES2Params pbes2Params;
				PBES2Params.DecodeCore(ref asnValueReader, expectedTag, encoded, out pbes2Params);
				asnValueReader.ThrowIfNotEmpty();
				pbes2Params2 = pbes2Params;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return pbes2Params2;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x0008797C File Offset: 0x00085B7C
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out PBES2Params decoded)
		{
			PBES2Params.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x0008798C File Offset: 0x00085B8C
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBES2Params decoded)
		{
			try
			{
				PBES2Params.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000879C4 File Offset: 0x00085BC4
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PBES2Params decoded)
		{
			decoded = default(PBES2Params);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.KeyDerivationFunc);
			AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.EncryptionScheme);
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x04000E09 RID: 3593
		internal AlgorithmIdentifierAsn KeyDerivationFunc;

		// Token: 0x04000E0A RID: 3594
		internal AlgorithmIdentifierAsn EncryptionScheme;
	}
}
