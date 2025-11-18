using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C1 RID: 705
	internal struct AlgorithmIdentifierAsn
	{
		// Token: 0x06002517 RID: 9495 RVA: 0x00086CBE File Offset: 0x00084EBE
		internal static AlgorithmIdentifierAsn Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return AlgorithmIdentifierAsn.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00086CCC File Offset: 0x00084ECC
		internal static AlgorithmIdentifierAsn Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			AlgorithmIdentifierAsn algorithmIdentifierAsn2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				AlgorithmIdentifierAsn algorithmIdentifierAsn;
				AlgorithmIdentifierAsn.DecodeCore(ref asnValueReader, expectedTag, encoded, out algorithmIdentifierAsn);
				asnValueReader.ThrowIfNotEmpty();
				algorithmIdentifierAsn2 = algorithmIdentifierAsn;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return algorithmIdentifierAsn2;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00086D1C File Offset: 0x00084F1C
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out AlgorithmIdentifierAsn decoded)
		{
			AlgorithmIdentifierAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00086D2C File Offset: 0x00084F2C
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out AlgorithmIdentifierAsn decoded)
		{
			try
			{
				AlgorithmIdentifierAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00086D64 File Offset: 0x00084F64
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out AlgorithmIdentifierAsn decoded)
		{
			decoded = default(AlgorithmIdentifierAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			ReadOnlySpan<byte> span = rebind.Span;
			decoded.Algorithm = asnValueReader.ReadObjectIdentifier();
			if (asnValueReader.HasData)
			{
				ReadOnlySpan<byte> readOnlySpan = asnValueReader.ReadEncodedValue();
				int num;
				decoded.Parameters = new ReadOnlyMemory<byte>?(span.Overlaps(readOnlySpan, out num) ? rebind.Slice(num, readOnlySpan.Length) : readOnlySpan.ToArray());
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00086DE6 File Offset: 0x00084FE6
		internal bool HasNullEquivalentParameters()
		{
			return AlgorithmIdentifierAsn.RepresentsNull(this.Parameters);
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00086DF4 File Offset: 0x00084FF4
		internal static bool RepresentsNull(ReadOnlyMemory<byte>? parameters)
		{
			if (parameters == null)
			{
				return true;
			}
			ReadOnlySpan<byte> span = parameters.Value.Span;
			return span.Length == 2 && span[0] == 5 && span[1] == 0;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00086E40 File Offset: 0x00085040
		// Note: this type is marked as 'beforefieldinit'.
		static AlgorithmIdentifierAsn()
		{
			byte[] array = new byte[2];
			array[0] = 5;
			AlgorithmIdentifierAsn.ExplicitDerNull = array;
		}

		// Token: 0x04000DF2 RID: 3570
		internal byte[] Algorithm;

		// Token: 0x04000DF3 RID: 3571
		internal ReadOnlyMemory<byte>? Parameters;

		// Token: 0x04000DF4 RID: 3572
		internal static readonly ReadOnlyMemory<byte> ExplicitDerNull;
	}
}
