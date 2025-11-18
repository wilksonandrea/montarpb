using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B7 RID: 695
	internal struct AsnValueReader
	{
		// Token: 0x060024D8 RID: 9432 RVA: 0x000852F7 File Offset: 0x000834F7
		internal AsnValueReader(ReadOnlySpan<byte> span, AsnEncodingRules ruleSet)
		{
			this._span = span;
			this._ruleSet = ruleSet;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x00085307 File Offset: 0x00083507
		internal bool HasData
		{
			get
			{
				return !this._span.IsEmpty;
			}
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00085317 File Offset: 0x00083517
		internal void ThrowIfNotEmpty()
		{
			if (!this._span.IsEmpty)
			{
				new AsnReader(AsnValueReader.s_singleByte, this._ruleSet).ThrowIfNotEmpty();
			}
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x00085340 File Offset: 0x00083540
		internal Asn1Tag PeekTag()
		{
			int num;
			return Asn1Tag.Decode(this._span, out num);
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x0008535C File Offset: 0x0008355C
		internal ReadOnlySpan<byte> PeekContentBytes()
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadEncodedValue(this._span, this._ruleSet, out num, out num2, out num3);
			return this._span.Slice(num, num2);
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00085390 File Offset: 0x00083590
		internal ReadOnlySpan<byte> PeekEncodedValue()
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadEncodedValue(this._span, this._ruleSet, out num, out num2, out num3);
			return this._span.Slice(0, num3);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000853C4 File Offset: 0x000835C4
		internal ReadOnlySpan<byte> ReadEncodedValue()
		{
			ReadOnlySpan<byte> readOnlySpan = this.PeekEncodedValue();
			this._span = this._span.Slice(readOnlySpan.Length);
			return readOnlySpan;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000853F4 File Offset: 0x000835F4
		internal bool TryReadInt32(out int value)
		{
			return this.TryReadInt32(out value, null);
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00085414 File Offset: 0x00083614
		internal bool TryReadInt32(out int value, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadInt32(this._span, this._ruleSet, out value, out num, expectedTag);
			this._span = this._span.Slice(num);
			return flag;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0008544C File Offset: 0x0008364C
		internal ReadOnlySpan<byte> ReadIntegerBytes()
		{
			return this.ReadIntegerBytes(null);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x00085468 File Offset: 0x00083668
		internal ReadOnlySpan<byte> ReadIntegerBytes(Asn1Tag? expectedTag)
		{
			int num;
			ReadOnlySpan<byte> readOnlySpan = AsnDecoder.ReadIntegerBytes(this._span, this._ruleSet, out num, expectedTag);
			this._span = this._span.Slice(num);
			return readOnlySpan;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000854A0 File Offset: 0x000836A0
		internal bool TryReadPrimitiveBitString(out int unusedBitCount, out ReadOnlySpan<byte> value)
		{
			return this.TryReadPrimitiveBitString(out unusedBitCount, out value, null);
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000854C0 File Offset: 0x000836C0
		internal bool TryReadPrimitiveBitString(out int unusedBitCount, out ReadOnlySpan<byte> value, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadPrimitiveBitString(this._span, this._ruleSet, out unusedBitCount, out value, out num, expectedTag);
			this._span = this._span.Slice(num);
			return flag;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000854F8 File Offset: 0x000836F8
		internal byte[] ReadBitString(out int unusedBitCount)
		{
			return this.ReadBitString(out unusedBitCount, null);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00085518 File Offset: 0x00083718
		internal byte[] ReadBitString(out int unusedBitCount, Asn1Tag? expectedTag)
		{
			int num;
			byte[] array = AsnDecoder.ReadBitString(this._span, this._ruleSet, out unusedBitCount, out num, expectedTag);
			this._span = this._span.Slice(num);
			return array;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00085550 File Offset: 0x00083750
		internal bool TryReadPrimitiveOctetString(out ReadOnlySpan<byte> value)
		{
			return this.TryReadPrimitiveOctetString(out value, null);
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00085570 File Offset: 0x00083770
		internal bool TryReadPrimitiveOctetString(out ReadOnlySpan<byte> value, Asn1Tag? expectedTag)
		{
			int num;
			bool flag = AsnDecoder.TryReadPrimitiveOctetString(this._span, this._ruleSet, out value, out num, expectedTag);
			this._span = this._span.Slice(num);
			return flag;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000855A8 File Offset: 0x000837A8
		internal byte[] ReadOctetString()
		{
			return this.ReadOctetString(null);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000855C4 File Offset: 0x000837C4
		internal byte[] ReadOctetString(Asn1Tag? expectedTag)
		{
			int num;
			byte[] array = AsnDecoder.ReadOctetString(this._span, this._ruleSet, out num, expectedTag);
			this._span = this._span.Slice(num);
			return array;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000855FC File Offset: 0x000837FC
		internal byte[] ReadObjectIdentifier()
		{
			return this.ReadObjectIdentifier(null);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x00085618 File Offset: 0x00083818
		internal byte[] ReadObjectIdentifier(Asn1Tag? expectedTag)
		{
			int num;
			byte[] array = AsnDecoder.ReadObjectIdentifier(this._span, this._ruleSet, out num, expectedTag);
			this._span = this._span.Slice(num);
			return array;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00085650 File Offset: 0x00083850
		internal AsnValueReader ReadSequence()
		{
			return this.ReadSequence(null);
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0008566C File Offset: 0x0008386C
		internal AsnValueReader ReadSequence(Asn1Tag? expectedTag)
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadSequence(this._span, this._ruleSet, out num, out num2, out num3, expectedTag);
			ReadOnlySpan<byte> readOnlySpan = this._span.Slice(num, num2);
			this._span = this._span.Slice(num3);
			return new AsnValueReader(readOnlySpan, this._ruleSet);
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000856C0 File Offset: 0x000838C0
		internal AsnValueReader ReadSetOf()
		{
			return this.ReadSetOf(null, false);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000856DD File Offset: 0x000838DD
		internal AsnValueReader ReadSetOf(Asn1Tag? expectedTag)
		{
			return this.ReadSetOf(expectedTag, false);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000856E8 File Offset: 0x000838E8
		internal AsnValueReader ReadSetOf(Asn1Tag? expectedTag, bool skipSortOrderValidation)
		{
			int num;
			int num2;
			int num3;
			AsnDecoder.ReadSetOf(this._span, this._ruleSet, out num, out num2, out num3, skipSortOrderValidation, expectedTag);
			ReadOnlySpan<byte> readOnlySpan = this._span.Slice(num, num2);
			this._span = this._span.Slice(num3);
			return new AsnValueReader(readOnlySpan, this._ruleSet);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x0008573A File Offset: 0x0008393A
		// Note: this type is marked as 'beforefieldinit'.
		static AsnValueReader()
		{
		}

		// Token: 0x04000DC9 RID: 3529
		private static readonly byte[] s_singleByte = new byte[1];

		// Token: 0x04000DCA RID: 3530
		private ReadOnlySpan<byte> _span;

		// Token: 0x04000DCB RID: 3531
		private readonly AsnEncodingRules _ruleSet;
	}
}
