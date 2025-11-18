using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002CB RID: 715
	internal struct Pbkdf2Params
	{
		// Token: 0x0600254D RID: 9549 RVA: 0x00087A08 File Offset: 0x00085C08
		internal static Pbkdf2Params Decode(ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			return Pbkdf2Params.Decode(Asn1Tag.Sequence, encoded, ruleSet);
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00087A18 File Offset: 0x00085C18
		internal static Pbkdf2Params Decode(Asn1Tag expectedTag, ReadOnlyMemory<byte> encoded, AsnEncodingRules ruleSet)
		{
			Pbkdf2Params pbkdf2Params2;
			try
			{
				AsnValueReader asnValueReader = new AsnValueReader(encoded.Span, ruleSet);
				Pbkdf2Params pbkdf2Params;
				Pbkdf2Params.DecodeCore(ref asnValueReader, expectedTag, encoded, out pbkdf2Params);
				asnValueReader.ThrowIfNotEmpty();
				pbkdf2Params2 = pbkdf2Params;
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
			return pbkdf2Params2;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00087A68 File Offset: 0x00085C68
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
		{
			Pbkdf2Params.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x00087A78 File Offset: 0x00085C78
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
		{
			try
			{
				Pbkdf2Params.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException ex)
			{
				throw new CryptographicException("ASN1 corrupted data.", ex);
			}
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x00087AB0 File Offset: 0x00085CB0
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out Pbkdf2Params decoded)
		{
			decoded = default(Pbkdf2Params);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			Pbkdf2SaltChoice.Decode(ref asnValueReader, rebind, out decoded.Salt);
			if (!asnValueReader.TryReadInt32(out decoded.IterationCount))
			{
				asnValueReader.ThrowIfNotEmpty();
			}
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Integer))
			{
				int num;
				if (asnValueReader.TryReadInt32(out num))
				{
					decoded.KeyLength = new int?(num);
				}
				else
				{
					asnValueReader.ThrowIfNotEmpty();
				}
			}
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Sequence))
			{
				AlgorithmIdentifierAsn.Decode(ref asnValueReader, rebind, out decoded.Prf);
			}
			else
			{
				AsnValueReader asnValueReader2 = new AsnValueReader(Pbkdf2Params.s_DefaultPrf, AsnEncodingRules.DER);
				AlgorithmIdentifierAsn.Decode(ref asnValueReader2, rebind, out decoded.Prf);
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00087B8A File Offset: 0x00085D8A
		// Note: this type is marked as 'beforefieldinit'.
		static Pbkdf2Params()
		{
		}

		// Token: 0x04000E0B RID: 3595
		private static readonly byte[] s_DefaultPrf = new byte[]
		{
			48, 12, 6, 8, 42, 134, 72, 134, 247, 13,
			2, 7, 5, 0
		};

		// Token: 0x04000E0C RID: 3596
		internal Pbkdf2SaltChoice Salt;

		// Token: 0x04000E0D RID: 3597
		internal int IterationCount;

		// Token: 0x04000E0E RID: 3598
		internal int? KeyLength;

		// Token: 0x04000E0F RID: 3599
		internal AlgorithmIdentifierAsn Prf;
	}
}
