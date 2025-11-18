using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000793 RID: 1939
	internal sealed class ObjectNull : IStreamable
	{
		// Token: 0x06005427 RID: 21543 RVA: 0x001284A9 File Offset: 0x001266A9
		internal ObjectNull()
		{
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x001284B1 File Offset: 0x001266B1
		internal void SetNullCount(int nullCount)
		{
			this.nullCount = nullCount;
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x001284BC File Offset: 0x001266BC
		public void Write(__BinaryWriter sout)
		{
			if (this.nullCount == 1)
			{
				sout.WriteByte(10);
				return;
			}
			if (this.nullCount < 256)
			{
				sout.WriteByte(13);
				sout.WriteByte((byte)this.nullCount);
				return;
			}
			sout.WriteByte(14);
			sout.WriteInt32(this.nullCount);
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x00128512 File Offset: 0x00126712
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.Read(input, BinaryHeaderEnum.ObjectNull);
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x00128520 File Offset: 0x00126720
		public void Read(__BinaryParser input, BinaryHeaderEnum binaryHeaderEnum)
		{
			switch (binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ObjectNull:
				this.nullCount = 1;
				return;
			case BinaryHeaderEnum.MessageEnd:
			case BinaryHeaderEnum.Assembly:
				break;
			case BinaryHeaderEnum.ObjectNullMultiple256:
				this.nullCount = (int)input.ReadByte();
				return;
			case BinaryHeaderEnum.ObjectNullMultiple:
				this.nullCount = input.ReadInt32();
				break;
			default:
				return;
			}
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x0012856C File Offset: 0x0012676C
		public void Dump()
		{
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0012856E File Offset: 0x0012676E
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY") && this.nullCount != 1)
			{
				int num = this.nullCount;
			}
		}

		// Token: 0x0400260A RID: 9738
		internal int nullCount;
	}
}
