using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B2 RID: 690
	internal struct Asn1Tag : IEquatable<Asn1Tag>
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x0008322E File Offset: 0x0008142E
		public TagClass TagClass
		{
			get
			{
				return (TagClass)(this._controlFlags & 192);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x0008323C File Offset: 0x0008143C
		public bool IsConstructed
		{
			get
			{
				return (this._controlFlags & 32) > 0;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x0008324A File Offset: 0x0008144A
		public int TagValue
		{
			get
			{
				return this._tagValue;
			}
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x00083252 File Offset: 0x00081452
		private Asn1Tag(byte controlFlags, int tagValue)
		{
			this._controlFlags = controlFlags & 224;
			this._tagValue = tagValue;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00083269 File Offset: 0x00081469
		public Asn1Tag(UniversalTagNumber universalTagNumber, bool isConstructed)
		{
			this = new Asn1Tag(isConstructed ? 32 : 0, (int)universalTagNumber);
			if (universalTagNumber < UniversalTagNumber.EndOfContents || universalTagNumber > UniversalTagNumber.RelativeObjectIdentifierIRI || universalTagNumber == (UniversalTagNumber)15)
			{
				throw new ArgumentOutOfRangeException("universalTagNumber");
			}
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00083294 File Offset: 0x00081494
		public Asn1Tag(TagClass tagClass, int tagValue, bool isConstructed)
		{
			this = new Asn1Tag((byte)tagClass | (isConstructed ? 32 : 0), tagValue);
			if (tagClass <= TagClass.Application)
			{
				if (tagClass == TagClass.Universal || tagClass == TagClass.Application)
				{
					goto IL_3D;
				}
			}
			else if (tagClass == TagClass.ContextSpecific || tagClass == TagClass.Private)
			{
				goto IL_3D;
			}
			throw new ArgumentOutOfRangeException("tagClass");
			IL_3D:
			if (tagValue < 0)
			{
				throw new ArgumentOutOfRangeException("tagValue");
			}
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000832ED File Offset: 0x000814ED
		public Asn1Tag(TagClass tagClass, int tagValue)
		{
			this = new Asn1Tag(tagClass, tagValue, false);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000832F8 File Offset: 0x000814F8
		public Asn1Tag AsConstructed()
		{
			return new Asn1Tag(this._controlFlags | 32, this.TagValue);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00083310 File Offset: 0x00081510
		public static bool TryDecode(ReadOnlySpan<byte> source, out Asn1Tag tag, out int bytesConsumed)
		{
			tag = default(Asn1Tag);
			bytesConsumed = 0;
			if (source.IsEmpty)
			{
				return false;
			}
			byte b = source[bytesConsumed];
			bytesConsumed++;
			uint num = (uint)(b & 31);
			if (num == 31U)
			{
				num = 0U;
				while (source.Length > bytesConsumed)
				{
					byte b2 = source[bytesConsumed];
					byte b3 = b2 & 127;
					bytesConsumed++;
					if (num >= 33554432U)
					{
						bytesConsumed = 0;
						return false;
					}
					num <<= 7;
					num |= (uint)b3;
					if (num == 0U)
					{
						bytesConsumed = 0;
						return false;
					}
					if ((b2 & 128) != 128)
					{
						if (num <= 30U)
						{
							bytesConsumed = 0;
							return false;
						}
						if (num > 2147483647U)
						{
							bytesConsumed = 0;
							return false;
						}
						goto IL_99;
					}
				}
				bytesConsumed = 0;
				return false;
			}
			IL_99:
			tag = new Asn1Tag(b, (int)num);
			return true;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000833C4 File Offset: 0x000815C4
		public static Asn1Tag Decode(ReadOnlySpan<byte> source, out int bytesConsumed)
		{
			Asn1Tag asn1Tag;
			if (Asn1Tag.TryDecode(source, out asn1Tag, out bytesConsumed))
			{
				return asn1Tag;
			}
			throw new InvalidOperationException("The provided data does not represent a valid tag.");
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000833E8 File Offset: 0x000815E8
		public bool Equals(Asn1Tag other)
		{
			return this._controlFlags == other._controlFlags && this.TagValue == other.TagValue;
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x00083409 File Offset: 0x00081609
		public override bool Equals(object obj)
		{
			return obj is Asn1Tag && this.Equals((Asn1Tag)obj);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x00083421 File Offset: 0x00081621
		public override int GetHashCode()
		{
			return ((int)this._controlFlags << 24) ^ this.TagValue;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x00083433 File Offset: 0x00081633
		public static bool operator ==(Asn1Tag left, Asn1Tag right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0008343D File Offset: 0x0008163D
		public static bool operator !=(Asn1Tag left, Asn1Tag right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0008344A File Offset: 0x0008164A
		public bool HasSameClassAndValue(Asn1Tag other)
		{
			return this.TagValue == other.TagValue && this.TagClass == other.TagClass;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0008346C File Offset: 0x0008166C
		// Note: this type is marked as 'beforefieldinit'.
		static Asn1Tag()
		{
		}

		// Token: 0x04000DAC RID: 3500
		internal static readonly Asn1Tag EndOfContents = new Asn1Tag(0, 0);

		// Token: 0x04000DAD RID: 3501
		public static readonly Asn1Tag Integer = new Asn1Tag(0, 2);

		// Token: 0x04000DAE RID: 3502
		public static readonly Asn1Tag PrimitiveBitString = new Asn1Tag(0, 3);

		// Token: 0x04000DAF RID: 3503
		public static readonly Asn1Tag ConstructedBitString = new Asn1Tag(32, 3);

		// Token: 0x04000DB0 RID: 3504
		public static readonly Asn1Tag PrimitiveOctetString = new Asn1Tag(0, 4);

		// Token: 0x04000DB1 RID: 3505
		public static readonly Asn1Tag ConstructedOctetString = new Asn1Tag(32, 4);

		// Token: 0x04000DB2 RID: 3506
		public static readonly Asn1Tag Null = new Asn1Tag(0, 5);

		// Token: 0x04000DB3 RID: 3507
		public static readonly Asn1Tag ObjectIdentifier = new Asn1Tag(0, 6);

		// Token: 0x04000DB4 RID: 3508
		public static readonly Asn1Tag Sequence = new Asn1Tag(32, 16);

		// Token: 0x04000DB5 RID: 3509
		public static readonly Asn1Tag SetOf = new Asn1Tag(32, 17);

		// Token: 0x04000DB6 RID: 3510
		private const byte ClassMask = 192;

		// Token: 0x04000DB7 RID: 3511
		private const byte ConstructedMask = 32;

		// Token: 0x04000DB8 RID: 3512
		private const byte ControlMask = 224;

		// Token: 0x04000DB9 RID: 3513
		private const byte TagNumberMask = 31;

		// Token: 0x04000DBA RID: 3514
		private readonly byte _controlFlags;

		// Token: 0x04000DBB RID: 3515
		private int _tagValue;
	}
}
